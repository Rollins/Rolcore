//-----------------------------------------------------------------------
// <copyright file="NameValueCollectionEx.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Collections.Specialized
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Diagnostics.Contracts;
    using System.Runtime.Serialization;

    /// <summary>
    /// Extends <see cref="NameValueCollection"/> to provide a read-only option.
    /// </summary>
    [Serializable]
    public class NameValueCollectionEx : NameValueCollection
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="NameValueCollectionEx"/> class.
        /// </summary>
        /// <param name="info">A System.Runtime.Serialization.SerializationInfo object that 
        /// contains the information required to serialize the new 
        /// <see cref="NameValueCollectionEx"/> instance.</param>
        /// <param name="context">A <see cref="StreamingContext"/> object that contains the source 
        /// and destination of the serialized stream associated with the new 
        /// <see cref="NameValueCollectionEx"/> instance.</param>
        protected NameValueCollectionEx(SerializationInfo info, StreamingContext context) 
            : base(info, context) 
        { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NameValueCollectionEx"/> class.
        /// </summary>
        public NameValueCollectionEx()
        {
        }

        /// <summary>
        /// Copies the entries from the specified <see cref="NameValueCollection"/> to a new 
        /// <see cref="NameValueCollectionEx"/> with the same initial capacity as the number of 
        /// entries copied and using the same hash code provider and the same comparer as the 
        /// source collection.
        /// </summary>
        /// <param name="col">The System.Collections.Specialized.NameValueCollection to copy to the
        /// new <see cref="NameValueCollectionEx"/> instance.</param>
        public NameValueCollectionEx(NameValueCollection col) 
            : base(col)
        {
            Contract.Requires<ArgumentNullException>(col != null, "col is null");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NameValueCollectionEx"/> class that is 
        /// empty, has the specified initial capacity and uses the default case-insensitive hash 
        /// code provider and the default case-insensitive comparer.
        /// </summary>
        /// <param name="capacity">The initial number of entries that the 
        /// <see cref="NameValueCollectionEx"/> can contain.</param>
        public NameValueCollectionEx(int capacity) : base(capacity)
        {
            Contract.Requires<ArgumentOutOfRangeException>(capacity >= 0, "capacity is less than zero");
        }

        public NameValueCollectionEx(IEqualityComparer equalityComparer) : base(equalityComparer)
        {
        }

        public NameValueCollectionEx(int capacity, IEqualityComparer equalityComparer) : base(capacity, equalityComparer)
        {
            Contract.Requires<ArgumentOutOfRangeException>(capacity >= 0, "capacity is less than zero");
        }

        public NameValueCollectionEx(int capacity, NameValueCollection col) : base(capacity, col)
        {
            Contract.Requires<ArgumentOutOfRangeException>(capacity >= 0, "capacity is less than zero");
            Contract.Requires<ArgumentNullException>(col != null, "col is null");
        }
        #endregion Constructors

        /// <summary>
        /// Creates a copy of the current instance that is read-only.
        /// <seealso cref="IsReadOnly"/>
        /// </summary>
        /// <returns>A read-only instance.</returns>
        public NameValueCollectionEx ToReadOnly()
        {
            NameValueCollectionEx result = new NameValueCollectionEx();
            result.Add(this);
            result.IsReadOnly = true;
            return result;
        }
    }
}
