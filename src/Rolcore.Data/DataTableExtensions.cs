using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Web.UI;

namespace Rolcore.Data
{
    public static class DataTableExtensions
    {
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
    }
}
