using System.Collections;

namespace UGF.Pool.Runtime
{
    /// <summary>
    /// Represents abstract collection of the pooled items.
    /// </summary>
    public interface IPoolCollection : IEnumerable
    {
        /// <summary>
        /// Gets the total count of items in collection.
        /// </summary>
        int Count { get; }
        
        /// <summary>
        /// Gets the total count of enabled items in collection.
        /// </summary>
        int EnabledCount { get; }
        
        /// <summary>
        /// Gets the total count of disabled items in collection.
        /// </summary>
        int DisabledCount { get; }
    }
}