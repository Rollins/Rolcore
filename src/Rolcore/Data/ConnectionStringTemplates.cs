using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace Rolcore.Data
{
    /// <summary>
    /// Handy templates for ADO.NET connection strings.
    /// </summary>
    public static class ConnectionStringTemplates
    {
        /// <summary>
        /// Template for an ASP.NET connection to a comma-seperated value file.
        /// </summary>
        public const string CsvFile = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=;Extended Properties=Text;";

        /// <summary>
        /// Create's a connection string to a comma-seperated-value (CSV) file.
        /// </summary>
        /// <param name="filePath">Specifies the path to the CSV file.</param>
        /// <returns>A string that can be used as a connection string in an ADO.NET connection.</returns>
        public static string CreateCsvFileConnectionString(string filePath)
        {
            OleDbConnectionStringBuilder resultBuilder = new OleDbConnectionStringBuilder(ConnectionStringTemplates.CsvFile) { 
                DataSource = filePath };

            return resultBuilder.ConnectionString;
        }
    }
}
