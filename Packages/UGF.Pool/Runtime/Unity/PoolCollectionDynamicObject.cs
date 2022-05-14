using System;
using UGF.RuntimeTools.Runtime.Contexts;
using Object = UnityEngine.Object;

namespace UGF.Pool.Runtime.Unity
{
    public class PoolCollectionDynamicObject<TObject> : PoolCollectionDynamic<TObject> where TObject : Object
    {
        public TObject Source { get; }

        public PoolCollectionDynamicObject(TObject source, IContext context, int capacity = 4) : base(context, capacity)
        {
            Source = source ? source : throw new ArgumentNullException(nameof(source));
        }

        protected override TObject OnBuild()
        {
            return Object.Instantiate(Source);
        }

        protected override void OnDestroy(TObject item)
        {
            Object.Destroy(item);
        }
    }
}
