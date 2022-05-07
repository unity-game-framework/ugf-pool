using System.Collections;
using NUnit.Framework;
using UGF.Pool.Runtime.GameObjects;
using UnityEngine;
using UnityEngine.TestTools;

namespace UGF.Pool.Runtime.Tests.GameObjects
{
    public class TestGameObjectPoolCollectionCycle
    {
        [Test]
        public void Enable()
        {
            var source = new GameObject().AddComponent<GameObjectPoolBehaviour>();
            var pool = new GameObjectPoolCollectionCycle<GameObjectPoolBehaviour>();

            source.PoolDisable();
            pool.Add(source);

            GameObjectPoolBehaviour behaviour = pool.Enable();

            Assert.NotNull(behaviour);
            Assert.True(behaviour.gameObject.activeSelf);
        }

        [Test]
        public void Disable()
        {
            var source = new GameObject().AddComponent<GameObjectPoolBehaviour>();
            var pool = new GameObjectPoolCollectionCycle<GameObjectPoolBehaviour>();

            source.PoolDisable();
            pool.Add(source);

            GameObjectPoolBehaviour behaviour = pool.Enable();

            Assert.NotNull(behaviour);
            Assert.True(behaviour.gameObject.activeSelf);

            bool result0 = pool.Disable(behaviour);

            Assert.True(result0);
            Assert.False(behaviour.gameObject.activeSelf);
        }

        [UnityTest]
        public IEnumerator DestroyAll()
        {
            var pool = new GameObjectPoolCollectionCycle<GameObjectPoolBehaviour>();

            for (int i = 0; i < 10; i++)
            {
                var behaviour = new GameObject().AddComponent<GameObjectPoolBehaviour>();

                behaviour.PoolDisable();

                pool.Add(behaviour);
            }

            for (int i = 0; i < 10; i++)
            {
                pool.Enable();
            }

            Assert.AreEqual(10, pool.Count);

            pool.DisableAll();
            pool.Clear();

            yield return null;

            Assert.AreEqual(0, pool.Count);
        }
    }
}
