//-----------------------------------------------------------------------
// <copyright file="IPostalCodeListProvider.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Geography
{
    using System.Collections.Generic;

    /// <summary>
    /// When implemented in a derived class, provides a list of postal codes.
    /// </summary>
    public interface IPostalCodeListProvider
    {
        /// <summary>
        /// When implemented in a derived class, returns a list of postal codes, typically for a 
        /// given local.
        /// </summary>
        /// <returns>An enumerable of postal code strings.</returns>
        IEnumerable<string> GetPostalCodes();
    }
}
