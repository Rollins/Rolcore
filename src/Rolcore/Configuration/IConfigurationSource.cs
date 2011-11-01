/*
 * Code from Kent Boogaart's article, found at
 * http://kentb.blogspot.com/2010/02/automatic-configuration-exposure-with.html
 * 
 * No licensing or copyright information given.
 */


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
