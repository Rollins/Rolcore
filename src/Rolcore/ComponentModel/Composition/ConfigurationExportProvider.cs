//-----------------------------------------------------------------------
// <copyright file="ConfigurationExportProvider.cs" company="Rollins, Inc.">
//     Code from Kent Boogaart's article, found at http://kentb.blogspot.com/2010/02/automatic-configuration-exposure-with.html
//     and Modified by Rollins, Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.ComponentModel.Composition
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.ComponentModel.Composition.ReflectionModel;
    using System.Linq;
    using System.Reflection;
    using Rolcore.Configuration;

    /// <summary>
    /// An <see cref="ExportProvider"/> that exports values from an App.Config or Web.Config file.
    /// </summary>
    public class ConfigurationExportProvider : ExportProvider
    {
        private readonly IConfigurationSource _ConfigurationSource;

        public ConfigurationExportProvider(IConfigurationSource configurationSource)
        {
            // the configuration source determines where configuration values come from (eg. App.Config file)
            _ConfigurationSource = configurationSource;
        }

        protected override IEnumerable<Export> GetExportsCore(ImportDefinition definition, AtomicComposition atomicComposition)
        {
            var contractName = definition.ContractName;

            if (string.IsNullOrEmpty(contractName))
            {
                // no contract, nothing we can do
                yield break;
            }

            if (definition.Cardinality != ImportCardinality.ZeroOrOne && definition.Cardinality != ImportCardinality.ExactlyOne)
            {
                // we only support single value cardinalities
                yield break;
            }

            if (this._ConfigurationSource.ContainsSetting(contractName))
            {
                // import was found to be an app setting - may need to convert it to an appropriate type for the importer
                Type targetType = null;
                string stringValue = null;

                if (ReflectionModelServices.IsImportingParameter(definition))
                {
                    // import appears on a parameter
                    var importingParameter = ReflectionModelServices.GetImportingParameter(definition);
                    targetType = importingParameter.Value.ParameterType;
                    stringValue = this._ConfigurationSource.GetSetting(contractName);
                }
                else
                {
                    // import appears on a member (property)
                    var getAccessor = ReflectionModelServices
                        .GetImportingMember(definition)
                        .GetAccessors()
                        .Where(x => x is MethodInfo)
                        .Select(x => x as MethodInfo)
                        .FirstOrDefault(x => (x.Attributes & MethodAttributes.SpecialName) == MethodAttributes.SpecialName && x.Name.StartsWith("get_", StringComparison.Ordinal));

                    if (getAccessor == null)
                    {
                        // cannot determine type of importing property
                        yield break;
                    }

                    targetType = getAccessor.ReturnType;
                    stringValue = this._ConfigurationSource.GetSetting(contractName);
                }

                if (targetType == null)
                {
                    yield break;
                }

                var export = new Export(contractName, () => Convert.ChangeType(stringValue, targetType));
                yield return export;
            }
            else if (this._ConfigurationSource.ContainsSection(contractName))
            {
                // import was found to be a configuration section
                var section = this._ConfigurationSource.GetSection(contractName);
                yield return new Export(contractName, () => section);
            }
        }
    }
}
