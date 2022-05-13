using System;
using UnityEngine;

namespace UGF.Pool.Runtime
{
    public class PoolComponent : MonoBehaviour, IPoolObject
    {
        public event Action Enabled;
        public event Action Disabled;

        public void PoolEnable()
        {
            if (gameObject.activeSelf) throw new InvalidOperationException("Pool object enabled already.");

            gameObject.SetActive(true);

            Enabled?.Invoke();
        }

        public void PoolDisable()
        {
            if (!gameObject.activeSelf) throw new InvalidOperationException("Pool object disabled already.");

            gameObject.SetActive(false);

            Disabled?.Invoke();
        }
    }
}
