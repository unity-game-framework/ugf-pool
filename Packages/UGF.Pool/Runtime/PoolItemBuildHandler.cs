namespace UGF.Pool.Runtime
{
    public delegate TItem PoolItemBuildHandler<out TItem>() where TItem : class;
}
