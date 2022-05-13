using System.Collections;
using NUnit.Framework;
using UGF.Pool.Runtime.GameObjects;
using UnityEngine;
using UnityEngine.TestTools;

namespace UGF.Pool.Runtime.Tests.GameObjects
{
    public class TestGameObjectPoolCollection
    {
        [Test]
        public void Enable()
        {
            var source = new GameObject().AddComponent<PoolComponent>();

            source.PoolDisable();

            var pool = new GameObjectPoolCollection<PoolComponent>(source);

            PoolComponent behaviour = pool.Enable();

            Assert.NotNull(behaviour);
            Assert.True(behaviour.gameObject.activeSelf);
        }

        [Test]
        public void Disable()
        {
            var source = new GameObject().AddComponent<PoolComponent>();

            source.PoolDisable();

            var pool = new GameObjectPoolCollection<PoolComponent>(source);

            PoolComponent behaviour = pool.Enable();

            Assert.NotNull(behaviour);
            Assert.True(behaviour.gameObject.activeSelf);

            bool result0 = pool.Disable(behaviour);

            Assert.True(result0);
            Assert.False(behaviour.gameObject.activeSelf);
        }

        [UnityTest]
        public IEnumerator DestroyAll()
        {
            var source = new GameObject().AddComponent<PoolComponent>();

            source.PoolDisable();

            var pool = new GameObjectPoolCollection<PoolComponent>(source);

            for (int i = 0; i < 10; i++)
            {
                pool.Enable();
            }

            Assert.AreEqual(12, pool.Count);

            pool.DisableAll();
            pool.Clear();

            yield return null;

            Assert.AreEqual(0, pool.Count);
        }
    }
}
