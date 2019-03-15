using System;
using UnityEngine;

namespace UGF.Pool.Runtime.GameObjects
{
    public class GameObjectPoolBehaviour : MonoBehaviour
    {
        public virtual bool IsPoolEnabled()
        {
            return gameObject.activeSelf;
        }

        public virtual void OnPoolEnable()
        {
            if (IsPoolEnabled()) throw new InvalidOperationException("The pool item already enabled.");

            gameObject.SetActive(true);
        }

        public virtual void OnPoolDisable()
        {
            if (!IsPoolEnabled()) throw new InvalidOperationException("The pool item already disabled.");

            gameObject.SetActive(false);
        }
    }
}