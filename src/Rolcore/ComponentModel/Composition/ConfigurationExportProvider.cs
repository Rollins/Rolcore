/*
 * Code mostly by Kent Boogaart's article, located at
 * http://kentb.blogspot.com/2010/02/automatic-configuration-exposure-with.html
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.ReflectionModel;
using System.Linq;
using System.Reflection;
using Rollins.Configuration;

namespace Rollins.ComponentModel.Composition
{
    /// <summary>
    /// An MEF <see cref="ExportProvider"/> that surfaces export values from an 
    /// <see cref="IConfigurationSource"/>, such as one that reads configuration settings from the
    /// App.Config/Web.Config file or from a database.
    /// <example>
    ///     <code>
    ///     AssemblyCatalog catalog = new AssemblyCatalog(typeof(Program).Assembly);
    ///
    ///     using (var container = new CompositionContainer(catalog, new ConfigurationExportProvider(new FileConfigurationSource())))
    ///     {
    ///         Program program = new Program();
    ///         container.SatisfyImportsOnce(program);
    ///         program.SomeMethod();
    ///     }
    ///     </code>
    /// </example>
    /// </summary>
    public class ConfigurationExportProvider : ExportProvider
    {
        private readonly IConfigurationSource _ConfigurationSource;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="configurationSource">Specifies the configuration source.</param>
        public ConfigurationExportProvider(IConfigurationSource configurationSource)
        {
            // the configuration source determines where configuration values come from (eg. App.Config file)
            this._ConfigurationSource = configurationSource;
        }


        protected override IEnumerable<Export> GetExportsCore(ImportDefinition definition, AtomicComposition atomicComposition)
        {
            
            string contractName = definition.ContractName;

            //
            // No contract specified, nothing to do

            if (string.IsNullOrEmpty(contractName))
                yield break;

            //
            // Only support single value cardinalities

            if (definition.Cardinality != ImportCardinality.ZeroOrOne && definition.Cardinality != ImportCardinality.ExactlyOne)
                yield break;

            //
            // Check app settings, then sections

            if (this._ConfigurationSource.ContainsSetting(contractName))
            {
                //
                // Convert setting to the appropriate type for the importer

                MethodInfo getAccessor = ReflectionModelServices
                    .GetImportingMember(definition)
                    .GetAccessors()
                        .Where(x => x is MethodInfo)
                    .Select(x => x as MethodInfo)
                    .FirstOrDefault(x => 
                           (x.Attributes & MethodAttributes.SpecialName) == MethodAttributes.SpecialName 
                        && x.Name.StartsWith("get_", StringComparison.Ordinal));

                //
                // No type, no result

                if (getAccessor == null)
                    yield break;

                //
                // Perform type conversion

                Type targetType = getAccessor.ReturnType;
                string stringValue = this._ConfigurationSource.GetSetting(contractName);
                Export export = new Export(contractName, () => Convert.ChangeType(stringValue, targetType));

                yield return export;
            }
            else if (this._ConfigurationSource.ContainsSection(contractName))
            {
                //
                // Import is a configuration section

                object section = this._ConfigurationSource.GetSection(contractName);
                yield return new Export(contractName, () => section);
            }
        }
    }
}
