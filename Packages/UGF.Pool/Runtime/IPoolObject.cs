namespace UGF.Pool.Runtime
{
    /// <summary>
    /// Represents the pool object that can be enable and disable.
    /// </summary>
    public interface IPoolObject
    {
        /// <summary>
        /// Enables pool object.
        /// </summary>
        void PoolEnable();
        
        /// <summary>
        /// Disable pool object.
        /// </summary>
        void PoolDisable();
    }
}