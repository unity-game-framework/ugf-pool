using System;
using System.Collections.Generic;
using UGF.Builder.Runtime;

namespace UGF.Pool.Runtime
{
    /// <summary>
    /// The dynamic implementation of the <see cref="IPoolCollection{TItem}"/> with dynamic expanding and trimming.
    /// </summary>
    public class PoolCollectionDynamic<TItem> : PoolCollection<TItem>
    {
        /// <summary>
        /// Gets the builder of the pool item.
        /// </summary>
        public IBuilder<TItem> Builder { get; }
        
        /// <summary>
        /// Gets or sets default count of items in collection.
        /// </summary>
        public int DefaultCount { get; set; } = 5;
        
        /// <summary>
        /// Gets or sets value that determines whether to automatically expand collection when its required.
        /// </summary>
        public bool AutoExpand { get; set; } = true;
        
        /// <summary>
        /// Gets or sets value that determines whether to automatically trim collection when its allowed.
        /// </summary>
        public bool AutoTrim { get; set; } = true;
        
        /// <summary>
        /// Gets or sets the default expand count.
        /// </summary>
        public int ExpandCount { get; set; } = 5;
        
        /// <summary>
        /// Gets or sets the default trim count.
        /// </summary>
        public int TrimCount { get; set; } = 5;
        
        /// <summary>
        /// Creates the pool collection with the specified builder and comparer, if presents.
        /// </summary>
        /// <param name="builder">The builder of the items.</param>
        /// <param name="comparer">The equality comparer for items.</param>
        public PoolCollectionDynamic(IBuilder<TItem> builder, IEqualityComparer<TItem> comparer = null) : base(comparer)
        {
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        /// <summary>
        /// Creates the pool collection with the specified builder and items from the specified collection, and comparer, if presents.
        /// </summary>
        /// <param name="builder">The builder of the items.</param>
        /// <param name="collection">The collection of the items.</param>
        /// <param name="comparer">The equality comparer for items.</param>
        public PoolCollectionDynamic(IBuilder<TItem> builder, ICollection<TItem> collection, IEqualityComparer<TItem> comparer = null) : base(collection, comparer)
        {
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        public override TItem Enable()
        {
            if (AutoExpand && IsExpandRequired())
            {
                Expand();
            }

            return base.Enable();
        }

        public override bool Disable(TItem item)
        {
            bool result = base.Disable(item);

            if (AutoTrim && IsTrimRequired())
            {
                Trim();
            }

            return result;
        }

        /// <summary>
        /// Determines whether expand of the collection is required.
        /// </summary>
        public virtual bool IsExpandRequired()
        {
            return Count < DefaultCount || DisabledCount == 0;
        }

        /// <summary>
        /// Determines whether trim of the collection is required.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsTrimRequired()
        {
            return Count > DefaultCount && DisabledCount > TrimCount;
        }

        /// <summary>
        /// Expands collection with default expand count.
        /// </summary>
        public void Expand()
        {
            Expand(ExpandCount);
        }

        /// <summary>
        /// Expands collection with the specified expand count.
        /// </summary>
        /// <param name="count">The count of items to add to collection.</param>
        public void Expand(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Add(OnExpandItem());
            }
        }

        /// <summary>
        /// Trims collection with default trim count.
        /// </summary>
        public void Trim()
        {
            if (Count > DefaultCount)
            {
                int count = Count - DefaultCount;
                int trim = count > TrimCount ? TrimCount : count;

                Trim(trim);
            }
        }

        /// <summary>
        /// Trims collection with the specified trim count.
        /// </summary>
        /// <param name="count">The count of items to remove from collection.</param>
        public void Trim(int count)
        {
            if (count > DisabledCount) throw new ArgumentException("The specified count to trim greater than disabled items.", nameof(count));

            for (int i = 0; i < count; i++)
            {
                OnTrimItem(Remove());
            }
        }

        /// <summary>
        /// Occurs when collection builds a new item to expand collection.
        /// </summary>
        protected virtual TItem OnExpandItem()
        {
            return Builder.Build();
        }

        /// <summary>
        /// Occurs when collection remove item to trim collection.
        /// </summary>
        /// <param name="item">The removed item from collection.</param>
        protected virtual void OnTrimItem(TItem item)
        {
        }
    }
}