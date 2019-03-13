using System;
using System.Collections.Generic;
using NUnit.Framework;
using UGF.Builder.Runtime;

namespace UGF.Pool.Runtime.Tests
{
    public class TestPoolCollectionDynamic
    {
        private class Target
        {
        }
        
        private class TargetBuilder : BuilderBase<Target>
        {
            public override Target Build()
            {
                return new Target();
            }
        }
        
        [Test]
        public void Enable()
        {
            var pool = new PoolCollectionDynamic<Target>(new TargetBuilder());

            Target target = pool.Enable();
            
            Assert.NotNull(target);
            Assert.AreEqual(5, pool.Count);
            Assert.AreEqual(1, pool.EnabledCount);
            Assert.AreEqual(4, pool.DisabledCount);
        }

        [Test]
        public void Disable()
        {
            var pool = new PoolCollectionDynamic<Target>(new TargetBuilder());
            var items = new List<Target>();

            for (int i = 0; i < 6; i++)
            {
                items.Add(pool.Enable());
            }

            Assert.AreEqual(10, pool.Count);
            Assert.AreEqual(6, pool.EnabledCount);
            Assert.AreEqual(4, pool.DisabledCount);
            
            for (int i = 0; i < items.Count; i++)
            {
                pool.Disable(items[i]);
            }
            
            Assert.AreEqual(5, pool.Count);
            Assert.AreEqual(0, pool.EnabledCount);
            Assert.AreEqual(5, pool.DisabledCount);
        }

        [Test]
        public void DisableAll()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void IsExpandRequired()
        {
            var pool = new PoolCollectionDynamic<Target>(new TargetBuilder());

            for (int i = 0; i < 5; i++)
            {
                pool.Enable();
            }

            bool result0 = pool.IsExpandRequired();
            
            Assert.True(result0);
        }

        [Test]
        public void IsTrimRequired()
        {
            var pool = new PoolCollectionDynamic<Target>(new TargetBuilder());
            
            pool.Expand();
            pool.Add(new Target());
            
            bool result0 = pool.IsTrimRequired();
            
            Assert.True(result0);
            Assert.AreEqual(6, pool.Count);
            Assert.AreEqual(0, pool.EnabledCount);
            Assert.AreEqual(6, pool.DisabledCount);
        }

        [Test]
        public void Expand()
        {
            var pool = new PoolCollectionDynamic<Target>(new TargetBuilder());
            
            pool.Expand();
            
            Assert.AreEqual(5, pool.Count);
        }

        [Test]
        public void ExpandWithCount()
        {
            var pool = new PoolCollectionDynamic<Target>(new TargetBuilder());
            
            pool.Expand(8);
            
            Assert.AreEqual(8, pool.Count);
        }

        [Test]
        public void Trim()
        {
            var pool = new PoolCollectionDynamic<Target>(new TargetBuilder());
            
            pool.Expand(6);
            pool.Trim();
            
            Assert.AreEqual(5, pool.Count);
        }

        [Test]
        public void TrimWithCount()
        {
            var pool = new PoolCollectionDynamic<Target>(new TargetBuilder());
            
            pool.Expand(10);
            pool.Trim(4);
            
            Assert.AreEqual(6, pool.Count);
        }
    }
}