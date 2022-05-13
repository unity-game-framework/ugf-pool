using System;
using UGF.RuntimeTools.Runtime.Contexts;
using Object = UnityEngine.Object;

namespace UGF.Pool.Runtime.GameObjects
{
    public class GameObjectPoolCollection<TItem> : PoolCollectionDynamic<TItem> where TItem : PoolComponent
    {
        public TItem Source { get; }

        public GameObjectPoolCollection(TItem source, int capacity = 4) : this(new Context(), source, capacity)
        {
        }

        public GameObjectPoolCollection(IContext context, TItem source, int capacity = 4) : base(context, capacity)
        {
            Source = source ? source : throw new ArgumentNullException(nameof(source));
        }

        protected override TItem OnBuild()
        {
            return Object.Instantiate(Source);
        }

        protected override void OnDestroy(TItem item)
        {
            Object.Destroy(item.gameObject);
        }
    }
}
