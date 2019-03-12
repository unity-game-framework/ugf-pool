using System.Collections.Generic;

namespace UGF.Pool.Runtime
{
    public interface IPoolCollection<TItem> : IPoolCollection, IEnumerable<TItem>
    {
        IEnumerable<TItem> Enabled { get; }
        IEnumerable<TItem> Disabled { get; }
        IEqualityComparer<TItem> Comparer { get; }
        
        bool Contains(TItem item);
        bool IsEnabled(TItem item);
        bool IsDisabled(TItem item);
        bool Add(TItem item);
        TItem Remove();
        void RemoveAll();
        TItem Enable();
        bool Disable(TItem item);
        void DisableAll();
        void Clear();
    }
}