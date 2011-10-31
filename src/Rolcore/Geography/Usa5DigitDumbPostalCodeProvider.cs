using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

namespace Rolcore.Geography
{
    /// <summary>
    /// Implements <see cref="IPostalCodeProvider"/> by providing 5-digit USA zip codes based on a
    /// range.
    /// </summary>
    [Export(typeof(IPostalCodeListProvider))]
    public class Usa5DigitDumbPostalCodeProvider : IPostalCodeListProvider
    {
        //
        // Backing stores, with defaults

        private uint 
            _PostalCodeRangeFirst = 0,
            _PostalCodeRangeLast = 99999;

        #region Properties

        [Import("Usa5DigitDumbPostalCodeProvider.PostalCodeRangeFirst", AllowDefault=true)]
        public uint PostalCodeRangeFirst
        {
            get { return _PostalCodeRangeFirst; }
            set { _PostalCodeRangeFirst = value; }
        }

        [Import("Usa5DigitDumbPostalCodeProvider.PostalCodeRangeLast", AllowDefault = true)]
        public uint PostalCodeRangeLast
        {
            get { return _PostalCodeRangeLast; }
            set { _PostalCodeRangeLast = value; }
        }

        #endregion Properties

        #region IPostalCodeProvider

        public IEnumerable<string> GetPostalCodes()
        {
            if (this.PostalCodeRangeFirst > this.PostalCodeRangeLast)
                throw new ArgumentOutOfRangeException("PostalCodeRangeFirst must be less than or equal to PostalCodeRangeLast");

            uint currentZip = this.PostalCodeRangeFirst;
            do
            {
                yield return currentZip.ToUsa5DigitPostalCode();
                currentZip = (this.PostalCodeRangeFirst < this.PostalCodeRangeLast) ? (currentZip + 1) : (currentZip - 1);
            } while (currentZip != this.PostalCodeRangeLast);
        }

        #endregion IPostalCodeProvider
    }
}
