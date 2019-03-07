using System.Collections;

namespace UGF.Pool.Runtime
{
    public interface IPoolCollection : IEnumerable
    {
        int Count { get; }
        int EnabledCount { get; }
        int DisabledCount { get; }
    }
}