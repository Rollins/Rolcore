/*
 * Code from Kent Boogaart's article, found at
 * http://kentb.blogspot.com/2010/02/automatic-configuration-exposure-with.html
 * 
 * Modified by Rollins, Inc.
 */

using System.Configuration;
using System.Linq;

namespace Rolcore.Configuration
{
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

        public bool ContainsSetting(string key)
        {
            return ConfigurationManager.AppSettings.AllKeys.Contains(key);
        }

        public bool ContainsSection(string name)
        {
            return ConfigurationManager.GetSection(name) != null;
        }

        public string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
