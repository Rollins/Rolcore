using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics.Contracts;

namespace Rolcore.Reflection
{
    /// <summary>
    /// Extension methods for <see cref="Assembly"/>.
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Gets a list of <see cref="Type"/>s decorated with the given attribute.
        /// </summary>
        /// <param name="assembly">Specifies the <see cref="Assembly"/> to search.</param>
        /// <param name="attributeType"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetTypesWithAttribute(this Assembly assembly, Type attributeType, bool inherit)
        {
            //
            // Pre-conditions

            Contract.Requires<ArgumentNullException>(assembly != null, "assembly is null.");
            Contract.Requires<ArgumentNullException>(attributeType != null, "attributeType is null.");
            Contract.Requires<ArgumentException>(attributeType.IsSubclassOf(typeof(Attribute)), "attributeType is not an attribute.");

            //
            // Sift through all types in assembly

            List<Type> result = assembly.GetTypes().ToList();
            for (int i = result.Count - 1; i >= 0; i--)
            {
                //
                // Check type for attribute

                MemberInfo typeMemberInfo = result[i];
                object[] attributes = typeMemberInfo.GetCustomAttributes(attributeType, inherit);

                //
                // If type has attribute, yield the result

                if (attributes.Any(attribute => attribute.GetType() == attributeType))
                    yield return result[i];
            }
        }
    }
}
