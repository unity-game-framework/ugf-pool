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

        public override bool Add(TItem item)
        {
            if (base.Add(item))
            {
                if (item.IsPoolEnabled())
                {
                    item.OnPoolDisable();   
                }

                return true;
            }

            return false;
        }

        public override TItem Enable()
        {
            TItem item = base.Enable();

            item.OnPoolEnable();

            return item;
        }

        public virtual TItem Enable(Vector3 position, Quaternion rotation)
        {
            TItem item = base.Enable();

            item.transform.SetPositionAndRotation(position, rotation);
            item.OnPoolEnable();

            return item;
        }

        public virtual TItem Enable(Transform parent, bool worldPositionStays = true)
        {
            TItem item = base.Enable();

            item.transform.SetParent(parent, worldPositionStays);
            item.OnPoolEnable();
            
            return item;
        }
        
        public virtual TItem Enable(Vector3 position, Quaternion rotation, Transform parent, bool worldPositionStays = true)
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));
            
            TItem item = base.Enable();
            Transform transform = item.transform;

            transform.SetPositionAndRotation(position, rotation);
            transform.SetParent(parent, worldPositionStays);
            item.OnPoolEnable();

            return item;
        }
         
        public override bool Disable(TItem item)
        {
            if (base.Disable(item))
            {
                item.OnPoolDisable();

                return true;
            }

            return false;
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