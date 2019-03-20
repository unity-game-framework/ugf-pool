using System;
using UnityEngine;

namespace UGF.Pool.Runtime.GameObjects
{
    /// <summary>
    /// Represents the pool object component behaviour to work with gameobject pools.
    /// </summary>
    public class GameObjectPoolBehaviour : MonoBehaviour, IPoolObject
    {
        public void PoolEnable()
        {
            if (IsPoolEnabled()) throw new InvalidOperationException("The pool item already enabled.");

            OnPoolEnable();
        }

        public void PoolDisable()
        {
            if (!IsPoolEnabled()) throw new InvalidOperationException("The pool item already disabled.");

            OnPoolDisable();
        }

        /// <summary>
        /// Determines whether this gameobject is enabled.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsPoolEnabled()
        {
            return gameObject.activeSelf;
        }

        /// <summary>
        /// Occurs when pool enabled.
        /// </summary>
        protected virtual void OnPoolEnable()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Occurs then pool disabled.
        /// </summary>
        protected virtual void OnPoolDisable()
        {
            gameObject.SetActive(false);
        }
    }
}