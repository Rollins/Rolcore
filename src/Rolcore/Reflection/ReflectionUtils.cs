using System;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Rolcore.Reflection
{
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
        /// Replaces variables in a string with property values from an object. Variables within 
        /// the string must be in {$Variable.Name} format.
        /// </summary>
        /// <param name="s">The string containing variable names to replace.</param>
        /// <param name="obj">The object from which to exctract values.</param>
        /// <returns>Parameter "s" replaced with values from the object.</returns>
        public static string ReplaceVars(string s, object obj)
        {
            StringBuilder result = new StringBuilder(s);
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

        public static T CreateObject<T>(string typeName, string assemblyName) where T : class
        {
            return ((T)Activator.CreateInstance(assemblyName, typeName).Unwrap());
        }

        public static IEnumerable<Type> GetTypesWithAttribute(string assemblyName, Type attributeType, bool inherit)
        {
            Assembly assembly = Assembly.Load(assemblyName);
            return assembly.GetTypesWithAttribute(attributeType, inherit);
        }
    }
    
}
