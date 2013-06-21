//-----------------------------------------------------------------------
// <copyright file="ReflectionUtils.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
using System.Diagnostics.Contracts;
namespace Rolcore.Reflection
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Contains static utility methods for common reflection tasks.
    /// </summary>
    public static class ReflectionUtils
    {

        private static Regex CreateVarMatcher()
        {
            return new Regex("(\\{)(\\$).*?(\\})", RegexOptions.Compiled | RegexOptions.IgnoreCase); // {$varName}
        }

        /// <summary>
        /// Gets a value indicating the directory path in which the currently executing assembly 
        /// resides.
        /// <seealso cref="Assembly.GetExecutingAssembly"/>
        /// </summary>
        public static string DirectoryPathOfExecutingAssembly
        {
            get
            {
                return Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            }
        }

        /// <summary>
        /// Replaces variables in a string with property values from an object. Variables within 
        /// the string must be in {$Variable.Name} format.
        /// </summary>
        /// <param name="s">The string containing variable names to replace.</param>
        /// <param name="obj">The object from which to extract values.</param>
        /// <returns>Parameter "s" replaced with values from the object.</returns>
        public static string ReplaceVars(string s, object obj)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(s), "s is null or empty.");
            Contract.Requires<ArgumentNullException>(obj != null, "obj is null.");

            var result = new StringBuilder(s);
            Regex varMatcher = CreateVarMatcher();
            MatchCollection varMatches = varMatcher.Matches(s);

            foreach (Match varMatch in varMatches)
            {
                string varName = varMatch.Value.Substring(2, varMatch.Value.Length - 3);
                object varValue = obj.GetPropertyValue(varName);
                if (varValue == null)
                    varValue = string.Empty;
                if ((varValue is IEnumerable) && (!(varValue is string)))
                {
                    IEnumerable arrayVarValue = (IEnumerable)varValue;
                    StringBuilder newVarValue = new StringBuilder();
                    foreach (object arrayEntry in arrayVarValue)
                        newVarValue.AppendLine(arrayEntry.ToString());

                    varValue = newVarValue.ToString();
                }
                result.Replace(varMatch.Value, varValue.ToString());
            }

            return result.ToString();
        }

        /// <summary>
        /// Creates an instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The result type.</typeparam>
        /// <param name="typeName">Specifies the name of the <see cref="Type"/> of object to 
        /// construct.</param>
        /// <param name="assemblyName">Specifies the assembly in which the specified type can be 
        /// found.</param>
        /// <returns>A new instance of the specified type.</returns>
        public static T CreateObject<T>(string typeName, string assemblyName) where T : class
        {
            Contract.Requires<ArgumentNullException>(typeName != null, "typeName is null");
            Contract.Requires<ArgumentNullException>(assemblyName != null, "assemblyName is null");

            return (T)Activator.CreateInstance(assemblyName, typeName).Unwrap();
        }

        /// <summary>
        /// Gets all <see cref="Type"/>s that are decorated with the specified attribute.
        /// </summary>
        /// <param name="assemblyName">Specifies the name of the assembly to search.</param>
        /// <param name="attributeType">Specifies the <see cref="Type"/> of <see cref="Attribute"/>
        /// to search for.</param>
        /// <param name="inherit">Specifies if inheritance chains should be searched.</param>
        /// <returns>An enumerable list of types decorated with the specified attribute.</returns>
        public static IEnumerable<Type> GetTypesWithAttribute(string assemblyName, Type attributeType, bool inherit)
        {
            if (string.IsNullOrEmpty(assemblyName))
                throw new ArgumentException("assemblyName is null or emoty");
            if (attributeType == null)
                throw new ArgumentNullException("attributeType");

            var assembly = Assembly.Load(assemblyName);
            return assembly.GetTypesWithAttribute(attributeType, inherit);
        }
    }
    
}
