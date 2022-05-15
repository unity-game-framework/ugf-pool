using System;
using System.Collections;
using System.Collections.Generic;

namespace UGF.Pool.Runtime
{
    public class PoolCollection<TItem> : IPoolCollection<TItem> where TItem : class
    {
        public int Count { get { return m_items.Count; } }
        public int EnabledCount { get { return m_enabled.Count; } }
        public int DisabledCount { get { return m_disabled.Count; } }
        public IEnumerable<TItem> Enabled { get { return m_enabled; } }
        public IEnumerable<TItem> Disabled { get { return m_disabled; } }

        IEnumerable IPoolCollection.Enabled { get { return m_enabled; } }
        IEnumerable IPoolCollection.Disabled { get { return m_disabled; } }

        private readonly HashSet<TItem> m_items;
        private readonly HashSet<TItem> m_enabled;
        private readonly HashSet<TItem> m_disabled;

        public PoolCollection(int capacity = 4)
        {
            if (capacity < 0) throw new ArgumentOutOfRangeException(nameof(capacity));

            m_items = new HashSet<TItem>(capacity);
            m_enabled = new HashSet<TItem>(capacity);
            m_disabled = new HashSet<TItem>(capacity);
        }

        public HashSet<TItem>.Enumerator GetEnumerator()
        {
            return m_items.GetEnumerator();
        }

        public bool Contains(TItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            return m_items.Contains(item);
        }

        public bool IsEnabled(TItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (!Contains(item)) throw new ArgumentException($"Specified item not from the current collection: '{item}'.");

            return m_enabled.Contains(item);
        }

        public bool IsDisabled(TItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (!Contains(item)) throw new ArgumentException($"Specified item not from the current collection: '{item}'.");

            return m_disabled.Contains(item);
        }

        public bool Add(TItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            if (m_items.Add(item))
            {
                m_disabled.Add(item);

                if (item is IPoolObject poolObject)
                {
                    poolObject.PoolDisable();
                }

                OnAdded(item);
                return true;
            }

            return false;
        }

        public bool Remove(TItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            if (m_items.Contains(item))
            {
                if (IsEnabled(item))
                {
                    Disable(item);
                }

                m_items.Remove(item);
                m_disabled.Remove(item);

                OnRemoved(item);
                return true;
            }

            return false;
        }

        public void Clear()
        {
            while (m_items.Count > 0)
            {
                Remove(GetAny());
            }

            m_items.Clear();
            m_enabled.Clear();
            m_disabled.Clear();

            OnClear();
        }

        public void EnableAll()
        {
            if (m_disabled.Count == 0) throw new InvalidOperationException("Collection has no disabled items.");

            while (m_disabled.Count > 0)
            {
                Enable();
            }
        }

        public void DisableAll()
        {
            if (m_enabled.Count == 0) throw new InvalidOperationException("Collection has no enabled items.");

            while (m_enabled.Count > 0)
            {
                Disable(GetAnyEnabled());
            }
        }

        public TItem Enable()
        {
            return OnEnable();
        }

        public bool Disable(TItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (!Contains(item)) throw new ArgumentException($"Specified item not from the current collection: '{item}'.");

            return OnDisable(item);
        }

        protected virtual void OnAdded(TItem item)
        {
        }

        protected virtual void OnRemoved(TItem item)
        {
        }

        protected virtual void OnClear()
        {
        }

        protected virtual TItem OnEnable()
        {
            if (m_items.Count == 0) throw new InvalidOperationException("Collection is empty.");
            if (m_disabled.Count == 0) throw new InvalidOperationException("Collection has no disabled items.");

            TItem item = GetAnyDisabled();

            m_enabled.Add(item);
            m_disabled.Remove(item);

            OnEnabled(item);

            if (item is IPoolObject poolObject)
            {
                poolObject.PoolEnable();
            }

            return item;
        }

        protected virtual bool OnDisable(TItem item)
        {
            if (m_enabled.Remove(item))
            {
                m_disabled.Add(item);

                OnDisabled(item);

                if (item is IPoolObject poolObject)
                {
                    poolObject.PoolDisable();
                }

                return true;
            }

            return false;
        }

        protected virtual void OnEnabled(TItem item)
        {
        }

        protected virtual void OnDisabled(TItem item)
        {
        }

        protected TItem GetAny()
        {
            return GetAny(m_items);
        }

        protected TItem GetAnyEnabled()
        {
            return GetAny(m_enabled);
        }

        protected TItem GetAnyDisabled()
        {
            return GetAny(m_disabled);
        }

        private TItem GetAny(HashSet<TItem> collection)
        {
            return TryGetAny(collection, out TItem item) ? item : throw new ArgumentException("Collection is empty.");
        }

        private bool TryGetAny(HashSet<TItem> collection, out TItem item)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            using (HashSet<TItem>.Enumerator enumerator = collection.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    item = enumerator.Current;
                    return true;
                }
            }

            item = default;
            return false;
        }

        bool IPoolCollection.Contains(object item)
        {
            return Contains((TItem)item);
        }

        bool IPoolCollection.IsEnabled(object item)
        {
            return IsEnabled((TItem)item);
        }

        bool IPoolCollection.IsDisabled(object item)
        {
            return IsDisabled((TItem)item);
        }

        bool IPoolCollection.Add(object item)
        {
            return Add((TItem)item);
        }

        bool IPoolCollection.Remove(object item)
        {
            return Remove((TItem)item);
        }

        object IPoolCollection.Enable()
        {
            return Enable();
        }

        bool IPoolCollection.Disable(object item)
        {
            return Disable((TItem)item);
        }

        IEnumerator<TItem> IEnumerable<TItem>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
