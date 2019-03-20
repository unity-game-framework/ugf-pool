using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.Pool.Runtime.GameObjects
{
    /// <summary>
    /// The ring implementation of the <see cref="IPoolCollection{TItem}"/> with ring reuse of the enabled gameobjects.
    /// </summary>
    public class GameObjectPoolCollectionRing<TItem> : PoolCollectionRing<TItem> where TItem : GameObjectPoolBehaviour
    {
        /// <summary>
        /// Creates pool collection with the specified comparer, if presents.
        /// </summary>
        /// <param name="comparer">The equality comparer for items.</param>
        public GameObjectPoolCollectionRing(IEqualityComparer<TItem> comparer = null) : base(comparer)
        {
        }

        /// <summary>
        /// Creates pool collection from the specified collection and with specified comparer, if presents.
        /// </summary>
        /// <param name="collection">The collection of the items.</param>
        /// <param name="comparer">The equality comparer for items.</param>
        public GameObjectPoolCollectionRing(ICollection<TItem> collection, IEqualityComparer<TItem> comparer = null) : base(collection, comparer)
        {
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
    }
}