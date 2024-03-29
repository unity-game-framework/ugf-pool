using System.Collections.Generic;

namespace UGF.Pool.Runtime
{
    public interface IPoolCollection<TItem> : IPoolCollection, IEnumerable<TItem> where TItem : class
    {
        new IEnumerable<TItem> Enabled { get; }
        new IEnumerable<TItem> Disabled { get; }

        new event PoolItemHandler<TItem> Added;
        new event PoolItemHandler<TItem> Removed;
        new event PoolItemHandler<TItem> ItemEnabled;
        new event PoolItemHandler<TItem> ItemDisabled;

        bool Contains(TItem item);
        bool IsEnabled(TItem item);
        bool IsDisabled(TItem item);
        bool Add(TItem item);
        bool Remove(TItem item);
        new TItem Enable();
        bool Disable(TItem item);
    }
}
