using UGF.RuntimeTools.Runtime.Contexts;

namespace UGF.Pool.Runtime
{
    public delegate TItem PoolItemBuildHandler<out TItem>(IContext context) where TItem : class;
}
