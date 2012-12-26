//-----------------------------------------------------------------------
// <copyright file="IPostalCodeListProvider.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. All rights reserved.
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
        IEnumerable<string> GetPostalCodes();
    }
}
