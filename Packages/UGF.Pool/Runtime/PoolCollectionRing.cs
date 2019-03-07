using System.Collections.Generic;

namespace UGF.Pool.Runtime
{
    public class PoolCollectionRing<TItem> : PoolCollection<TItem>
    {
        public PoolCollectionRing(IEqualityComparer<TItem> comparer = null) : base(comparer)
        {
        }

        public PoolCollectionRing(ICollection<TItem> collection, IEqualityComparer<TItem> comparer = null) : base(collection, comparer)
        {
        }
    }
}