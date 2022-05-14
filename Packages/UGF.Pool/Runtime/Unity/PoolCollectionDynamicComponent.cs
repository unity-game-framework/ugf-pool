using System;
using UGF.RuntimeTools.Runtime.Contexts;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.Pool.Runtime.Unity
{
    public class PoolCollectionDynamicComponent<TComponent> : PoolCollectionDynamic<TComponent> where TComponent : Component
    {
        public TComponent Source { get; }

        public PoolCollectionDynamicComponent(TComponent source, IContext context, int capacity = 4) : base(context, capacity)
        {
            Source = source ? source : throw new ArgumentNullException(nameof(source));
        }

        protected override TComponent OnBuild()
        {
            return Object.Instantiate(Source);
        }

        protected override void OnDestroy(TComponent item)
        {
            Object.Destroy(item.gameObject);
        }
    }
}
