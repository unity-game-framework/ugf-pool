using UGF.RuntimeTools.Runtime.Contexts;

namespace UGF.Pool.Runtime
{
    public delegate void PoolItemDestroyHandler<in TItem>(TItem item, IContext context) where TItem : class;
}
