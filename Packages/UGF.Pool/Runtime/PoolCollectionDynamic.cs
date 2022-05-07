using System;
using UGF.Builder.Runtime;

namespace UGF.Pool.Runtime
{
    public class PoolCollectionDynamic<TItem> : PoolCollection<TItem> where TItem : class
    {
        public IBuilder<TItem> Builder { get; }
        public int DefaultCount { get; set; } = 4;
        public bool ExpandAuto { get; set; } = true;
        public int ExpandCount { get; set; } = 3;
        public int ExpandThreshold { get; set; } = 1;
        public bool TrimAuto { get; set; } = true;
        public int TrimCount { get; set; } = 4;
        public int TrimThreshold { get; set; } = 8;

        public PoolCollectionDynamic(IBuilder<TItem> builder, int capacity = 4) : base(capacity)
        {
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        protected override void OnEnabled(TItem item)
        {
            base.OnEnabled(item);

            if (ExpandAuto && IsExpandRequired())
            {
                Expand();
            }
        }

        protected override void OnDisabled(TItem item)
        {
            base.OnDisabled(item);

            if (TrimAuto && IsTrimRequired())
            {
                Trim();
            }
        }

        public bool IsExpandRequired()
        {
            return Count < DefaultCount || DisabledCount == ExpandThreshold;
        }

        public bool IsTrimRequired()
        {
            return Count > DefaultCount && DisabledCount > TrimThreshold;
        }

        public void Expand()
        {
            Expand(ExpandCount);
        }

        public void Expand(int count)
        {
            for (int i = 0; i < count; i++)
            {
                TItem item = Builder.Build();

                Add(item);
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
                TItem item = GetAnyDisabled();

                Remove(item);
            }
        }
    }
}
