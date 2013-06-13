//-----------------------------------------------------------------------
// <copyright file="ConfigurationExportProvider.cs" company="Rollins, Inc.">
//     Code from Kent Boogaart's article, found at 
//     http://kentb.blogspot.com/2010/02/automatic-configuration-exposure-with.html and Modified by
//     Rollins, Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.ComponentModel.Composition
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.ComponentModel.Composition.ReflectionModel;
    using System.Linq;
    using System.Reflection;
    using Rolcore.Configuration;

    /// <summary>
    /// An <see cref="ExportProvider"/> that exports values from an App.Config or Web.Config file.
    /// </summary>
    [Export(typeof(ExportProvider)), Export(typeof(ConfigurationExportProvider))]
    public class ConfigurationExportProvider : ExportProvider
    {
        /// <summary>
        /// The source of configuration values, for example the App.Config.
        /// </summary>
        private readonly IConfigurationSource configurationSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationExportProvider"/> class.
        /// </summary>
        /// <param name="configurationSource">Specifies the source of configuration values, for 
        /// example an App.Config or Web.Config file.</param>
        public ConfigurationExportProvider(IConfigurationSource configurationSource)
        {
            this.configurationSource = configurationSource;
        }

        /// <summary>
        /// Gets all the exports that match the constraint defined by the specified definition.
        /// </summary>
        /// <param name="definition">The object that defines the conditions of the 
        /// System.ComponentModel.Composition.Primitives.Export objects to return.</param>
        /// <param name="atomicComposition">The transactional container for the composition.</param>
        /// <returns>A collection that contains all the exports that match the specified condition.</returns>
        protected override IEnumerable<Export> GetExportsCore(
            ImportDefinition definition, 
            AtomicComposition atomicComposition)
        {
            var contractName = definition.ContractName;

            var nothingToDo =
                string.IsNullOrEmpty(contractName) // no contract
                || (definition.Cardinality != ImportCardinality.ZeroOrOne
                 && definition.Cardinality != ImportCardinality.ExactlyOne); // we only support single value cardinalities

            if (nothingToDo)
            {
                yield break;
            }

            if (this.configurationSource.ContainsSetting(contractName))
            {
                // import was found to be an app setting - may need to convert it to an appropriate type for the importer
                Type targetType = null;
                string stringValue = null;

                if (ReflectionModelServices.IsImportingParameter(definition))
                {
                    // import appears on a parameter
                    var importingParameter = ReflectionModelServices.GetImportingParameter(definition);
                    targetType = importingParameter.Value.ParameterType;
                    stringValue = this.configurationSource.GetSetting(contractName);
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
                    stringValue = this.configurationSource.GetSetting(contractName);
                }

                if (targetType == null)
                {
                    yield break;
                }

                var export = new Export(contractName, () => Convert.ChangeType(stringValue, targetType));
                yield return export;
            }
            else if (this.configurationSource.ContainsSection(contractName))
            {
                // import was found to be a configuration section
                var section = this.configurationSource.GetSection(contractName);
                yield return new Export(contractName, () => section);
            }
        }
    }
}
