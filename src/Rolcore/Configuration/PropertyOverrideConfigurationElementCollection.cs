using System.Configuration;
using System.Collections;
using System;

namespace Rolcore.Configuration
{
    [Obsolete("Use the MEF Framework. See: Rolcore.ComponentModel.Composition.ConfigurationExportProvider.")]
    [ConfigurationCollection(typeof(PropertyOverrideConfigurationElement), AddItemName = "propertyOverride")]
    public class PropertyOverrideConfigurationElementCollection : ConfigurationElementCollection
    {
        #region Constructors
        protected PropertyOverrideConfigurationElementCollection() {}
        protected PropertyOverrideConfigurationElementCollection(IComparer comparer) : base(comparer) {}
        #endregion Constructors

        protected override ConfigurationElement CreateNewElement()
        {
            return new PropertyOverrideConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((PropertyOverrideConfigurationElement)element).Name;
        }

        public PropertyOverrideConfigurationElement this[int index]
        {
            get
            {
                return base.BaseGet(index) as PropertyOverrideConfigurationElement;
            }
            set
            {
                if (base.BaseGet(index) != null)
                    base.BaseRemoveAt(index);
                this.BaseAdd(index, value);
            }
        }
    }
}
