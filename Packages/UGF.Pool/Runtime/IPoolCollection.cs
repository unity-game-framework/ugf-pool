using System;
using System.Collections;

namespace UGF.Pool.Runtime
{
    public interface IPoolCollection : IEnumerable
    {
        int Count { get; }
        int EnabledCount { get; }
        int DisabledCount { get; }
        IEnumerable Enabled { get; }
        IEnumerable Disabled { get; }

        event PoolItemHandler<object> Added;
        event PoolItemHandler<object> Removed;
        event Action Cleared;
        event PoolItemHandler<object> ItemEnabled;
        event PoolItemHandler<object> ItemDisabled;

        bool Contains(object item);
        bool IsEnabled(object item);
        bool IsDisabled(object item);
        bool Add(object item);
        bool Remove(object item);
        void EnableAll();
        void DisableAll();
        object Enable();
        bool Disable(object item);
        void Clear();
    }
}
