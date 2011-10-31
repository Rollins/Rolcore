using System.Collections.Specialized;
using System;
using System.Collections;

namespace Rolcore.Collections.Specialized
{
    /// <summary>
    /// Extends <see cref="NameValueCollection"/> to provide a read-only option.
    /// </summary>
    public class NameValueCollectionEx : NameValueCollection
    {
        #region Constructors
        protected NameValueCollectionEx(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        public NameValueCollectionEx(){}
        public NameValueCollectionEx(NameValueCollection col) : base(col){}
        public NameValueCollectionEx(int capacity) : base(capacity){}
        public NameValueCollectionEx(System.Collections.IEqualityComparer equalityComparer) : base(equalityComparer){}
        public NameValueCollectionEx(int capacity, System.Collections.IEqualityComparer equalityComparer) : base(capacity, equalityComparer){}
        public NameValueCollectionEx(int capacity, NameValueCollection col) : base(capacity, col){}
                                                                                                   
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
