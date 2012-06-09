using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

using System.Linq;
using System.Reflection;

namespace Rolcore.Reflection
{
    /// <summary>
    /// Extensions for <see cref="Object"/>.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Determines if the given <see cref="object"/> is <see cref="Nullable<>"/>.
        /// </summary>
        /// <param name="obj">Specifies the object instance.</param>
        /// <returns>True if the specified object is nullable. False if it is not.</returns>
        public static bool IsNullableType(this object obj)
        {
            return (obj == null) || (obj.GetType().IsNullableType());
        }

        /// <summary>
        /// Use this instead of <see cref="Convert.ChangeType"/> to support 
        /// <see cref="Nullable<>"/> types. Returns an <see cref="Object"/> with the specified
        /// <see cref="Type"/> of which the value is equivelant to the specified object.
        /// </summary>
        /// <param name="obj">Specifies the object for which the type should change.</param>
        /// <param name="toType">Specifies the type to change the specified object to.</param>
        /// <returns>The specified object, converted to the specified type.</returns>
        public static object ChangeType(this object obj, Type toType)
        {
            if (obj.IsNullableType())
            {
                if (obj == null)
                    return null;
                NullableConverter oConverter = new NullableConverter(obj.GetType());
                toType = oConverter.UnderlyingType;
            }
            else if (toType.IsNullableType())
            {
                if (obj == null)
                    return null;
                NullableConverter converter = new NullableConverter(toType);
                toType = converter.UnderlyingType;
            }

            return Convert.ChangeType(obj, toType);
        }

        /// <summary>
        /// Gets an array of <see cref="MethodInfo"/> in which each item of the array is decorated
        /// with the specified attribute.
        /// </summary>
        /// <param name="obj">Specifies the instance to search.</param>
        /// <param name="attributeType">Specifies the attribute to search for.</param>
        /// <param name="inherit">Specifies weather to search the instance's inheritance chain to 
        /// find the attribute.</param>
        /// <returns>Methods decorated with the specified attribute.</returns>
        public static MethodInfo[] GetMethodsWithAttribute(this object obj, Type attributeType, bool inherit)
        {
            //
            // Pre-conditions
            if (obj == null)
                throw new ArgumentNullException("obj", "obj is null.");
            if (attributeType == null)
                throw new ArgumentNullException("attributeType", "attributeType is null.");
            if(!attributeType.IsSubclassOf(typeof(Attribute)))
                throw new ArgumentException("attributeType is not an attribute.");

            //
            // Find methods decorated w/ specified attribute

            List<MethodInfo> result = obj.GetType().GetMethods().ToList();
            for (int i = result.Count - 1; i >= 0; i--)
            {
                MethodInfo methodInfo = result[i];
                object[] attributes = methodInfo.GetCustomAttributes(attributeType, inherit);
                if (attributes.Count(attribute => attribute.GetType() == attributeType) == 0)
                    result.RemoveAt(i);
            }
            return result.ToArray();
        }

        /// <summary>
        /// Gets the value of a property of a given object.
        /// </summary>
        /// <param name="propertyName">Specifies the name of the property to be returned. This 
        /// value can be specified using "dot syntax", e.g. "FirstProperty.SecondProperty".</param>
        /// <param name="obj">Specifies the object from which to retrieve the property value.</param>
        /// <returns>The value of the specified property.</returns>
        public static object GetPropertyValue(this object obj, string propertyName)
        {
            //
            // Pre-conditions

            if (obj == null)
                throw new ArgumentNullException("obj", "obj is null.");
            if (String.IsNullOrEmpty(propertyName))
                throw new ArgumentException("propertyName is null or empty.", "propertyName");

            //
            // Setup

            Type type = obj.GetType();
            Binder binder = null;
            object[] args = null;

            //
            // Parse name, read value

            int dotIndex = propertyName.IndexOf('.');
            if (dotIndex >= 0)
            {
                string firstPropertyName = propertyName.Substring(0, dotIndex);
                object parentObj = type.InvokeMember(firstPropertyName, BindingFlags.GetProperty, binder, obj, args);
                return parentObj.GetPropertyValue(propertyName.Substring(dotIndex + 1));
            }
            
            object result = type.InvokeMember(propertyName, BindingFlags.GetProperty, binder, obj, args);

            return result;
        }

        /// <summary>
        /// Sets value of a property of a given object.
        /// </summary>
        /// <param name="obj">Specifies the object on which to act.</param>
        /// <param name="propertyName">Specifies the name of the property to be changed. This value
        /// can be specified using "dot syntax", e.g. "FirstProperty.SecondProperty".</param>
        /// <param name="propertyValue">Specifies the new value of the property.</param>
        public static void SetPropertyValue(this object obj, string propertyName, object propertyValue)
        {
            if (obj == null)
                throw new ArgumentNullException("obj", "obj is null.");
            if (String.IsNullOrEmpty(propertyName))
                throw new ArgumentException("propertyName is null or empty.", "propertyName");

            //
            // Parse object heararchy and set value.

            int dotIndex = propertyName.IndexOf('.');
            if (dotIndex >= 0) // Navigate the object heirarchy.
            {
                string firstPropertyName = propertyName.Substring(0, dotIndex);
                object parentObj = obj.GetPropertyValue(firstPropertyName);
                parentObj.SetPropertyValue(propertyName.Substring(dotIndex + 1), propertyValue);
            }
            else // Found the property to set the value of
            {
                Type type = obj.GetType();
                PropertyInfo property = type.GetProperty(propertyName);

                // Using "ChangeType" to ensure compatability. For example, assigning the string 
                // "5" to an integer property or assigning a non-nullable value to a nullable type.
                propertyValue = propertyValue.ChangeType(property.PropertyType);

                property.SetValue(obj, propertyValue, null);
            }
        }

        /// <summary>
        /// Sets value of multiple properties of a given object.
        /// </summary>
        /// <param name="obj">Specifies the object on which to act.</param>
        /// <param name="propertyValues">Specifies the names and values of the properties to set.</param>
        public static void SetPropertyValues(this object obj, NameValueCollection propertyValues)
        {
            //
            // Pre conditions

            if (obj == null)
                throw new ArgumentNullException("obj", "obj is null.");
            if (propertyValues == null)
                throw new ArgumentNullException("propertyValues", "propertyValues is null.");
            if (propertyValues.Count < 1)
                throw new ArgumentException("propertyValues is empty.");

            //
            // Set values

            foreach (string propertyKey in propertyValues.Keys)
            {
                string propertyValue = propertyValues[propertyKey];
                obj.SetPropertyValue(propertyKey, propertyValue);
            }
        }

        /// <summary>
        /// Copies value type properties with matching names and compatible types from the current 
        /// instance to the specified destination, even if the source and destination are not in 
        /// the same inheritance family. Incompatible and missing properties are ignored. If both
        /// the source and the destination have a complex property, the method will recurse if 
        /// shallow is false.
        /// </summary>
        /// <param name="source">Specifies the source object to copy from.</param>
        /// <param name="dest">Specifies the destination object to copy to.</param>
        /// <param name="shallow">Specifies if the copy should be shallow.</param>
        public static void CopyMatchingObjectPropertiesTo(this object source, object dest, bool shallow = false)
        {
            //
            // Pre-conditions

            if (source == null)
                throw new ArgumentNullException("source", "source is null.");
            if (dest == null)
                throw new ArgumentNullException("dest", "dest is null.");

            //
            // Copy

            var sourceProperties = source.GetType().GetProperties();
            var destProperties = dest.GetType().GetProperties();
            foreach (var sourceProperty in sourceProperties)
            {
                //
                // Find destination property 

                var propertyName = sourceProperty.Name;
                var sourceType = sourceProperty.PropertyType;
                var destProperty = destProperties
                    .SingleOrDefault(p =>
                           p.Name == propertyName
                        && p.PropertyType.IsAssignableFrom(sourceType));

                if (destProperty != null)
                {
                    //
                    // Perform copy, if possible

                    var sourcePropertyValue = source.GetPropertyValue(propertyName);
                    
                    //
                    // Assign value types properties

                    if (((sourceType.IsValueType || sourceType.IsAnsiClass) && destProperty.CanWrite) || (destProperty.CanWrite && sourcePropertyValue == null))
                        dest.SetPropertyValue(
                            propertyName,
                            sourcePropertyValue);

                    //
                    // Recurse on complex types 

                    else if (!shallow && !sourceType.IsValueType && !sourceType.IsAnsiClass && sourcePropertyValue != null)
                        sourcePropertyValue.CopyMatchingObjectPropertiesTo(dest.GetPropertyValue(propertyName));
                }

            }
        }
    }
}
