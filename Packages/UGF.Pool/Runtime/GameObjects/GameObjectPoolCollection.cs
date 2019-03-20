using System;
using System.Collections.Generic;
using UGF.Builder.Runtime.GameObjects;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.Pool.Runtime.GameObjects
{
    /// <summary>
    /// The dynamic implementation of the <see cref="IPoolCollection{TItem}"/> with dynamic expanding and trimming with gameobjects.
    /// </summary>
    public class GameObjectPoolCollection<TItem> : PoolCollectionDynamic<TItem> where TItem : GameObjectPoolBehaviour
    {
        /// <summary>
        /// Gets the builder of the pool item.
        /// </summary>
        public new IGameObjectBuilder<TItem> Builder { get; }

        /// <summary>
        /// Creates the pool collection with the specified builder and comparer, if presents.
        /// </summary>
        /// <param name="builder">The builder of the items.</param>
        /// <param name="comparer">The equality comparer for items.</param>
        public GameObjectPoolCollection(IGameObjectBuilder<TItem> builder, IEqualityComparer<TItem> comparer = null) : base(builder, comparer)
        {
            Builder = builder;
        }

        /// <summary>
        /// Creates the pool collection with the specified builder and items from the specified collection, and comparer, if presents.
        /// </summary>
        /// <param name="builder">The builder of the items.</param>
        /// <param name="collection">The collection of the items.</param>
        /// <param name="comparer">The equality comparer for items.</param>
        public GameObjectPoolCollection(IGameObjectBuilder<TItem> builder, ICollection<TItem> collection, IEqualityComparer<TItem> comparer = null) : base(builder, collection, comparer)
        {
            Builder = builder;
        }

        /// <summary>
        /// Enables available disabled gameobject with the specified position and rotation, and returns it.
        /// </summary>
        /// <param name="position">The position of the enabled gameobject.</param>
        /// <param name="rotation">The rotation of the enabled gameobject.</param>
        public virtual TItem Enable(Vector3 position, Quaternion rotation)
        {
            TItem item = base.Enable();

            item.transform.SetPositionAndRotation(position, rotation);

            return item;
        }

        /// <summary>
        /// Enables available disabled gameobject with the specified parent and returns it.
        /// </summary>
        /// <param name="parent">The transform parent to attach.</param>
        /// <param name="worldPositionStays">The value determines whether to transform local positions to world space before attach to parent.</param>
        public virtual TItem Enable(Transform parent, bool worldPositionStays = true)
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));

            TItem item = base.Enable();

            item.transform.SetParent(parent, worldPositionStays);

            return item;
        }

        /// <summary>
        /// Enables available disabled gameobject with the specified position, rotation and parent, and returns it.
        /// </summary>
        /// <param name="position">The position of the enabled gameobject.</param>
        /// <param name="rotation">The rotation of the enabled gameobject.</param>
        /// <param name="parent">The transform parent to attach.</param>
        /// <param name="worldPositionStays">The value determines whether to transform local positions to world space before attach to parent.</param>
        public virtual TItem Enable(Vector3 position, Quaternion rotation, Transform parent, bool worldPositionStays = true)
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));

            TItem item = base.Enable();
            Transform transform = item.transform;

            transform.SetPositionAndRotation(position, rotation);
            transform.SetParent(parent, worldPositionStays);

            return item;
        }

        /// <summary>
        /// Removes and destroy all disabled gameobjects.
        /// </summary>
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