//-----------------------------------------------------------------------
// <copyright file="State.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Geography
{
    /// <summary>
    /// Represents a geographical state or province.
    /// </summary>
    public class StateOrProvince
    {

        public string StateName { get; set; }

        public string StateAbbreviation { get; set; }

        public StateOrProvince(string stateAbbreviation, string stateName)
        {
            this.StateName = stateName;
            this.StateAbbreviation = stateAbbreviation;
        }
    }
}
