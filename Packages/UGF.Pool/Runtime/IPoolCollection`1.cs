using System.Collections.Generic;

namespace UGF.Pool.Runtime
{
    /// <summary>
    /// Represents collection of the pooled items.
    /// </summary>
    public interface IPoolCollection<TItem> : IPoolCollection, IEnumerable<TItem>
    {
        /// <summary>
        /// Gets the enumerable of the enabled items in collection.
        /// </summary>
        IEnumerable<TItem> Enabled { get; }
        
        /// <summary>
        /// Gets the enumerable of the disabled items in collection.
        /// </summary>
        IEnumerable<TItem> Disabled { get; }
        
        /// <summary>
        /// Gets the comparer of the items in collection.
        /// </summary>
        IEqualityComparer<TItem> Comparer { get; }

        /// <summary>
        /// Determines whether collection contains the specified item.
        /// </summary>
        /// <param name="item">The pool item to check.</param>
        bool Contains(TItem item);
        
        /// <summary>
        /// Determines whether the specified item is enabled.
        /// <para>
        /// Will throw exception if the specified item does not from this collection.
        /// </para>
        /// </summary>
        /// <param name="item">The pool item to check.</param>
        bool IsEnabled(TItem item);
        
        /// <summary>
        /// Determines whether the specified item is disabled.
        /// <para>
        /// Will throw exception if the specified item does not from this collection.
        /// </para>
        /// </summary>
        /// <param name="item">The pool item to check.</param>
        bool IsDisabled(TItem item);
        
        /// <summary>
        /// Adds the specified item to collection, if not presents already.
        /// </summary>
        /// <param name="item">The pool item to add.</param>
        bool Add(TItem item);
        
        /// <summary>
        /// Removes one disabled item from collection and returns it.
        /// </summary>
        TItem Remove();
        
        /// <summary>
        /// Removes all disabled items from collection.
        /// </summary>
        void RemoveAll();
        
        /// <summary>
        /// Enables available disabled item and returns it.
        /// </summary>
        TItem Enable();
        
        /// <summary>
        /// Disable the specified item.
        /// </summary>
        /// <param name="item">The pool item to disabled.</param>
        bool Disable(TItem item);
        
        /// <summary>
        /// Disable all enabled items in collection.
        /// </summary>
        void DisableAll();
        
        /// <summary>
        /// Clears collection from all items.
        /// </summary>
        void Clear();
    }
}