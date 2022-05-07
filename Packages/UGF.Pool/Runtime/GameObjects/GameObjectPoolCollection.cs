using System;
using UGF.Builder.Runtime.GameObjects;
using Object = UnityEngine.Object;

namespace UGF.Pool.Runtime.GameObjects
{
    public class GameObjectPoolCollection<TItem> : PoolCollectionDynamic<TItem> where TItem : GameObjectPoolBehaviour
    {
        public new IGameObjectBuilder<TItem> Builder { get; }
        public bool DestroyOnRemove { get; set; } = true;

        public GameObjectPoolCollection(IGameObjectBuilder<TItem> builder, int capacity = 4) : base(builder, capacity)
        {
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
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
