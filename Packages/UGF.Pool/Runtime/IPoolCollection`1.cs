using System.Collections.Generic;

namespace UGF.Pool.Runtime
{
    public interface IPoolCollection<TPoolObject> : IPoolCollection, IEnumerable<TPoolObject> where TPoolObject : IPoolObject
    {
        IEnumerable<TPoolObject> Enabled { get; }
        IEnumerable<TPoolObject> Disabled { get; }
        IEqualityComparer<TPoolObject> Comparer { get; }
        
        bool Contains(TPoolObject poolObject);
        bool IsEnabled(TPoolObject poolObject);
        bool IsDisabled(TPoolObject poolObject);
        bool Add(TPoolObject poolObject);
        int Add(IList<TPoolObject> list);
        void Remove();
        void Remove(int count);
        TPoolObject Enable();
        void EnableAll(IList<TPoolObject> list);
        bool Disable(TPoolObject poolObject);
        void DisableAll();
        void Clear();
    }
}