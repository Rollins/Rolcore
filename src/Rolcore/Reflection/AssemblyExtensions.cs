//-----------------------------------------------------------------------
// <copyright file="AssemblyExtensions.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Extension methods for <see cref="Assembly"/>.
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Gets a list of <see cref="Type"/>s decorated with the given attribute.
        /// </summary>
        /// <param name="assembly">Specifies the <see cref="Assembly"/> to search.</param>
        /// <param name="attributeType">Specifies the <see cref="Type"/> of <see cref="Attribute"/>
        /// to search for.</param>
        /// <param name="inherit">Specifies if inheritance chains should be searched.</param>
        /// <returns>An enumerable list of types decorated with the specified attribute.</returns>
        public static IEnumerable<Type> GetTypesWithAttribute(this Assembly assembly, Type attributeType, bool inherit)
        {
            //
            // Pre-conditions

            if (assembly == null)
                throw new ArgumentNullException("assembly", "assembly is null.");
            if (attributeType == null)
                throw new ArgumentNullException("attributeType", "attributeType is null.");
            if (!attributeType.IsSubclassOf(typeof(Attribute)))
                throw new ArgumentNullException("attributeType is not an attribute.");

            //
            // Sift through all types in assembly

            List<Type> result = assembly.GetTypes().ToList();
            for (var i = result.Count - 1; i >= 0; i--)
            {
                //
                // Check type for attribute

                MemberInfo typeMemberInfo = result[i];
                var attributes = typeMemberInfo.GetCustomAttributes(attributeType, inherit);

                //
                // If type has attribute, yield the result

                if (attributes.Any(attribute => attribute.GetType() == attributeType))
                    yield return result[i];
            }
        }
    }
}
