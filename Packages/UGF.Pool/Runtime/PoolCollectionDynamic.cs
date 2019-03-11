using System;
using System.Collections.Generic;
using UGF.Builder.Runtime;

namespace UGF.Pool.Runtime
{
    public class PoolCollectionDynamic<TItem> : PoolCollection<TItem>
    {
        public IBuilder<TItem> Builder { get; }
        public int DefaultCount { get; set; } = 5;
        public bool AutoExpand { get; set; } = true;
        public bool AutoTrim { get; set; } = true;
        public int ExpandCount { get; set; } = 5;
        public int TrimCount { get; set; } = 5;

        public PoolCollectionDynamic(IBuilder<TItem> builder, IEqualityComparer<TItem> comparer = null) : base(comparer)
        {
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

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

        public virtual bool IsExpandRequired()
        {
            return Count < DefaultCount || DisabledCount == 0;
        }

        public virtual bool IsTrimRequired()
        {
            return Count > DefaultCount && DisabledCount > TrimCount;
        }

        public void Expand()
        {
            Expand(ExpandCount);
        }

        public void Expand(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Add(OnBuild());
            }
        }

        public void Trim()
        {
            if (Count > DefaultCount)
            {
                int count = Count - DefaultCount;
                int trim = count > TrimCount ? TrimCount : count;
                
                Trim(trim);
            }
        }

        public void Trim(int count)
        {
            if (count > DisabledCount) throw new ArgumentException("The specified count to trim greater than disabled items.", nameof(count));
            
            for (int i = 0; i < count; i++)
            {
                Remove();
            }
        }

        protected virtual TItem OnBuild()
        {
            return Builder.Build();
        }
    }
}