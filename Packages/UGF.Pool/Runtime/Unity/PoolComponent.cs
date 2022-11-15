using System;
using UGF.RuntimeTools.Runtime.Containers;
using UnityEngine;

namespace UGF.Pool.Runtime.Unity
{
    [AddComponentMenu("Unity Game Framework/Pool/Pool Component", 2000)]
    public class PoolComponent : MonoBehaviour, IPoolObject
    {
        [SerializeField] private ContainerComponent m_container;
        [SerializeField] private bool m_changeGameObjectActiveState = true;

        public ContainerComponent Container { get { return m_container; } set { m_container = value; } }
        public bool ChangeGameObjectActiveState { get { return m_changeGameObjectActiveState; } set { m_changeGameObjectActiveState = value; } }

        public event Action Enabled;
        public event Action Disabled;

        public void PoolEnable()
        {
            if (m_changeGameObjectActiveState)
            {
                if (gameObject.activeSelf) throw new InvalidOperationException("Pool object enabled already.");

                gameObject.SetActive(true);
            }

            Enabled?.Invoke();
        }

        public void PoolDisable()
        {
            if (m_changeGameObjectActiveState)
            {
                if (!gameObject.activeSelf) throw new InvalidOperationException("Pool object disabled already.");

                gameObject.SetActive(false);
            }

            Disabled?.Invoke();
        }
    }
}
