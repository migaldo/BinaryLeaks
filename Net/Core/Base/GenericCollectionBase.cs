using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace Primavera.Platform.CloudServices900.Base
{
    /// <summary>
    /// An abstract base class used to create strongly typed collections.
    /// </summary>
    /// <typeparam name="T">The collection type.</typeparam>
    public abstract class CollectionBase<T> :
        ICollection<T>, ICollection
    {
        /* Sample Class Usage:

        public class FooCollection
            : CollectionBase<Foo>
        {
            public void Add(Foo item)
            {
                this.InnerCollection.Add(item);
            }

            protected override ICollection<Foo> CreateInnerCollection()
            {
                return new List<Foo>();
            }
        }
        
        */

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionBase&lt;T&gt;"/> class.
        /// </summary>
        protected CollectionBase()
        {
            this.InnerCollection = this.CreateInnerCollection();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionBase&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="innerCollection">The inner collection.</param>
        /// <remarks>
        /// Creates a new instance and sets the inner collection to the supplied
        /// collection.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown if the innerCollection parameter
        /// is null (Nothing in VB).</exception>
        protected CollectionBase(ICollection<T> innerCollection)
        {
            if (innerCollection == null)
            {
                throw new ArgumentNullException("innerCollection");
            }

            this.InnerCollection = innerCollection;
        }

        #endregion

        #region Properties

        #region InnerCollection

        [DebuggerBrowsable(DebuggerBrowsableState.Never), EditorBrowsable(EditorBrowsableState.Never)]
        private ICollection<T> internalCollection;

        /// <summary>
        /// Gets or sets the inner collection.
        /// </summary>
        /// <value>The inner collection.</value>
        protected ICollection<T> InnerCollection
        {
            [DebuggerStepThrough]
            get
            {
                return this.internalCollection; 
            }

            [DebuggerStepThrough]
            set 
            {
                this.internalCollection = value;
            }
        }
        #endregion InnerCollection

        #endregion

        #region Methods

        #region CreateInnerCollection
        /// <summary>
        /// When implemented by a sub class this method creates a collection
        /// that can be used as inner collection.
        /// </summary>
        /// <returns>A new <c>ICollection</c> instance.</returns>
        protected abstract ICollection<T> CreateInnerCollection();
        #endregion CreateInnerCollection

        #endregion Methods

        #region ICollection<T> Members

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item of T.</param>
        void ICollection<T>.Add(T item)
        {
            this.InnerCollection.Add(item);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        void ICollection<T>.Clear()
        {
            this.InnerCollection.Clear();
        }

        /// <summary>
        /// Determines whether [contains] [the specified item].
        /// </summary>
        /// <param name="item">The item of T.</param>
        /// <returns>
        /// Returns <c>true</c> if [contains] [the specified item]; otherwise, <c>false</c>.
        /// </returns>
        bool ICollection<T>.Contains(T item)
        {
            return this.InnerCollection.Contains(item);
        }

        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">Index of the array.</param>
        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            this.InnerCollection.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of items in the collection.
        /// </summary>
        public int Count
        {
            get { return this.InnerCollection.Count; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is read only.
        /// </summary>
        /// <value>
        /// Returns <c>true</c> if this instance is read only; otherwise, <c>false</c>.
        /// </value>
        bool ICollection<T>.IsReadOnly
        {
            get { return this.InnerCollection.IsReadOnly; }
        }

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item of T.</param>
        /// <returns>True if remove has success.</returns>
        bool ICollection<T>.Remove(T item)
        {
            return this.InnerCollection.Remove(item);
        }

        #endregion

        #region IEnumerable<T> Members

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this.InnerCollection.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.InnerCollection.GetEnumerator();
        }

        #endregion

        #region ICollection Members

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.ICollection"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection"/>. The <see cref="T:System.Array"/> must have zero-based indexing.</param>
        /// <param name="index">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="array"/> is null. 
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than zero. 
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="array"/> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.ICollection"/> is greater than the available space from <paramref name="index"/> to the end of the destination <paramref name="array"/>.-or-The type of the source <see cref="T:System.Collections.ICollection"/> cannot be cast automatically to the type of the destination <paramref name="array"/>.
        /// </exception>
        void ICollection.CopyTo(Array array, int index)
        {
            this.InnerCollection.CopyTo((T[])array, index);
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        int ICollection.Count
        {
            get { return this.InnerCollection.Count; }
        }

        /// <summary>
        /// Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection"/> is synchronized (thread safe).
        /// </summary>
        /// <value></value>
        /// <returns>true if access to the <see cref="T:System.Collections.ICollection"/> is synchronized (thread safe); otherwise, false.</returns>
        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

        /// <summary>
        /// Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"/>.
        /// </summary>
        /// <value></value>
        /// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"/>.</returns>
        object ICollection.SyncRoot
        {
            get { return this.InnerCollection; }
        }

        #endregion
    }
}
