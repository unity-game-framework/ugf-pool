using System.Collections.Generic;
using NUnit.Framework;

namespace UGF.Pool.Runtime.Tests
{
    public class TestPoolCollectionDynamic
    {
        private class Target
        {
        }

        [Test]
        public void Enable()
        {
            var pool = new PoolCollectionDynamicHandlers<Target>(_ => new Target());

            Target target = pool.Enable();

            Assert.NotNull(target);
            Assert.AreEqual(4, pool.Count);
            Assert.AreEqual(1, pool.EnabledCount);
            Assert.AreEqual(3, pool.DisabledCount);
        }

        [Test]
        public void Disable()
        {
            var pool = new PoolCollectionDynamicHandlers<Target>(_ => new Target());
            var items = new List<Target>();

            for (int i = 0; i < 5; i++)
            {
                items.Add(pool.Enable());
            }

            Assert.AreEqual(8, pool.Count);
            Assert.AreEqual(5, pool.EnabledCount);
            Assert.AreEqual(3, pool.DisabledCount);

            for (int i = 0; i < items.Count; i++)
            {
                pool.Disable(items[i]);
            }

            Assert.AreEqual(4, pool.Count);
            Assert.AreEqual(0, pool.EnabledCount);
            Assert.AreEqual(4, pool.DisabledCount);
        }

        [Test]
        public void DisableAll()
        {
            var pool = new PoolCollectionDynamicHandlers<Target>(_ => new Target());

            for (int i = 0; i < 5; i++)
            {
                pool.Enable();
            }

            Assert.AreEqual(8, pool.Count);
            Assert.AreEqual(5, pool.EnabledCount);
            Assert.AreEqual(3, pool.DisabledCount);

            pool.DisableAll();

            Assert.AreEqual(4, pool.Count);
            Assert.AreEqual(0, pool.EnabledCount);
            Assert.AreEqual(4, pool.DisabledCount);
        }

        [Test]
        public void IsExpandRequired()
        {
            var pool = new PoolCollectionDynamicHandlers<Target>(_ => new Target());

            for (int i = 0; i < 4; i++)
            {
                pool.Enable();
            }

            bool result0 = pool.IsExpandRequired();

            Assert.True(result0);
        }

        [Test]
        public void IsTrimRequired()
        {
            var pool = new PoolCollectionDynamicHandlers<Target>(_ => new Target());

            pool.Expand(10);

            bool result0 = pool.IsTrimRequired();

            Assert.True(result0);
            Assert.AreEqual(10, pool.Count);
            Assert.AreEqual(0, pool.EnabledCount);
            Assert.AreEqual(10, pool.DisabledCount);
        }

        [Test]
        public void Expand()
        {
            var pool = new PoolCollectionDynamicHandlers<Target>(_ => new Target());

            pool.Expand();

            Assert.AreEqual(4, pool.Count);
        }

        [Test]
        public void ExpandWithCount()
        {
            var pool = new PoolCollectionDynamicHandlers<Target>(_ => new Target());

            pool.Expand(8);

            Assert.AreEqual(8, pool.Count);
        }

        [Test]
        public void Trim()
        {
            var pool = new PoolCollectionDynamicHandlers<Target>(_ => new Target());

            pool.Expand(6);
            pool.Trim();

            Assert.AreEqual(4, pool.Count);
        }

        [Test]
        public void TrimWithCount()
        {
            var pool = new PoolCollectionDynamicHandlers<Target>(_ => new Target());

            pool.Expand(10);
            pool.Trim(4);

            Assert.AreEqual(6, pool.Count);
        }
    }
}
