using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace UGF.Pool.Runtime
{
    public class PoolCollection<TPoolObject> : IPoolCollection<TPoolObject> where TPoolObject : IPoolObject
    {
        public int Count { get { return m_poolObjects.Count; } }
        public int Capacity { get; }
        public int EnabledCount { get { return m_enabled.Count; } }
        public int DisabledCount { get { return m_disabled.Count; } }
        public IEqualityComparer<TPoolObject> Comparer { get { return m_poolObjects.Comparer; } }

        IEnumerable<TPoolObject> IPoolCollection<TPoolObject>.Enabled { get { return m_enabled; } }
        IEnumerable<TPoolObject> IPoolCollection<TPoolObject>.Disabled { get { return m_disabled; } }
        bool ICollection.IsSynchronized { get { return false; } }

        object ICollection.SyncRoot
        {
            get
            {
                if (m_syncRoot == null)
                {
                    Interlocked.CompareExchange<object>(ref m_syncRoot, new object(), null);
                }

                return m_syncRoot;
            }
        }

        private readonly HashSet<TPoolObject> m_poolObjects;
        private readonly HashSet<TPoolObject> m_enabled;
        private readonly Stack<TPoolObject> m_disabled;
        private object m_syncRoot;

        public PoolCollection()
        {
        }

        public bool Contains(TPoolObject poolObject)
        {
            return m_poolObjects.Contains(poolObject);
        }

        public bool IsEnabled(TPoolObject poolObject)
        {
            if (!Contains(poolObject)) throw new InvalidOperationException("The specified pool object not from this collection.");

            return m_enabled.Contains(poolObject);
        }

        public bool IsDisabled(TPoolObject poolObject)
        {
            if (!Contains(poolObject)) throw new InvalidOperationException("The specified pool object not from this collection.");

            return !m_enabled.Contains(poolObject);
        }

        public bool Add(TPoolObject poolObject)
        {
            if (m_poolObjects.Add(poolObject))
            {
                m_disabled.Push(poolObject);

                return true;
            }

            return false;
        }

        public int Add(IList<TPoolObject> list)
        {
            int count = 0;

            for (int i = 0; i < list.Count; i++)
            {
                TPoolObject poolObject = list[i];

                if (Add(poolObject))
                {
                    count++;
                }
            }

            return count;
        }

        public void Remove()
        {
            if (m_disabled.Count == 0) throw new InvalidOperationException("The count of the disabled objects is zero.");

            m_poolObjects.Remove(m_disabled.Pop());
        }

        public void Remove(int count)
        {
            if (count > m_disabled.Count) throw new InvalidOperationException("The specified count greater then the disabled objects count.");

            for (int i = 0; i < count; i++)
            {
                m_poolObjects.Remove(m_disabled.Pop());
            }
        }

        public TPoolObject Enable()
        {
            TPoolObject poolObject = m_disabled.Pop();

            m_enabled.Add(poolObject);

            return poolObject;
        }

        public void EnableAll(IList<TPoolObject> list)
        {
            int count = m_disabled.Count;

            for (int i = 0; i < count; i++)
            {
                list.Add(Enable());
            }
        }

        public bool Disable(TPoolObject poolObject)
        {
            if (m_enabled.Remove(poolObject))
            {
                m_disabled.Push(poolObject);

                return true;
            }

            return false;
        }

        public void DisableAll()
        {
            foreach (TPoolObject poolObject in m_enabled)
            {
                m_disabled.Push(poolObject);
            }

            m_enabled.Clear();
        }

        public void Clear()
        {
            m_poolObjects.Clear();
            m_enabled.Clear();
            m_disabled.Clear();
        }

        public void CopyTo(Array array, int index)
        {
            ((ICollection<TPoolObject>)m_poolObjects).CopyTo((TPoolObject[])array, index);
        }

        public HashSet<TPoolObject>.Enumerator GetEnumerator()
        {
            return m_poolObjects.GetEnumerator();
        }

        IEnumerator<TPoolObject> IEnumerable<TPoolObject>.GetEnumerator()
        {
            return ((IEnumerable<TPoolObject>)m_poolObjects).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)m_poolObjects).GetEnumerator();
        }
    }
}