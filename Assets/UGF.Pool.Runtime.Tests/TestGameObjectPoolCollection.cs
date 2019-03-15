using System.Collections;
using NUnit.Framework;
using UGF.Builder.Runtime.GameObjects;
using UGF.Pool.Runtime.GameObjects;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

namespace UGF.Pool.Runtime.Tests
{
    public class TestGameObjectPoolCollection
    {
        [Test]
        public void Add()
        {
            var builder = new GameObjectBuilder<GameObjectPoolBehaviour>(new GameObject().AddComponent<GameObjectPoolBehaviour>());
            var pool = new GameObjectPoolCollection<GameObjectPoolBehaviour>(builder);
            var behaviour = new GameObject().AddComponent<GameObjectPoolBehaviour>();

            Assert.True(behaviour.IsPoolEnabled());

            bool result0 = pool.Add(behaviour);

            Assert.True(result0);
            Assert.False(behaviour.IsPoolEnabled());
        }

        [Test]
        public void Enable()
        {
            var builder = new GameObjectBuilder<GameObjectPoolBehaviour>(new GameObject().AddComponent<GameObjectPoolBehaviour>());
            var pool = new GameObjectPoolCollection<GameObjectPoolBehaviour>(builder);

            GameObjectPoolBehaviour behaviour = pool.Enable();

            Assert.NotNull(behaviour);
            Assert.True(behaviour.IsPoolEnabled());
        }

        [Test]
        public void EnableWithPositionAndRotation()
        {
            var builder = new GameObjectBuilder<GameObjectPoolBehaviour>(new GameObject().AddComponent<GameObjectPoolBehaviour>());
            var pool = new GameObjectPoolCollection<GameObjectPoolBehaviour>(builder);

            GameObjectPoolBehaviour behaviour = pool.Enable(Vector3.one, Quaternion.Euler(Vector3.one));

            Assert.NotNull(behaviour);
            Assert.True(behaviour.IsPoolEnabled());
            Assert.AreEqual(Vector3.one, behaviour.transform.position);
            Assert.True(QuaternionEqualityComparer.Instance.Equals(Quaternion.Euler(Vector3.one), behaviour.transform.rotation));
        }

        [Test]
        public void EnableWithParent()
        {
            var builder = new GameObjectBuilder<GameObjectPoolBehaviour>(new GameObject().AddComponent<GameObjectPoolBehaviour>());
            var pool = new GameObjectPoolCollection<GameObjectPoolBehaviour>(builder);
            Transform parent = new GameObject().transform;

            GameObjectPoolBehaviour behaviour = pool.Enable(parent);

            Assert.NotNull(behaviour);
            Assert.True(behaviour.IsPoolEnabled());
            Assert.AreEqual(parent, behaviour.transform.parent);
        }

        [Test]
        public void EnableWithPositionAndRotationAndParent()
        {
            var builder = new GameObjectBuilder<GameObjectPoolBehaviour>(new GameObject().AddComponent<GameObjectPoolBehaviour>());
            var pool = new GameObjectPoolCollection<GameObjectPoolBehaviour>(builder);
            Transform parent = new GameObject().transform;

            GameObjectPoolBehaviour behaviour = pool.Enable(Vector3.one, Quaternion.Euler(Vector3.one), parent);

            Assert.NotNull(behaviour);
            Assert.True(behaviour.IsPoolEnabled());
            Assert.AreEqual(Vector3.one, behaviour.transform.position);
            Assert.True(QuaternionEqualityComparer.Instance.Equals(Quaternion.Euler(Vector3.one), behaviour.transform.rotation));
            Assert.AreEqual(parent, behaviour.transform.parent);
        }

        [Test]
        public void Disable()
        {
            var builder = new GameObjectBuilder<GameObjectPoolBehaviour>(new GameObject().AddComponent<GameObjectPoolBehaviour>());
            var pool = new GameObjectPoolCollection<GameObjectPoolBehaviour>(builder);

            GameObjectPoolBehaviour behaviour = pool.Enable();

            Assert.NotNull(behaviour);
            Assert.True(behaviour.IsPoolEnabled());

            bool result0 = pool.Disable(behaviour);

            Assert.True(result0);
            Assert.False(behaviour.IsPoolEnabled());
        }

        [UnityTest]
        public IEnumerator DestroyAll()
        {
            var builder = new GameObjectBuilder<GameObjectPoolBehaviour>(new GameObject().AddComponent<GameObjectPoolBehaviour>());
            var pool = new GameObjectPoolCollection<GameObjectPoolBehaviour>(builder);
            Transform parent = new GameObject().transform;

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