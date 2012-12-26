//-----------------------------------------------------------------------
// <copyright file="ConnectionStringTemplates.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Data
{
    using System.Data.OleDb;

    /// <summary>
    /// Handy templates for ADO.NET connection strings.
    /// </summary>
    public static class ConnectionStringTemplates
    {
        public const string MsJetOleDb4 = "Data Source=;Provider=Microsoft.Jet.OLEDB.4.0;";
        public const string MsAceOleDb12 = "Data Source=;Provider=Microsoft.ACE.OLEDB.12.0;";

        /// <summary>
        /// Template for an ADO.NET connection to a comma separated value file.
        /// </summary>
        public const string CsvFile = MsJetOleDb4 + "Extended Properties=Text;";

        /// <summary>
        /// Template for an ADO.NET connection to an Excel 8 file.
        /// </summary>
        public const string Excel8 = MsJetOleDb4 + @"Extended Properties=""Excel 8.0;IMEX=1""";

        /// <summary>
        /// Template for an ADO.NET connection to an Excel 12 file.
        /// </summary>
        public const string Excel12 = MsAceOleDb12 + "Extended Properties=Excel 12.0;";

        /// <summary>
        /// Creates a connection string to a comma separated value (CSV) file.
        /// </summary>
        /// <param name="filePath">Specifies the path to the CSV file.</param>
        /// <returns>A string that can be used as a connection string in an ADO.NET connection.</returns>
        public static string CreateCsvFileConnectionString(string filePath)
        {
            var resultBuilder = new OleDbConnectionStringBuilder(ConnectionStringTemplates.CsvFile){ 
                DataSource = filePath };

            return resultBuilder.ConnectionString;
        }

        /// <summary>
        /// Creates a connection string to an Excel (version 8) file.
        /// </summary>
        /// <param name="filePath">Specifies the path to the Excel file.</param>
        /// <returns>A string that can be used as a connection string in an ADO.NET connection.</returns>
        public static string CreateExcel8FileConnectionString(string filePath)
        {
            var resultBuilder = new OleDbConnectionStringBuilder(ConnectionStringTemplates.Excel8){
                DataSource = filePath };

            return resultBuilder.ConnectionString;
        }

        /// <summary>
        /// Creates a connection string to an Excel (version 8) file.
        /// </summary>
        /// <param name="filePath">Specifies the path to the Excel file.</param>
        /// <returns>A string that can be used as a connection string in an ADO.NET connection.</returns>
        public static string CreateExcel12FileConnectionString(string filePath)
        {
            var resultBuilder = new OleDbConnectionStringBuilder(ConnectionStringTemplates.Excel12){
                DataSource = filePath };

            return resultBuilder.ConnectionString;
        }
    }
}
