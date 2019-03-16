using System;
using System.Collections.Generic;
using UGF.Builder.Runtime.GameObjects;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.Pool.Runtime.GameObjects
{
    public class GameObjectPoolCollection<TItem> : PoolCollectionDynamic<TItem> where TItem : GameObjectPoolBehaviour
    {
        public new IGameObjectBuilder<TItem> Builder { get; }

        public GameObjectPoolCollection(IGameObjectBuilder<TItem> builder, IEqualityComparer<TItem> comparer = null) : base(builder, comparer)
        {
            Builder = builder;
        }

        public GameObjectPoolCollection(IGameObjectBuilder<TItem> builder, ICollection<TItem> collection, IEqualityComparer<TItem> comparer = null) : base(builder, collection, comparer)
        {
            Builder = builder;
        }

        public virtual TItem Enable(Vector3 position, Quaternion rotation)
        {
            TItem item = base.Enable();

            item.transform.SetPositionAndRotation(position, rotation);

            return item;
        }

        public virtual TItem Enable(Transform parent, bool worldPositionStays = true)
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));

            TItem item = base.Enable();

            item.transform.SetParent(parent, worldPositionStays);

            return item;
        }

        public virtual TItem Enable(Vector3 position, Quaternion rotation, Transform parent, bool worldPositionStays = true)
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));

            TItem item = base.Enable();
            Transform transform = item.transform;

            transform.SetPositionAndRotation(position, rotation);
            transform.SetParent(parent, worldPositionStays);

            return item;
        }

        public virtual void DestroyAll()
        {
            int count = DisabledCount;

            for (int i = 0; i < count; i++)
            {
                TItem item = Remove();

                Object.Destroy(item.gameObject);
            }
        }

        protected override void OnTrimItem(TItem item)
        {
            base.OnTrimItem(item);

            Object.Destroy(item.gameObject);
        }
    }
}