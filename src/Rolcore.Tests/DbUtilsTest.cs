using Rolcore.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace Rolcore.Tests
{
    /// <summary>
    ///This is a test class for DbUtilsTest and is intended
    ///to contain all DbUtilsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DbUtilsTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        const string StringColumnName = "StringColumn";
        const string IntColumnName = "IntColumn";

        private static DataTable CreateTwoColumnTestTable()
        {
            DataTable result = new DataTable();
            result.Columns.Add(StringColumnName, typeof(string));
            result.Columns.Add(IntColumnName, typeof(int));

            return result;
        }

        /// <summary>
        ///A test for SetPrimaryKeys
        ///</summary>
        [TestMethod()]
        public void SetPrimaryKeysTest()
        {
            using (DataTable table = CreateTwoColumnTestTable())
            {
                Assert.AreEqual(0, table.PrimaryKey.Length);

                DbUtils.SetPrimaryKeys(table, StringColumnName);
                Assert.AreEqual(1, table.PrimaryKey.Length);
                Assert.AreEqual(StringColumnName, table.PrimaryKey[0].ColumnName);

                DbUtils.SetPrimaryKeys(table, StringColumnName, IntColumnName);
                Assert.AreEqual(2, table.PrimaryKey.Length);
                Assert.AreEqual(StringColumnName, table.PrimaryKey[0].ColumnName);
                Assert.AreEqual(IntColumnName, table.PrimaryKey[1].ColumnName);
            }
        }

        /// <summary>
        ///A test for PrimaryKeyOrDefault
        ///</summary>
        [TestMethod()]
        public void PrimaryKeyOrDefaultTest()
        {
            DataTable table = CreateTwoColumnTestTable();

            DataColumn[] actual = DbUtils.PrimaryKeyOrDefault(table);
            Assert.AreEqual(2, actual.Length);
            Assert.AreEqual(StringColumnName, actual[0].ColumnName);
            Assert.AreEqual(IntColumnName, actual[1].ColumnName);

            DbUtils.SetPrimaryKeys(table, StringColumnName);
            actual = DbUtils.PrimaryKeyOrDefault(table);
            Assert.AreEqual(1, actual.Length);
            Assert.AreEqual(StringColumnName, actual[0].ColumnName);
        }

        /// <summary>
        ///A test for GetRowPrimaryKeyValues
        ///</summary>
        [TestMethod()]
        public void GetRowPrimaryKeyValuesTest()
        {
            DataTable table = CreateTwoColumnTestTable();
            object[] expected = new object[] { "String Value", 42 };
            DataRow row = table.Rows.Add(expected);
            object[] actual = DbUtils.GetRowPrimaryKeyValues(row);
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
                Assert.AreEqual(expected[i], actual[i]);
        }

        /// <summary>
        ///A test for GetColumnNames
        ///</summary>
        [TestMethod()]
        public void GetColumnNamesFromDataTableTest()
        {
            DataTable table = CreateTwoColumnTestTable();
            string[] expected = new string[] { StringColumnName, IntColumnName };
            string[] actual = DbUtils.GetColumnNames(table);
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
                Assert.AreEqual(expected[i], actual[i]);
        }

        /// <summary>
        ///A test for GetColumnMapOrDefault
        ///</summary>
        [TestMethod()]
        public void GetColumnMapOrDefaultTest()
        {
            NameValueCollection columnMap = new NameValueCollection();
            string mappedIntColumnName = string.Format("{0}_Foo", IntColumnName);
            columnMap.Add(IntColumnName, mappedIntColumnName);

            DataTable source = CreateTwoColumnTestTable();
            NameValueCollection expected = new NameValueCollection();
            expected.Add(StringColumnName, StringColumnName);
            expected.Add(IntColumnName, mappedIntColumnName);

            NameValueCollection actual = DbUtils.GetColumnMapOrDefault(columnMap, source);
            Assert.AreEqual(expected.Count, actual.Count);
            foreach (string expectedColumnName in expected.AllKeys)
                Assert.AreEqual(expected[expectedColumnName], actual[expectedColumnName]);
        }

        /*[TestMethod()]
        public void ExeucteScalarTest()
        {
            Assert.Inconclusive("Verify the correctness of this test method.");
        }*/

        /// <summary>
        ///A test for CreateParameter
        ///</summary>
        [TestMethod()]
        public void CreateParameterDefaultDirectionTest()
        {
            string paramName = "TestParameter";
            SqlDbType dbType = SqlDbType.VarChar;
            int size = 24;
            object value = (SqlString)"Hello World";
            SqlParameter actual = DbUtils.CreateParameter(paramName, dbType, size, value);
            Assert.AreEqual(ParameterDirection.Input, actual.Direction);
            Assert.AreEqual(paramName, actual.ParameterName);
            Assert.AreEqual(size, actual.Size);
            Assert.AreEqual(dbType, actual.SqlDbType);
            Assert.AreEqual(value, actual.SqlValue);
            Assert.AreEqual(value, actual.Value);
        }

        /// <summary>
        ///A test for CreateParameter
        ///</summary>
        [TestMethod()]
        public void CreateParameterWithDirectionTest()
        {
            string paramName = "TestParameter";
            SqlDbType dbType = SqlDbType.VarChar;
            int size = 24;
            object value = (SqlString)"Hello World";
            ParameterDirection direction = ParameterDirection.ReturnValue;
            SqlParameter actual = DbUtils.CreateParameter(paramName, dbType, size, value, direction);
            Assert.AreEqual(direction, actual.Direction);
            Assert.AreEqual(paramName, actual.ParameterName);
            Assert.AreEqual(size, actual.Size);
            Assert.AreEqual(dbType, actual.SqlDbType);
            Assert.AreEqual(value, actual.SqlValue);
            Assert.AreEqual(value, actual.Value);
        }

        /// <summary>
        ///A test for CopyTableData
        ///</summary>
        [TestMethod()]
        public void CopyTableDataDataTableTest()
        {
            DataTable source = CreateTwoColumnTestTable();
            source.Rows.Add("Insert", 1);
            source.Rows.Add("Update", 2);
            source.Rows.Add("Same", 3);

            DataTable destination = CreateTwoColumnTestTable();
            destination.Rows.Add("Update", 1);
            destination.Rows.Add("Delete", 2);
            destination.Rows.Add("Same", 3);

            NameValueCollection columnMap = null;
            bool insert = true;
            bool update = true;
            bool delete = true;
            DbUtils.CopyTableData(source, destination, columnMap, insert, update, delete);

            DbUtils.SetPrimaryKeys(source, StringColumnName); // Ensure unaffected
            Assert.AreEqual(1, source.Rows.Find("Insert")[IntColumnName]);
            Assert.AreEqual(2, source.Rows.Find("Update")[IntColumnName]);
            Assert.AreEqual(3, source.Rows.Find("Same")[IntColumnName]);

            DbUtils.SetPrimaryKeys(destination, StringColumnName);
            Assert.AreEqual(1, source.Rows.Find("Insert")[IntColumnName]);
            Assert.AreEqual(2, source.Rows.Find("Update")[IntColumnName]);
            Assert.AreEqual(3, source.Rows.Find("Same")[IntColumnName]);
            Assert.IsNull(source.Rows.Find("Delete"));
        }

        /// <summary>
        ///A test for CopyRowData
        ///</summary>
        [TestMethod()]
        public void CopyRowDataTest()
        {
            DataTable table = CreateTwoColumnTestTable();
            DataRow source = table.Rows.Add("Row One", 1);
            DataRow destination = table.Rows.Add("Row Two", 2);
            NameValueCollection columnMap = null;
            DbUtils.CopyRowData(source, destination, columnMap);
            foreach (DataColumn column in table.Columns)
                Assert.AreEqual(source[column], destination[column]);
        }
    }
}
