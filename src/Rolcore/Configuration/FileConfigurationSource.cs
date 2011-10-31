using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

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
