using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rolcore.Repository
{
    public static class IRepositoryWriterExtensions
    {
        public static void ApplyRulesDefaultImplementation<TItem, TConcurrency>(this IRepositoryWriter<TItem, TConcurrency> writer, params TItem[] items)
            where TItem : class
        {
            Debug.WriteLineIf(writer.Rules == null, "No rules specified for " + typeof(TItem));
            if (writer.Rules != null)
            {
                foreach (var item in items)
                {
                    foreach (var rule in writer.Rules)
                    {
                        rule.Apply(item);
                    }
                }
            }
        }
    }
}
