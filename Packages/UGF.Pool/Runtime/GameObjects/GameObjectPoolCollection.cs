using System.Collections.Generic;
using UGF.Builder.Runtime;
using UnityEngine.SceneManagement;

namespace UGF.Pool.Runtime.GameObjects
{
    public class GameObjectPoolCollection<TItem> : PoolCollectionDynamic<TItem> where TItem : GameObjectPoolBehaviour
    {
        public Scene Scene { get; }

        public GameObjectPoolCollection(Scene scene, IBuilder<TItem> builder, IEqualityComparer<TItem> comparer = null) : base(builder, comparer)
        {
            Scene = scene;
        }

        public GameObjectPoolCollection(Scene scene, IBuilder<TItem> builder, ICollection<TItem> collection, IEqualityComparer<TItem> comparer = null) : base(builder, collection, comparer)
        {
            Scene = scene;
        }
    }
}