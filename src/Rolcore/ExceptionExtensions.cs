using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rolcore
{
    /// <summary>
    /// Extension methods for <see cref="Exception"/>.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Performs an action for the given <see cref="Exception"/> and its inner exceptions.
        /// </summary>
        /// <param name="exception">Specifies the exception to iterate.</param>
        /// <param name="action">Specifies the action to take for each exception.</param>
        public static void ForEachInnerException(this Exception exception, Action<Exception> action)
        {
            Exception currentException = exception;
            while (currentException != null)
            {
                action(currentException);
                currentException = currentException.InnerException;
            }
        }
    }
}
