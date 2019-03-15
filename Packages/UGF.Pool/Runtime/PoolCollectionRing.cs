using System;
using System.Collections.Generic;

namespace UGF.Pool.Runtime
{
    public class PoolCollectionRing<TItem> : PoolCollection<TItem>
    {
        private readonly Dictionary<TItem, LinkedListNode<TItem>> m_nodes = new Dictionary<TItem, LinkedListNode<TItem>>();
        private readonly LinkedList<TItem> m_used = new LinkedList<TItem>();
        
        public PoolCollectionRing(IEqualityComparer<TItem> comparer = null) : base(comparer)
        {
        }

        public PoolCollectionRing(ICollection<TItem> collection, IEqualityComparer<TItem> comparer = null) : base(collection, comparer)
        {
        }

        public override bool Add(TItem item)
        {
            if (base.Add(item))
            {
                m_nodes.Add(item, new LinkedListNode<TItem>(item));
                
                return true;
            }

            return false;
        }

        public override TItem Remove()
        {
            TItem item = base.Remove();

            m_nodes.Remove(item);
            
            return item;
        }

        public override TItem Enable()
        {
            if (Count == 0) throw new InvalidOperationException("The count of the items is zero.");

            LinkedListNode<TItem> node = null;
            
            if (DisabledCount == 0)
            {
                node = m_used.Last;
                
                Disable(node.Value);
            }
            
            TItem item = base.Enable();
            
            m_used.AddFirst(node ?? m_nodes[item]);
            
            return item;
        }

        public override bool Disable(TItem item)
        {
            if (base.Disable(item))
            {
                m_used.Remove(m_nodes[item]);
                
                return true;
            }

            return false;
        }

        public override void Clear()
        {
            base.Clear();
            
            m_used.Clear();
            m_nodes.Clear();
        }
    }
}