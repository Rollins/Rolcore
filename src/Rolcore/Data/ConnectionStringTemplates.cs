using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace Rolcore.Data
{
    public static class ConnectionStringTemplates
    {
        public const string CsvFile = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=;Extended Properties=Text;";

        public static string CreateCsvFileConnectionString(string filePath)
        {
            OleDbConnectionStringBuilder resultBuilder = new OleDbConnectionStringBuilder(ConnectionStringTemplates.CsvFile) { 
                DataSource = filePath };

            return resultBuilder.ConnectionString;
        }
    }
}
