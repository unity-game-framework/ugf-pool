using System;
using UGF.RuntimeTools.Runtime.Contexts;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.Pool.Runtime.Unity
{
    public class PoolCollectionDynamicScriptableObject<TScriptableObject> : PoolCollectionDynamic<TScriptableObject> where TScriptableObject : ScriptableObject
    {
        public Type Type { get; }

        public PoolCollectionDynamicScriptableObject(IContext context, int capacity = 4) : this(typeof(TScriptableObject), context, capacity)
        {
        }

        public PoolCollectionDynamicScriptableObject(Type type, IContext context, int capacity = 4) : base(context, capacity)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        protected override TScriptableObject OnBuild()
        {
            return (TScriptableObject)ScriptableObject.CreateInstance(Type);
        }

        protected override void OnDestroy(TScriptableObject item)
        {
            Object.Destroy(item);
        }
    }
}
