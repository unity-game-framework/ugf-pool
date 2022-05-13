using System;
using UGF.RuntimeTools.Runtime.Contexts;

namespace UGF.Pool.Runtime
{
    public class PoolCollectionDynamicHandlers<TItem> : PoolCollectionDynamic<TItem> where TItem : class
    {
        public PoolItemBuildHandler<TItem> BuildHandler { get; }
        public PoolItemDestroyHandler<TItem> DestroyHandler { get; }

        public PoolCollectionDynamicHandlers(IContext context, PoolItemBuildHandler<TItem> buildHandler, PoolItemDestroyHandler<TItem> destroyHandler, int capacity = 4) : base(context, capacity)
        {
            BuildHandler = buildHandler ?? throw new ArgumentNullException(nameof(buildHandler));
            DestroyHandler = destroyHandler ?? throw new ArgumentNullException(nameof(destroyHandler));
        }

        protected override TItem OnBuild()
        {
            return BuildHandler.Invoke(Context);
        }

        protected override void OnDestroy(TItem item)
        {
            DestroyHandler.Invoke(item, Context);
        }
    }
}
