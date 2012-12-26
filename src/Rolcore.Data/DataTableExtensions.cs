using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Collections.Specialized;
using System.Data.OleDb;

namespace Rolcore.Data
{
    /// <summary>
    /// Extension methods for <see cref="DataTable"/>.
    /// </summary>
    public static class DataTableExtensions
    {

        /// <summary>
        /// Imports a CSV into a DataTable
        /// </summary>
        /// <param name="folderPath">Path of the CSV file</param>
        /// <param name="fileName">File name including the extension</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public static void FillFromCsvFile(this DataTable table, string folderPath, string fileName)
        {
            //
            // Pre-conditions 

            if (table == null)
                throw new ArgumentNullException("table", "table is null.");
            if (String.IsNullOrEmpty(folderPath))
                throw new ArgumentException("folderPath is null or empty.", "folderPath");
            if (String.IsNullOrEmpty(fileName))
                throw new ArgumentException("fileName is null or empty.", "fileName");

            //
            // Fill 

            string command = string.Format("SELECT * FROM {0}", fileName);
            using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(command, ConnectionStringTemplates.CreateCsvFileConnectionString(folderPath)))
                dataAdapter.Fill(table);
        }

        /// <summary>
        /// Converts the given <see cref="DataTable"/> into the equivelant HTML table.
        /// </summary>
        /// <param name="table">Specifies the <see cref="DataTable"/> to convert to HTML.</param>
        /// <returns>An HTML representation of the given <see cref="DataTable"/>.</returns>
        public static string ToHtmlTable(this DataTable table)
        {
            using (StringWriter writer = new StringWriter())
            {
                HtmlTextWriter html = new HtmlTextWriter(writer);

                //
                // Start of table

                html.RenderBeginTag(HtmlTextWriterTag.Table);

                //
                // Header

                html.RenderBeginTag(HtmlTextWriterTag.Tr);

                foreach (DataColumn column in table.Columns)
                {
                    html.RenderBeginTag(HtmlTextWriterTag.Th);
                    html.WriteLine(column.ColumnName);
                    html.RenderEndTag();
                } 

                html.RenderEndTag();

                //
                // Content (rows)

                foreach (DataRow row in table.Rows)
                {
                    html.RenderBeginTag(HtmlTextWriterTag.Tr);

                    foreach (DataColumn column in table.Columns)
                    {
                        html.RenderBeginTag(HtmlTextWriterTag.Td);
                        string rowData = row[column].ToString();
                        html.WriteLine(rowData);
                        html.RenderEndTag();
                    }

                    html.RenderEndTag();
                }

                //
                // End of table

                html.RenderEndTag();

                return writer.GetStringBuilder().ToString();
            }
        }

        /// <summary>
        /// Gets the column names of the table.
        /// </summary>
        /// <param name="table">Specifies the table to retrieve the column names from.</param>
        /// <returns>An array of strings containing the names of all the columns in the table.</returns>
        public static string[] GetColumnNames(this DataTable table)
        {
            List<string> result = new List<string>(table.Columns.Count);
            foreach (DataColumn column in table.Columns)
                result.Add(column.ColumnName);
            return result.ToArray();
        }

        /// <summary>
        /// Generates a column map for the specified source <see cref="DataTable"/>, adding only 
        /// unmapped columns.
        /// </summary>
        /// <param name="source">Specifies the source <see cref="DataTable"/> containing the 
        /// columns to be mapped.</param>
        /// <param name="columnMap">Specifies any existing mappings.</param>
        public static NameValueCollection GetColumnMapOrDefault(this DataTable source, NameValueCollection columnMap = null)
        {
            //
            // Pre-conditions

            if (source == null)
                throw new ArgumentNullException("source", "source is null.");
            if (columnMap == null)
                columnMap = new NameValueCollection(source.Columns.Count);

            NameValueCollection result = new NameValueCollection(columnMap);

            // Force entries in columnMap for all columns, assuming only mismatched column names
            // were mapped

            if (result.Count != source.Columns.Count)
                foreach (DataColumn sourceColumn in source.Columns)
                    if (!result.AllKeys.Contains(sourceColumn.ColumnName))
                        result.Add(sourceColumn.ColumnName, sourceColumn.ColumnName);

            return result;

            //TODO: Unit test
        }
    }
}
