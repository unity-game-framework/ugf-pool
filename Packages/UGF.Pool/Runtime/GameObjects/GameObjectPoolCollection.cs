using UGF.RuntimeTools.Runtime.Contexts;
using Object = UnityEngine.Object;

namespace UGF.Pool.Runtime.GameObjects
{
    public class GameObjectPoolCollection<TItem> : PoolCollectionDynamic<TItem> where TItem : GameObjectPoolBehaviour
    {
        public bool DestroyOnRemove { get; set; } = true;

        public GameObjectPoolCollection(PoolItemBuildHandler<TItem> builder, IContext context, int capacity = 4) : base(builder, context, capacity)
        {
        }

        protected override void OnRemoved(TItem item)
        {
            base.OnRemoved(item);

            if (DestroyOnRemove)
            {
                Object.Destroy(item.gameObject);
            }
        }
    }
}
