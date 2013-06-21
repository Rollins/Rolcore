//-----------------------------------------------------------------------
// <copyright file="DataRowExtensionsTest.cs" company="Rollins, Inc.">
//     Copyright © Rollins, Inc. 
// </copyright>
//-----------------------------------------------------------------------
namespace Rolcore.Data.Tests
{
    using System;
    using System.Collections.Specialized;
    using System.Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Rolcore.Data;
    
    /// <summary>
    /// Tests for <see cref="DataRowExtensions"/>.
    /// </summary>
    [TestClass]
    public sealed class DataRowExtensionsTest : IDisposable
    {
        #region Test Attributes
        /// <summary>
        /// The name used for the <see cref="table"/>.
        /// </summary>
        private const string DataTableName = "Test";

        /// <summary>
        /// The name used for a column containing a <see cref="string"/>.
        /// </summary>
        private const string StringColumnName = "StringCol";

        /// <summary>
        /// The name used for a column containing an <see cref="int"/>.
        /// </summary>
        private const string IntColumnName = "IntCol";

        /// <summary>
        /// The name used for a column containing a <see cref="bool"/>.
        /// </summary>
        private const string BoolColumnName = "BoolCol";

        /// <summary>
        /// The suffix added to column names for <see cref=" cref=CreateDifferentTable"/>.
        /// </summary>
        private const string DifferentTableColumnSuffix = "DifferentName";

        /// <summary>
        /// An array of all column names.
        /// </summary>
        private readonly string[] columnNames = 
        {
            StringColumnName,
            IntColumnName,
            BoolColumnName
        };

        /// <summary>
        /// An array of all column types.
        /// </summary>
        private readonly Type[] columnTypes = 
        {
            typeof(string),
            typeof(int),
            typeof(bool)
        };
        
        /// <summary>
        /// Table to contain the target <see cref="DataRow"/> objects.
        /// </summary>
        private DataTable table;

        /// <summary>
        /// Test initialization.
        /// </summary>
        [TestInitialize]
        public void DataRowExtensionsTestInitialize()
        {
            Assert.AreEqual(this.columnNames.Length, this.columnTypes.Length);

            this.table = new DataTable(DataTableName);

            for (int i = 0; i < this.columnNames.Length; i++)
            {
                var column = new DataColumn(this.columnNames[i], this.columnTypes[i]);
                this.table.Columns.Add(column);
            }
        }

        /// <summary>
        /// Test cleanup.
        /// </summary>
        [TestCleanup]
        public void Dispose()
        {
            if (this.table != null)
            {
                this.table.Dispose();
                this.table = null;
            }
        }

        /// <summary>
        /// Creates a table with a different schema than <see cref="table"/>.
        /// </summary>
        /// <returns>A <see cref="DataTable"/> with a different schema.</returns>
        public DataTable CreateDifferentTable()
        {
            var result = new DataTable();
            for (int i = 0; i < this.columnNames.Length; i++)
            {

                //// Different Column Names

                var column = new DataColumn(
                    this.columnNames[i] + DifferentTableColumnSuffix, this.columnTypes[i]);
                result.Columns.Add(column);

                //// Same Column Names

                column = new DataColumn(
                    this.columnNames[i], this.columnTypes[i]);
                result.Columns.Add(column);
            }

            return result;
        }
        #endregion Test Attributes

        /// <summary>
        /// Tests performing a direct copy from one row to another where both rows have the same
        /// schema.
        /// </summary>
        [TestMethod]
        public void CopyTo_DoesAStraightCopy()
        {
            var sourceRow = this.table.NewRow();
            sourceRow[StringColumnName] = "String Value";
            sourceRow[IntColumnName] = 123;
            sourceRow[BoolColumnName] = true;

            var destRow = this.table.NewRow();
            sourceRow.CopyTo(destRow);

            Assert.AreEqual(sourceRow[StringColumnName], destRow[StringColumnName]);
            Assert.AreEqual(sourceRow[IntColumnName], destRow[IntColumnName]);
            Assert.AreEqual(sourceRow[BoolColumnName], destRow[BoolColumnName]);
        }

        [TestMethod]
        public void CopyTo_CopiesMatchingRowsByDefault()
        {
            using (var differentTable = this.CreateDifferentTable())
            {
                var sourceRow = this.table.NewRow();
                sourceRow[StringColumnName] = "String Value";
                sourceRow[IntColumnName] = 123;
                sourceRow[BoolColumnName] = true;

                var destRow = differentTable.NewRow();

                sourceRow.CopyTo(destRow);

                //// Different Column Names

                for (int i = 0; i < this.columnNames.Length; i++)
                {
                    Assert.AreNotEqual(sourceRow[this.columnNames[i]], destRow[this.columnNames[i] + DifferentTableColumnSuffix]);
                }

                //// Same Column Names

                for (int i = 0; i < this.columnNames.Length; i++)
                {
                    Assert.AreEqual(sourceRow[this.columnNames[i]], destRow[this.columnNames[i]]);
                }
            }
        }

        [TestMethod]
        public void CopyTo_CopiesMappedColumns()
        {
            using (var differentTable = this.CreateDifferentTable())
            {
                var sourceRow = this.table.NewRow();
                sourceRow[StringColumnName] = "String Value";
                sourceRow[IntColumnName] = 123;
                sourceRow[BoolColumnName] = true;

                var destRow = differentTable.NewRow();

                var map = new NameValueCollection(columnNames.Length);
                map.Add(StringColumnName, StringColumnName + DifferentTableColumnSuffix);
                map.Add(IntColumnName, IntColumnName + DifferentTableColumnSuffix);
                map.Add(BoolColumnName, BoolColumnName + DifferentTableColumnSuffix);

                sourceRow.CopyTo(destRow, map);

                //// Different Column Names

                for (int i = 0; i < this.columnNames.Length; i++)
                {
                    Assert.AreEqual(sourceRow[this.columnNames[i]], destRow[this.columnNames[i] + DifferentTableColumnSuffix]);
                }

                //// Same Column Names

                for (int i = 0; i < this.columnNames.Length; i++)
                {
                    Assert.AreNotEqual(sourceRow[this.columnNames[i]], destRow[this.columnNames[i]]);
                }
            }
        }
    }
}
