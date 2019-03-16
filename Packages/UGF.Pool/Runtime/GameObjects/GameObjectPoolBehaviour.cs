using System;
using UnityEngine;

namespace UGF.Pool.Runtime.GameObjects
{
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

        public virtual bool IsPoolEnabled()
        {
            return gameObject.activeSelf;
        }

        protected virtual void OnPoolEnable()
        {
            gameObject.SetActive(true);
        }

        protected virtual void OnPoolDisable()
        {
            gameObject.SetActive(false);
        }
    }
}