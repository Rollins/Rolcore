using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rolcore.Configuration
{
    /// <summary>
    /// When implemented in a derived class, provides access to configuration settings such as 
    /// those in an App.Config file.
    /// </summary>
    public interface IConfigurationSource
    {
        object GetSection(string name);

        bool ContainsSetting(string key);
        bool ContainsSection(string name);

        string GetSetting(string key);
    }
}
