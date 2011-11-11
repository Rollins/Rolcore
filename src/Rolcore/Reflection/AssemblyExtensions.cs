using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;


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

            if (assembly == null)
                throw new ArgumentNullException("assembly", "assembly is null.");
            if (attributeType == null)
                throw new ArgumentNullException("attributeType", "attributeType is null.");
            if (!attributeType.IsSubclassOf(typeof(Attribute)))
                throw new ArgumentNullException("attributeType is not an attribute.");

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
