using System.Configuration;
using System;

namespace Rolcore.Configuration
{
    [Obsolete("Use the MEF Framework. See: Rolcore.ComponentModel.Composition.ConfigurationExportProvider.")]
    public class PropertyOverrideConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get
            {
                return this["name"] as string;
            }
        }

        [ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get
            {
                return this["value"] as string;
            }
        }
    }
}
