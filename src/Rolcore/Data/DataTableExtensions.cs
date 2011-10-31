using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics.Contracts;
using System.Data.OleDb;

namespace Rolcore.Data
{
    public static class DataTableExtensions
    {
        /// <summary>
        /// Imports a CSV into a DataTable
        /// </summary>
        /// <param name="folderPath">Path of the CSV file</param>
        /// <param name="fileName">File name including the extension</param>
        /// <returns></returns>
        public static void FillFromCsvFile(this DataTable table, string folderPath, string fileName)
        {
            Contract.Requires(table != null, "table is null.");
            Contract.Requires(!String.IsNullOrEmpty(folderPath), "filePath is null or empty.");
            Contract.Requires(!String.IsNullOrEmpty(fileName), "fileName is null or empty.");

            string command = string.Format("SELECT * FROM {0}", fileName);
            using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(command, ConnectionStringTemplates.CreateCsvFileConnectionString(folderPath)))
            {
                dataAdapter.Fill(table);
            }
        }
    }
}
