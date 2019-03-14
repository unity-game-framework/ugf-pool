using System;
using System.Collections;
using System.Collections.Generic;

namespace UGF.Pool.Runtime
{
    public class PoolCollection<TItem> : IPoolCollection<TItem>
    {
        public int Count { get { return m_items.Count; } }
        public int EnabledCount { get { return m_enabled.Count; } }
        public int DisabledCount { get { return m_disabled.Count; } }
        public IEqualityComparer<TItem> Comparer { get { return m_items.Comparer; } }
        public EnabledEnumerable Enabled { get { return m_enabledEnumerable ?? (m_enabledEnumerable = new EnabledEnumerable(m_enabled)); } }
        public DisabledEnumerable Disabled { get { return m_disabledEnumerable ?? (m_disabledEnumerable = new DisabledEnumerable(m_disabled)); } }

        IEnumerable<TItem> IPoolCollection<TItem>.Enabled { get { return m_enabled; } }
        IEnumerable<TItem> IPoolCollection<TItem>.Disabled { get { return m_disabled; } }

        private readonly HashSet<TItem> m_items;
        private readonly HashSet<TItem> m_enabled;
        private readonly Stack<TItem> m_disabled;
        private EnabledEnumerable m_enabledEnumerable;
        private DisabledEnumerable m_disabledEnumerable;

        public sealed class EnabledEnumerable : IEnumerable<TItem>
        {
            private readonly HashSet<TItem> m_collection;

            internal EnabledEnumerable(HashSet<TItem> collection)
            {
                m_collection = collection;
            }

            public HashSet<TItem>.Enumerator GetEnumerator()
            {
                return m_collection.GetEnumerator();
            }

            IEnumerator<TItem> IEnumerable<TItem>.GetEnumerator()
            {
                return ((IEnumerable<TItem>)m_collection).GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable)m_collection).GetEnumerator();
            }
        }

        public sealed class DisabledEnumerable : IEnumerable<TItem>
        {
            private readonly Stack<TItem> m_collection;

            internal DisabledEnumerable(Stack<TItem> collection)
            {
                m_collection = collection;
            }

            public Stack<TItem>.Enumerator GetEnumerator()
            {
                return m_collection.GetEnumerator();
            }

            IEnumerator<TItem> IEnumerable<TItem>.GetEnumerator()
            {
                return ((IEnumerable<TItem>)m_collection).GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable)m_collection).GetEnumerator();
            }
        }

        public PoolCollection(IEqualityComparer<TItem> comparer = null)
        {
            m_items = new HashSet<TItem>(comparer);
            m_enabled = new HashSet<TItem>(comparer);
            m_disabled = new Stack<TItem>();
        }

        public PoolCollection(ICollection<TItem> collection, IEqualityComparer<TItem> comparer = null)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            m_items = new HashSet<TItem>(collection, comparer);
            m_enabled = new HashSet<TItem>(comparer);
            m_disabled = new Stack<TItem>(collection);
        }

        public bool Contains(TItem item)
        {
            return m_items.Contains(item);
        }

        public bool IsEnabled(TItem item)
        {
            if (!Contains(item)) throw new ArgumentException("The specified item not from this collection.", nameof(item));

            return m_enabled.Contains(item);
        }

        public bool IsDisabled(TItem item)
        {
            if (!Contains(item)) throw new ArgumentException("The specified item not from this collection.", nameof(item));

            return !m_enabled.Contains(item);
        }

        public virtual bool Add(TItem item)
        {
            if (m_items.Add(item))
            {
                m_disabled.Push(item);

                return true;
            }

            return false;
        }

        public virtual TItem Remove()
        {
            if (m_disabled.Count == 0) throw new InvalidOperationException("The count of the disabled items is zero.");

            TItem item = m_disabled.Pop();

            m_items.Remove(item);

            return item;
        }

        public virtual void RemoveAll()
        {
            int count = m_disabled.Count;
            
            for (int i = 0; i < count; i++)
            {
                Remove();
            }
        }

        public virtual TItem Enable()
        {
            if (m_disabled.Count == 0) throw new InvalidOperationException("The count of the disabled items is zero.");

            TItem item = m_disabled.Pop();

            m_enabled.Add(item);

            return item;
        }

        public virtual bool Disable(TItem item)
        {
            if (!Contains(item)) throw new ArgumentException("The specified item not from this collection.", nameof(item));

            if (m_enabled.Remove(item))
            {
                m_disabled.Push(item);

                return true;
            }

            return false;
        }

        public virtual void DisableAll()
        {
            int count = m_enabled.Count;

            for (int i = 0; i < count; i++)
            {
                foreach (TItem item in m_enabled)
                {
                    Disable(item);
                    break;
                }
            }
        }

        public virtual void Clear()
        {
            m_items.Clear();
            m_enabled.Clear();
            m_disabled.Clear();
        }

        public HashSet<TItem>.Enumerator GetEnumerator()
        {
            return m_items.GetEnumerator();
        }

        IEnumerator<TItem> IEnumerable<TItem>.GetEnumerator()
        {
            return ((IEnumerable<TItem>)m_items).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)m_items).GetEnumerator();
        }
    }
}