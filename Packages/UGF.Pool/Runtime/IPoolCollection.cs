using System.Collections;

namespace UGF.Pool.Runtime
{
    public interface IPoolCollection : ICollection
    {
        int Capacity { get; }
        int EnabledCount { get; }
        int DisabledCount { get; }
    }
}