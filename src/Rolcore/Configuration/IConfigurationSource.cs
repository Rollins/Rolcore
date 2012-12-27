//-----------------------------------------------------------------------
// <copyright file="IConfigurationSource.cs" company="Rollins, Inc.">
//     Code from Kent Boogaart's article, found at http://kentb.blogspot.com/2010/02/automatic-configuration-exposure-with.html
//     and Modified by Rollins, Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Configuration
{
    /// <summary>
    /// When implemented in a derived class, provides access to configuration settings such as 
    /// those in an App.Config file.
    /// </summary>
    public interface IConfigurationSource
    {
        /// <summary>
        /// Gets the specified configuration section.
        /// </summary>
        /// <param name="name">Specifies the name of the configuration section.</param>
        /// <returns>The configuration section.</returns>
        object GetSection(string name);

        /// <summary>
        /// Returns true if the specified setting exists.
        /// </summary>
        /// <param name="key">Specifies the key used to identify the setting.</param>
        /// <returns>True if the setting exists, false if it does not.</returns>
        bool ContainsSetting(string key);

        /// <summary>
        /// Returns true if the specified section exists.
        /// </summary>
        /// <param name="name">Specifies the key used to identify the section.</param>
        /// <returns>True if the setting exists, false if it does not.</returns>
        bool ContainsSection(string name);

        /// <summary>
        /// Gets the specified setting value.
        /// </summary>
        /// <param name="key">Specifies the key used to identify the setting.</param>
        /// <returns>The value of the setting.</returns>
        string GetSetting(string key);
    }
}
