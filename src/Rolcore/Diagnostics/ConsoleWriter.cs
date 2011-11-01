/*
 * Code from EduSoft, found at
 * http://www.koders.com/csharp/fid0A1086040608245C7586A5EB7CB1CB5E8C346F39.aspx
 * 
 * No licensing or copyright information given.
 * 
 * Modified by Rollins, Inc.
 */

using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using Rolcore;

namespace Rolcore.Diagnostics
{
    /// <summary>
    /// Implements a <see cref="TextWriter"/> for writing information to the Console.
    /// </summary>
    /// <seealso cref="Debugger.Log"/>
    public class ConsoleWriter : TextWriter
    {
        private bool isOpen;
        private static UnicodeEncoding encoding;
        private readonly int level;
        private readonly string category;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DebuggerWriter"/> class.
        /// </summary>
        public ConsoleWriter()
            : this(0, Debugger.DefaultCategory)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleWriter"/> class with the specified level and category.
        /// </summary>
        /// <param name="level">A description of the importance of the messages.</param>
        /// <param name="category">The category of the messages.</param>
        public ConsoleWriter(int level, string category)
            : this(level, category, CultureInfo.CurrentCulture)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleWriter"/> class with the specified level, category and format provider.
        /// </summary>
        /// <param name="level">A description of the importance of the messages.</param>
        /// <param name="category">The category of the messages.</param>
        /// <param name="formatProvider">An <see cref="IFormatProvider"/> object that controls formatting.</param>
        public ConsoleWriter(int level, string category, IFormatProvider formatProvider)
            : base(formatProvider)
        {
            this.level = level;
            this.category = string.IsNullOrWhiteSpace(category) ? string.Empty : String.Format("({0}):", category);
            this.isOpen = true;
        }
        protected ConsoleWriter(IFormatProvider formatProvider)
            : base(formatProvider)
        {
            
        }
        #endregion Constructors

        protected override void Dispose(bool disposing)
        {
            isOpen = false;
            base.Dispose(disposing);
        }

        public override void Write(char value)
        {
            if (!isOpen)
                throw new ObjectDisposedException(null);

            Console.Write(string.Format("{0}{1} {2}", "\t".Repeat(level), category, value));
        }

        public override void Write(string value)
        {
            if (!isOpen)
            {
                throw new ObjectDisposedException(null);
            }
            if (value != null)
            {
                Console.Write(string.Format("{0}{1} {2}", "\t".Repeat(level), category, value));
            }
        }

        public override void Write(char[] buffer, int index, int count)
        {
            if (!isOpen)
            {
                throw new ObjectDisposedException(null);
            }
            if (buffer == null || index < 0 || count < 0 || buffer.Length - index < count)
            {
                base.Write(buffer, index, count); // delegate throw exception to base class
            }
            Console.Write(string.Format("{0}{1} {2}", "\t".Repeat(level), category, new string(buffer, index, count)));
        }

        public override Encoding Encoding
        {
            get
            {
                if (encoding == null)
                {
                    encoding = new UnicodeEncoding(false, false);
                }
                return encoding;
            }
        }

        public int Level
        {
            get { return level; }
        }

        public string Category
        {
            get { return category; }
        }
    }
}
