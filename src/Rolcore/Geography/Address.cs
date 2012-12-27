//-----------------------------------------------------------------------
// <copyright file="Address.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Geography
{
    using System.Text;

    /// <summary>
    /// Represents an address.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets the first line of the address.
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the second line of the address.
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the city of the address.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state/province line of the address.
        /// </summary>
        public string StateOrProvince { get; set; }

        /// <summary>
        /// Gets or sets the postal/zip code line of the address.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Returns a string representing the <see cref="Address"/> instance.
        /// </summary>
        /// <returns>The address instance, represented as a string.</returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(this.Address1);

            if (!string.IsNullOrEmpty(this.Address2))
            {
                result.AppendLine(this.Address2);
            }

            result.AppendLine(
                string.Format("{0}, {1} {2}", this.City, this.StateOrProvince, this.PostalCode));
            return result.ToString();
        }
    }
}
