using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace UGF.Pool.Runtime.Tests
{
    public class TestPoolCollection
    {
        private class Target
        {
        }
        
        [Test]
        public void Count()
        {
            var pool = new PoolCollection<Target>();

            pool.Add(new Target());
            
            Assert.AreEqual(1, pool.Count);
        }

        [Test]
        public void EnabledCount()
        {
            var pool = new PoolCollection<Target>();

            pool.Add(new Target());
            pool.Enable();
            
            Assert.AreEqual(1, pool.EnabledCount);
        }

        [Test]
        public void DisabledCount()
        {
            var pool = new PoolCollection<Target>();

            pool.Add(new Target());
            
            Assert.AreEqual(1, pool.DisabledCount);
        }

        [Test]
        public void Comparer()
        {
            EqualityComparer<Target> comparer = EqualityComparer<Target>.Default;
            var pool = new PoolCollection<Target>(comparer);
            
            Assert.AreSame(comparer, pool.Comparer);
        }

        [Test]
        public void Enabled()
        {
            var pool = new PoolCollection<Target>();

            pool.Add(new Target());
            pool.Enable();

            int count = pool.Enabled.Count();
            
            Assert.AreEqual(1, count);
        }

        [Test]
        public void Disabled()
        {
            var pool = new PoolCollection<Target>();

            pool.Add(new Target());

            int count = pool.Disabled.Count();
            
            Assert.AreEqual(1, count);
        }

        [Test]
        public void PoolCollection()
        {
        }

        [Test]
        public void PoolCollection2()
        {
            var collection = new List<Target> { new Target(), new Target() };
            EqualityComparer<Target> comparer = EqualityComparer<Target>.Default;
            var pool = new PoolCollection<Target>(collection);
            
            Assert.AreEqual(2, pool.Count);
            Assert.AreEqual(0, pool.EnabledCount);
            Assert.AreEqual(2, pool.DisabledCount);
            Assert.AreSame(comparer, pool.Comparer);
        }

        [Test]
        public void Contains()
        {
            var pool = new PoolCollection<Target>();
            var target = new Target();

            pool.Add(target);

            bool result0 = pool.Contains(target);
            bool result1 = pool.Contains(null);
            
            Assert.True(result0);
            Assert.False(result1);
        }

        [Test]
        public void IsEnabled()
        {
            var pool = new PoolCollection<Target>();
            var target = new Target();

            pool.Add(target);
            pool.Enable();

            bool result0 = pool.IsEnabled(target);
            
            Assert.True(result0);
        }

        [Test]
        public void IsDisabled()
        {
            var pool = new PoolCollection<Target>();
            var target = new Target();

            pool.Add(target);

            bool result0 = pool.IsDisabled(target);
            
            Assert.True(result0);
        }

        [Test]
        public void Add()
        {
            var pool = new PoolCollection<Target>();

            pool.Add(new Target());
            
            Assert.AreEqual(1, pool.Count);
        }

        [Test]
        public void Remove()
        {
            var pool = new PoolCollection<Target>();
            var target = new Target();

            pool.Add(target);
            
            Target result0 = pool.Remove();
            
            Assert.NotNull(result0);
            Assert.AreSame(result0, target);
            Assert.AreEqual(0, pool.Count);
        }

        [Test]
        public void RemoveAll()
        {
            var pool = new PoolCollection<Target>();

            for (int i = 0; i < 5; i++)
            {
                pool.Add(new Target());
            }

            pool.Enable();
            
            Assert.AreEqual(5, pool.Count);
            Assert.AreEqual(1, pool.EnabledCount);
            Assert.AreEqual(4, pool.DisabledCount);
            
            pool.RemoveAll();
            
            Assert.AreEqual(1, pool.Count);
            Assert.AreEqual(1, pool.EnabledCount);
            Assert.AreEqual(0, pool.DisabledCount);
        }

        [Test]
        public void Enable()
        {
            var pool = new PoolCollection<Target>();
            var target = new Target();

            pool.Add(target);

            Target result0 = pool.Enable();
            
            Assert.NotNull(result0);
            Assert.AreSame(result0, target);
            Assert.AreEqual(1, pool.Count);
            Assert.AreEqual(1, pool.EnabledCount);
            Assert.AreEqual(0, pool.DisabledCount);
        }

        [Test]
        public void Disable()
        {
            var pool = new PoolCollection<Target>();
            var target = new Target();

            pool.Add(target);
            pool.Enable();

            bool result0 = pool.Disable(target);
            
            Assert.True(result0);
            Assert.AreEqual(1, pool.Count);
            Assert.AreEqual(0, pool.EnabledCount);
            Assert.AreEqual(1, pool.DisabledCount);
        }

        [Test]
        public void DisableAll()
        {
            var pool = new PoolCollection<Target>();

            for (int i = 0; i < 5; i++)
            {
                pool.Add(new Target());
                pool.Enable();
            }

            Assert.AreEqual(5, pool.Count);
            Assert.AreEqual(5, pool.EnabledCount);
            Assert.AreEqual(0, pool.DisabledCount);
            
            pool.DisableAll();
            
            Assert.AreEqual(5, pool.Count);
            Assert.AreEqual(0, pool.EnabledCount);
            Assert.AreEqual(5, pool.DisabledCount);
        }
        
        [Test]
        public void Clear()
        {
            var pool = new PoolCollection<Target>();

            pool.Add(new Target());
            pool.Clear();
            
            Assert.AreEqual(0, pool.Count);
            Assert.AreEqual(0, pool.EnabledCount);
            Assert.AreEqual(0, pool.DisabledCount);
        }

        [Test]
        public void GetEnumerator()
        {
        }
    }
}