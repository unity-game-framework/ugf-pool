using System;
using System.Collections.Generic;

namespace UGF.Pool.Runtime
{
    public class PoolCollectionCycle<TItem> : PoolCollection<TItem> where TItem : class
    {
        private readonly Dictionary<TItem, LinkedListNode<TItem>> m_nodes = new Dictionary<TItem, LinkedListNode<TItem>>();
        private readonly LinkedList<TItem> m_used = new LinkedList<TItem>();

        public PoolCollectionCycle(int capacity = 4) : base(capacity)
        {
        }

        protected override void OnAdded(TItem item)
        {
            base.OnAdded(item);

            m_nodes.Add(item, new LinkedListNode<TItem>(item));
        }

        protected override void OnRemoved(TItem item)
        {
            base.OnRemoved(item);

            m_nodes.Remove(item);
        }

        protected override TItem OnEnable()
        {
            if (Count == 0) throw new InvalidOperationException("Collection is empty.");

            if (DisabledCount == 0)
            {
                Disable(m_used.Last.Value);
            }

            TItem item = base.OnEnable();
            LinkedListNode<TItem> node = m_nodes[item];

            m_used.AddFirst(node);

            return item;
        }

        protected override void OnDisabled(TItem item)
        {
            base.OnDisabled(item);

            m_used.Remove(m_nodes[item]);
        }

        protected override void OnClear()
        {
            base.OnClear();

            m_nodes.Clear();
            m_used.Clear();
        }
    }
}
