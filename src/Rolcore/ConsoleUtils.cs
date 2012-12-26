using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Rolcore
{
    /// <summary>
    /// Utilities for interacting with the <see cref="Console"/>.
    /// </summary>
    public static class ConsoleUtils
    {
        /// <summary>
        /// Prompts for input until valid input is received.
        /// </summary>
        /// <typeparam name="T">The type of input to prompt for.</typeparam>
        /// <param name="prompt">The initial prompt for input.</param>
        /// <param name="afterInvalidResponsePrompt">The prompt to use after receiving invalid input.</param>
        /// <returns>User input as the appropriate type.</returns>
        public static T PromptForValue<T>(string prompt, string afterInvalidResponsePrompt) where T : struct
        {
            //
            // Initial prompt

            Console.WriteLine(prompt);
            var input = Console.ReadLine();
            
            var result = new Nullable<T>();
            var convertibleInput = (IConvertible)input;

            while (!result.HasValue)
            {
                try
                {
                    //
                    // Attempt to convert

                    result = new Nullable<T>((T)convertibleInput.ToType(typeof(T), CultureInfo.CurrentCulture));
                }
                catch (Exception e)
                {
                    //
                    // Handle invalid input

                    if ((e is InvalidCastException) || (e is FormatException))
                    {
                        //
                        // Re-attempt 

                        Console.WriteLine(afterInvalidResponsePrompt);
                        input = Console.ReadLine();
                        convertibleInput = (IConvertible)input;
                    }
                    else
                        throw;
                }
            }

            return result.Value;
        }

        /// <summary>
        /// Prompts for input until valid input is received.
        /// </summary>
        /// <typeparam name="T">The type of input to prompt for.</typeparam>
        /// <param name="prompt">The prompt for input.</param>
        /// <returns>User input as the appropriate type.</returns>
        public static T PromptForValue<T>(string prompt) where T : struct
        {
            return PromptForValue<T>(prompt, prompt);
        }

        /// <summary>
        /// Prompts for string input.
        /// </summary>
        /// <param name="prompt">The prompt for input.</param>
        /// <returns>Input received by the console.</returns>
        public static string Prompt(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }
    }
}
