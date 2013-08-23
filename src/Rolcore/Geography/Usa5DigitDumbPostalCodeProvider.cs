//-----------------------------------------------------------------------
// <copyright file="Usa5DigitDumbPostalCodeProvider.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Geography
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Implements <see cref="IPostalCodeProvider"/> by providing 5-digit USA zip codes based on a
    /// range.
    /// </summary>
    [Export(typeof(IPostalCodeListProvider))]
    public class Usa5DigitDumbPostalCodeProvider : IPostalCodeListProvider
    {
        /// <summary>
        /// Backing store and default for <see cref="PostalCodeRangeFirst"/>
        /// </summary>
        private uint postalCodeRangeFirst = 0;

        /// <summary>
        /// Backing store and default for <see cref="PostalCodeRangeLast"/>
        /// </summary>
        private uint postalCodeRangeLast = MaxPostalCodeRangeLast;

        public const uint MaxPostalCodeRangeLast = 99999;

        #region Properties

        /// <summary>
        /// Gets or sets a value specifying the lower-bound of the list of postal codes to 
        /// generate.
        /// </summary>
        [Import("Usa5DigitDumbPostalCodeProvider.PostalCodeRangeFirst", AllowDefault = true)]
        public uint PostalCodeRangeFirst
        {
            get { return this.postalCodeRangeFirst; }
            set { this.postalCodeRangeFirst = value; }
        }

        /// <summary>
        /// Gets or sets a value specifying the upper-bound of the list of postal codes to 
        /// generate.
        /// </summary>
        [Import("Usa5DigitDumbPostalCodeProvider.PostalCodeRangeLast", AllowDefault = true)]
        public uint PostalCodeRangeLast
        {
            get { return this.postalCodeRangeLast; }
            set 
            {
                Contract.Requires<ArgumentOutOfRangeException>(value <= MaxPostalCodeRangeLast, "value cannot be greater than MaxPostalCodeRangeLast");
                this.postalCodeRangeLast = value; 
            }
        }

        #endregion Properties

        #region IPostalCodeProvider

        /// <summary>
        /// Gets a list of 5-digit postal codes.
        /// </summary>
        /// <returns>An enumerable of postal code strings.</returns>
        public IEnumerable<string> GetPostalCodes()
        {
            if (this.PostalCodeRangeFirst > this.PostalCodeRangeLast)
            {
                throw new ArgumentOutOfRangeException("PostalCodeRangeFirst must be less than or equal to PostalCodeRangeLast");
            }

            uint currentZip = this.PostalCodeRangeFirst;
            do
            {
                yield return currentZip.ToUsa5DigitPostalCode();
                currentZip = (this.PostalCodeRangeFirst < this.PostalCodeRangeLast) ? (currentZip + 1) : (currentZip - 1);
            } 
            while (currentZip != this.PostalCodeRangeLast);
        }

        #endregion IPostalCodeProvider
    }
}
