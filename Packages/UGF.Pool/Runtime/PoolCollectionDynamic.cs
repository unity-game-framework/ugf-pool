using System;
using UGF.RuntimeTools.Runtime.Contexts;

namespace UGF.Pool.Runtime
{
    public abstract class PoolCollectionDynamic<TItem> : PoolCollection<TItem> where TItem : class
    {
        public IContext Context { get; }
        public int DefaultCount { get; set; } = 4;
        public bool ExpandAuto { get; set; } = true;
        public int ExpandCount { get; set; } = 4;
        public int ExpandThreshold { get; set; } = 0;
        public bool TrimAuto { get; set; } = true;
        public int TrimCount { get; set; } = 4;
        public int TrimThreshold { get; set; } = 8;

        protected PoolCollectionDynamic(IContext context, int capacity = 4) : base(capacity)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public bool IsExpandRequired()
        {
            return Count < DefaultCount || DisabledCount <= ExpandThreshold;
        }

        public bool IsTrimRequired()
        {
            return Count > DefaultCount && DisabledCount >= TrimThreshold;
        }

        public void Expand()
        {
            Expand(ExpandCount);
        }

        public void Expand(int count)
        {
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

            for (int i = 0; i < count; i++)
            {
                TItem item = OnBuild();

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
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (count > DisabledCount) throw new ArgumentException("The specified count to trim greater than disabled items.", nameof(count));

            for (int i = 0; i < count; i++)
            {
                TItem item = GetAnyDisabled();

                Remove(item);

                OnDestroy(item);
            }
        }

        protected abstract TItem OnBuild();
        protected abstract void OnDestroy(TItem item);

        protected override TItem OnEnable()
        {
            if (ExpandAuto && IsExpandRequired())
            {
                Expand();
            }

            return base.OnEnable();
        }

        protected override void OnDisabled(TItem item)
        {
            base.OnDisabled(item);

            if (TrimAuto && IsTrimRequired())
            {
                Trim();
            }
        }
    }
}
