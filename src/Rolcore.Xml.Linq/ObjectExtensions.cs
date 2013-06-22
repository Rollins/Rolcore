//-----------------------------------------------------------------------
// <copyright file="ObjectExtensions.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Xml.Linq
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Xml.Linq;

    /// <summary>
    /// Extension methods for <see cref="object"/>.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Returns an <see cref="XElement"/> representing the specified object.
        /// </summary>
        /// <param name="o">Specifies the object to convert.</param>
        public static XElement ToXElement(this object o)
        {
            Contract.Requires<ArgumentNullException>(o != null, "o is null");
            return XElement.Parse(o.ToXmlString());
        } // TODO: Test
    }
}
