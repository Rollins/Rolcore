using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rolcore.Repository
{
    /// <summary>
    /// Extension methods for <see cref="IRepositoryReader{}"/>.
    /// </summary>
    public static class IRepositoryReaderExtensions
    {
        /// <summary>
        /// Executes the specified where clause for each specified value and yields the results. 
        /// This can be used instead of a Contains(), which not all <see cref="IQueryable{}"/> 
        /// implementations support, but it is MUCH SLOWER than a native Contains() so use caution.
        /// </summary>
        /// <param name="repository">Specifies the repository to run the where clause on.</param>
        /// <param name="values">Specifies the values to iterate.</param>
        /// <param name="where">Specifies the where clause.</param>
        /// <returns>The results of each WHERE execution.</returns>
        public static IEnumerable<T> IterativeWhere<T>(this IRepositoryReader<T> repository, IEnumerable values, Func<object, T, bool> where)
            where T : class
        {
            foreach (var value in values)
            {
                var items = repository.Items().AsEnumerable()
                    .Where(item => 
                        where(value, item));

                foreach(var result in items)
                {
                    yield return result;
                }
            }
        }
    }
}
