using NUnit.Framework;

namespace UGF.Pool.Runtime.Tests
{
    public class TestPoolCollectionCycle
    {
        public class Target
        {
            public int Value { get; }

            public Target(int value)
            {
                Value = value;
            }
        }

        [Test]
        public void Add()
        {
            var pool = new PoolCollectionCycle<Target>();
            var target = new Target(0);

            pool.Add(target);

            bool result0 = pool.Contains(target);
            bool result1 = pool.IsDisabled(target);

            Assert.True(result0);
            Assert.True(result1);
        }

        [Test]
        public void Remove()
        {
            var pool = new PoolCollectionCycle<Target>();
            var target = new Target(0);

            pool.Add(target);
            pool.Remove(target);

            bool result0 = pool.Contains(target);

            Assert.False(result0);
            Assert.AreEqual(0, pool.Count);
        }

        [Test]
        public void Enable()
        {
            var pool = new PoolCollectionCycle<Target>
            {
                new Target(0),
                new Target(1)
            };

            Target target0 = pool.Enable();
            Target target1 = pool.Enable();
            Target target2 = pool.Enable();

            Assert.AreEqual(1, target0.Value);
            Assert.AreEqual(0, target1.Value);
            Assert.AreEqual(1, target2.Value);
        }

        [Test]
        public void Disable()
        {
            var pool = new PoolCollectionCycle<Target>
            {
                new Target(0),
                new Target(1)
            };

            Target target = pool.Enable();

            pool.Disable(target);

            bool result0 = pool.IsDisabled(target);

            Assert.True(result0);
        }
    }
}
