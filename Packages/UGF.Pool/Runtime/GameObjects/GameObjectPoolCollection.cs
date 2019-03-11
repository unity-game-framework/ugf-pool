using System;
using System.Collections.Generic;
using UGF.Builder.Runtime.GameObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGF.Pool.Runtime.GameObjects
{
    public class GameObjectPoolCollection<TItem> : PoolCollectionDynamic<TItem> where TItem : GameObjectPoolBehaviour
    {
        public Scene Scene { get; }
        public new GameObjectBuilder<TItem> Builder { get; }

        public GameObjectPoolCollection(Scene scene, GameObjectBuilder<TItem> builder, IEqualityComparer<TItem> comparer = null) : base(builder, comparer)
        {
            if (!scene.IsValid()) throw new ArgumentException("The specified scene not valid.", nameof(scene));

            Scene = scene;
            Builder = builder;
        }

        public GameObjectPoolCollection(Scene scene, GameObjectBuilder<TItem> builder, ICollection<TItem> collection, IEqualityComparer<TItem> comparer = null) : base(builder, collection, comparer)
        {
            if (!scene.IsValid()) throw new ArgumentException("The specified scene not valid.", nameof(scene));

            Scene = scene;
            Builder = builder;
        }

        public override bool Add(TItem item)
        {
            if (base.Add(item))
            {
                item.OnPoolDisable();
                
                return true;
            }

            return false;
        }

        public override TItem Remove()
        {
            TItem item = base.Remove();

            item.OnPoolDisable();
            
            return item;
        }

        public override TItem Enable()
        {
            TItem item = base.Enable();

            item.OnPoolEnable();

            return item;
        }

        public virtual TItem Enable(Vector3 position, Quaternion rotation, Transform parent)
        {
            TItem item = base.Enable();
            Transform transform = item.transform;

            transform.SetPositionAndRotation(position, rotation);
            transform.SetParent(parent);
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

        protected override TItem OnBuild()
        {
            TItem item = base.OnBuild();

            SceneManager.MoveGameObjectToScene(item.gameObject, Scene);
            
            return item;
        }
    }
}