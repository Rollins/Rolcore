//-----------------------------------------------------------------------
// <copyright file="FileConfigurationSource.cs" company="Rollins, Inc.">
//     Code from Kent Boogaart's article, found at http://kentb.blogspot.com/2010/02/automatic-configuration-exposure-with.html
//     and Modified by Rollins, Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Configuration
{
    using System.Configuration;
    using System.Linq;

    /// <summary>
    /// Implements <see cref="IConfigurationSource"/> to provide configuration via the 
    /// <see cref="ConfigurationManager"/>.
    /// <seealso cref="ConfigurationExportProvider"/>
    /// </summary>
    public class FileConfigurationSource : IConfigurationSource
    {
        /// <summary>
        /// Gets a configuration section with the specified name.
        /// </summary>
        /// <param name="name">Specifies the name of the configuration section to return.</param>
        public object GetSection(string name)
        {
            return ConfigurationManager.GetSection(name);
        }

        /// <summary>
        /// Returns true if the specified setting exists.
        /// </summary>
        /// <param name="key">Specifies the key used to identify the setting.</param>
        /// <returns>True if the setting exists, false if it does not.</returns>
        public bool ContainsSetting(string key)
        {
            return ConfigurationManager.AppSettings.AllKeys.Contains(key);
        }

        /// <summary>
        /// Returns true if the specified section exists.
        /// </summary>
        /// <param name="name">Specifies the key used to identify the section.</param>
        /// <returns>True if the setting exists, false if it does not.</returns>
        public bool ContainsSection(string name)
        {
            return ConfigurationManager.GetSection(name) != null;
        }

        /// <summary>
        /// Gets the specified setting value.
        /// </summary>
        /// <param name="key">Specifies the key used to identify the setting.</param>
        /// <returns>The value of the setting.</returns>
        public string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
