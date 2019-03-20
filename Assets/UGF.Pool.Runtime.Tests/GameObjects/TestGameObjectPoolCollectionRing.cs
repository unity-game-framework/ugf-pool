using System.Collections;
using NUnit.Framework;
using UGF.Pool.Runtime.GameObjects;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

namespace UGF.Pool.Runtime.Tests.GameObjects
{
    public class TestGameObjectPoolCollectionRing
    {
        [Test]
        public void EnableWithPositionAndRotation()
        {
            var source = new GameObject().AddComponent<GameObjectPoolBehaviour>();
            var pool = new GameObjectPoolCollectionRing<GameObjectPoolBehaviour>();

            source.PoolDisable();
            pool.Add(source);

            GameObjectPoolBehaviour behaviour = pool.Enable(Vector3.one, Quaternion.Euler(Vector3.one));

            behaviour.PoolEnable();

            Assert.NotNull(behaviour);
            Assert.True(behaviour.IsPoolEnabled());
            Assert.AreEqual(Vector3.one, behaviour.transform.position);
            Assert.True(QuaternionEqualityComparer.Instance.Equals(Quaternion.Euler(Vector3.one), behaviour.transform.rotation));
        }

        [Test]
        public void EnableWithParent()
        {
            var source = new GameObject().AddComponent<GameObjectPoolBehaviour>();
            var pool = new GameObjectPoolCollectionRing<GameObjectPoolBehaviour>();

            source.PoolDisable();
            pool.Add(source);

            Transform parent = new GameObject().transform;

            GameObjectPoolBehaviour behaviour = pool.Enable(parent);

            behaviour.PoolEnable();

            Assert.NotNull(behaviour);
            Assert.True(behaviour.IsPoolEnabled());
            Assert.AreEqual(parent, behaviour.transform.parent);
        }

        [Test]
        public void EnableWithPositionAndRotationAndParent()
        {
            var source = new GameObject().AddComponent<GameObjectPoolBehaviour>();
            var pool = new GameObjectPoolCollectionRing<GameObjectPoolBehaviour>();

            source.PoolDisable();
            pool.Add(source);
            
            Transform parent = new GameObject().transform;

            GameObjectPoolBehaviour behaviour = pool.Enable(Vector3.one, Quaternion.Euler(Vector3.one), parent);

            behaviour.PoolEnable();

            Assert.NotNull(behaviour);
            Assert.True(behaviour.IsPoolEnabled());
            Assert.AreEqual(Vector3.one, behaviour.transform.position);
            Assert.True(QuaternionEqualityComparer.Instance.Equals(Quaternion.Euler(Vector3.one), behaviour.transform.rotation));
            Assert.AreEqual(parent, behaviour.transform.parent);
        }

        [Test]
        public void Disable()
        {
            var source = new GameObject().AddComponent<GameObjectPoolBehaviour>();
            var pool = new GameObjectPoolCollectionRing<GameObjectPoolBehaviour>();

            source.PoolDisable();
            pool.Add(source);
            
            GameObjectPoolBehaviour behaviour = pool.Enable();

            behaviour.PoolEnable();

            Assert.NotNull(behaviour);
            Assert.True(behaviour.IsPoolEnabled());

            bool result0 = pool.Disable(behaviour);

            behaviour.PoolDisable();

            Assert.True(result0);
            Assert.False(behaviour.IsPoolEnabled());
        }

        [UnityTest]
        public IEnumerator DestroyAll()
        {
            var pool = new GameObjectPoolCollectionRing<GameObjectPoolBehaviour>();
            Transform parent = new GameObject().transform;

            for (int i = 0; i < 10; i++)
            {
                pool.Add(new GameObject().AddComponent<GameObjectPoolBehaviour>());
            }

            for (int i = 0; i < 10; i++)
            {
                pool.Enable(parent);
            }

            Assert.AreEqual(10, pool.Count);
            Assert.AreEqual(10, parent.childCount);

            pool.DisableAll();
            pool.DestroyAll();

            yield return null;

            Assert.AreEqual(0, pool.Count);
            Assert.AreEqual(0, parent.childCount);
        }
    }
}