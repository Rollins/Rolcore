using System;
using System.Data;
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
            if (table == null)
                throw new ArgumentNullException("table", "table is null.");
            if (String.IsNullOrEmpty(folderPath))
                throw new ArgumentException("folderPath is null or empty.", "folderPath");
            if (String.IsNullOrEmpty(fileName))
                throw new ArgumentException("fileName is null or empty.", "fileName");
            
            string command = string.Format("SELECT * FROM {0}", fileName);
            using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(command, ConnectionStringTemplates.CreateCsvFileConnectionString(folderPath)))
            {
                dataAdapter.Fill(table);
            }
        }
    }
}
