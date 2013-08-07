// <copyright file="CompositionUtils.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.ComponentModel.Composition
{
    using System;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.IO;
    using System.Linq;
    using Rolcore.Configuration;

    /// <summary>
    /// Utilities for composing objects using MEF.
    /// </summary>
    public static class Containers
    {
        /// <summary>
        /// Creates a <see cref="CompositionContainer"/> with an <see cref="ApplicationCatalog"/> a
        /// <see cref="ConfigurationExportProvider"/>. The 
        /// <see cref="ConfigurationExportProvider"/> is reads from the application's config file,
        /// either an App.Config or Web.Config file.
        /// </summary>
        /// <param name="additionalProviders"></param>
        /// <returns></returns>
        public static CompositionContainer CreateApplicationAndConfigFileContainer(params ExportProvider[] additionalProviders)
        {
            var applicationCatalog = new ApplicationCatalog();
            var configExportProvider = new ConfigurationExportProvider(new FileConfigurationSource());
            
            var exportProviders = new ExportProvider[1 + additionalProviders.Length];
            exportProviders[0] = configExportProvider;
            Array.Copy(additionalProviders, 0, exportProviders, 1, additionalProviders.Length);

            var result = new CompositionContainer(applicationCatalog, configExportProvider);
            return result;
        } // TODO: Test
    }
}
