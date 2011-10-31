using System;
using System.Configuration;

namespace Rolcore.Configuration
{
    [Obsolete("Use the MEF Framework. See: Rolcore.ComponentModel.Composition.ConfigurationExportProvider.")]
    public static class AppSettingsUtils
    {
        public static string ReadStringSetting(Type type, string propertyName)
        {
            string appSettingsKey = string.Format("{0}.{1}", type.FullName, propertyName);
            return ConfigurationManager.AppSettings[appSettingsKey];
        }

        public static ConnectionStringSettings ReadConnectionStringSettings(Type type, string propertyName)
        {
            return ConfigurationManager.ConnectionStrings[string.Format("{0}.{1}", type.FullName, propertyName)];
        }

        public static string ReadConnectionString(Type type, string propertyName)
        {
            ConnectionStringSettings result = ReadConnectionStringSettings(type, propertyName);
            return (result != null) ? result.ConnectionString : null;
        }
    }
}
