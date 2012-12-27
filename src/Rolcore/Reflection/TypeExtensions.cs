//-----------------------------------------------------------------------
// <copyright file="TypeExtensions.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Reflection
{
    using System;

    /// <summary>
    /// Extension methods for <see cref="Type"/>.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Determines if the given <see cref="Type"/> is <see cref="Nullable<>"/>.
        /// </summary>
        /// <param name="t">Specifies the type.</param>
        /// <returns>True if the specified type is nullable. False if it is not.</returns>
        public static bool IsNullableType(this Type type)
        {
            return (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)));
        }

        /// <summary>
        /// Determines if the given <see cref="Type"/> has an empty constructor.
        /// </summary>
        /// <param name="type">Specifies the <see cref="Type"/> to evaluate.</param>
        /// <returns>True if the <see cref="Type"/> contains an empty constructor.</returns>
        public static bool HasEmptyConstructor(this Type type)
        {
            return type.GetConstructor(Type.EmptyTypes) != null;
        }
    }
}
