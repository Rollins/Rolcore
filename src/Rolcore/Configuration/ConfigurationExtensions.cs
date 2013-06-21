using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace Rolcore.Configuration
{
    /// <summary>
    /// Extension methods for <see cref="System.Configuration.Configuration"/>.
    /// </summary>
    public static class ConfigurationExtensions
    {
        [Pure]
        public static bool IsAppSettingsReadOnly(this System.Configuration.Configuration config)
        {
            return config.AppSettings.IsReadOnly();
        }

        public static void WriteAppSetting(this System.Configuration.Configuration config, string key, string value)
        {
            Contract.Requires<ArgumentNullException>(config != null, "config is null.");
            Contract.Requires<InvalidOperationException>(config.AppSettings != null, "config does not contain AppSettings.");
            Contract.Requires<InvalidOperationException>(!config.IsAppSettingsReadOnly(), "AppSettings is read-only.");
            Contract.Requires<InvalidOperationException>(!config.AppSettings.SectionInformation.IsLocked, "AppSettings section is locked.");
            Contract.Requires<ArgumentException>(!String.IsNullOrEmpty(key), "key is null or empty.");
            Contract.Requires<ArgumentException>(!String.IsNullOrEmpty(value), "value is null or empty.");

            if (config.AppSettings.Settings.AllKeys.Contains(key))
            {
                config.AppSettings.Settings[key].Value = value;
            }
            else
            {
                config.AppSettings.Settings.Add(new KeyValueConfigurationElement(key, value));
            }
    
            config.Save();
        }

        public static void DeleteAppSetting(this System.Configuration.Configuration config, string key)
        {
            Contract.Requires<ArgumentNullException>(config != null, "config is null.");
            Contract.Requires<InvalidOperationException>(config.AppSettings != null, "config does not contain AppSettings.");
            Contract.Requires<InvalidOperationException>(!config.IsAppSettingsReadOnly(), "AppSettings is read-only.");
            Contract.Requires<InvalidOperationException>(!config.AppSettings.SectionInformation.IsLocked, "AppSettings section is locked.");
            Contract.Requires<ArgumentException>(!String.IsNullOrEmpty(key), "key is null or empty.");

            config.AppSettings.Settings.Remove(key);
            config.Save();
        }
    }
}
