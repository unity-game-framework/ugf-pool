namespace UGF.Pool.Runtime
{
    public delegate void PoolItemHandler<in TItem>(TItem item) where TItem : class;
}
