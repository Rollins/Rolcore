//-----------------------------------------------------------------------
// <copyright file="StateOrProvince.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Geography
{
    /// <summary>
    /// Represents a geographical state or province.
    /// </summary>
    public class StateOrProvince
    {
        /// <summary>
        /// Gets or sets a value specifying the name of the province.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value specifying the standard abbreviation of the province.
        /// </summary>
        public string Abbreviation { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="StateOrProvince"/>.
        /// </summary>
        /// <param name="abbreviation">Specifies the initial value for <see cref="Abbreviation"/>.</param>
        /// <param name="name">Specifies the initial value for <see cref="Name"/>.</param>
        public StateOrProvince(string abbreviation, string name)
        {
            this.Name = name;
            this.Abbreviation = abbreviation;
        }
    }
}
