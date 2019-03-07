using UnityEngine;

namespace UGF.Pool.Runtime.GameObjects
{
    public class GameObjectPoolBehaviour : MonoBehaviour
    {
        public virtual void OnPoolEnable()
        {
            gameObject.SetActive(true);
        }

        public virtual void OnPoolDisable()
        {
            gameObject.SetActive(false);
        }
    }
}