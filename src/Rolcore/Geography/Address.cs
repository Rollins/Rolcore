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
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string StateOrProvince { get; set; }
        public string PostalCode { get; set; }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(Address1);
            if (!string.IsNullOrEmpty(Address2))
                result.AppendLine(Address2);
            result.AppendLine(string.Format("{0}, {1} {2}", City, StateOrProvince, PostalCode));
            return result.ToString();
        }
    }
}
