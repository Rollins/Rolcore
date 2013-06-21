using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rolcore.Configuration;
using System.Configuration;

namespace Rolcore.Tests
{
    [TestClass]
    public class ConfigurationExtensionsTest
    {
        [TestMethod]
        public void WriteAppSetting_AddsNewSetting()
        {
            var target = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var setting = target.AppSettings.Settings["temp setting"];
            Assert.IsNull(setting, "setting already exists");

            target.WriteAppSetting("temp setting", "arbitrary value");
            setting = target.AppSettings.Settings["temp setting"];
            Assert.AreEqual("arbitrary value", setting.Value);

            target.DeleteAppSetting("temp setting");
        }

        [TestMethod]
        public void DeleteAppSetting_DeletesSetting()
        {
            var target = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            target.WriteAppSetting("temp setting", "arbitrary value");

            target.DeleteAppSetting("temp setting");
            var setting = target.AppSettings.Settings["temp setting"];
            Assert.IsNull(setting, "setting not deleted");
        }

        [TestMethod]
        public void WriteAppSetting_UpdatesExistingSetting()
        {
            var target = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var setting = target.AppSettings.Settings["existing setting"];
            var settingValue = DateTime.UtcNow.ToString("u");
            Assert.AreNotEqual(settingValue, setting.Value);

            target.WriteAppSetting("existing setting", settingValue);
            Assert.AreEqual(settingValue, setting.Value); 
        }
    }
}
