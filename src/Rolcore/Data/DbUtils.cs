using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using Rolcore.Reflection;

namespace Rolcore.Data
{
    public static class DbUtils
    {
        #region Primary Key Utilities
        public static void SetPrimaryKeys(DataTable table, params string[] primaryKeyColumnNames)
        {
            List<DataColumn> keyColumns = new List<DataColumn>(primaryKeyColumnNames.Length);
            foreach (DataColumn column in table.Columns)
            {
                if (primaryKeyColumnNames.Contains(column.ColumnName))
                    keyColumns.Add(column);
            }
            table.PrimaryKey = keyColumns.ToArray();
        }

        public static DataColumn[] PrimaryKeyOrDefault(DataTable table)
        {
            return (table.PrimaryKey.Length > 0) ? table.PrimaryKey : table.Columns.OfType<DataColumn>().ToArray();
        }


        public static object[] GetRowPrimaryKeyValues(DataRow row)
        {
            // Pre-conditions
            if (row == null)
                throw new ArgumentNullException("row", "row is null.");

            // Determine primary key, default to all rows if none specified
            DataColumn[] primaryKey = PrimaryKeyOrDefault(row.Table);

            // Get values from row that are part of the primary key
            List<object> result = new List<object>(primaryKey.Length);
            foreach (DataColumn primaryKeyColumn in primaryKey)
                result.Add(row[primaryKeyColumn]);

            return result.ToArray();
        }
        #endregion Primary Key Utilities

        #region Data Access Object Utilities
        public static DbConnection CreateDbConnection(ConnectionStringSettings connectionSettings)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(connectionSettings.ProviderName);
            DbConnection result = providerFactory.CreateConnection();
            result.ConnectionString = connectionSettings.ConnectionString;
            return result;
        }

        public static DbDataAdapter CreateDbDataAdapter(
            DbProviderFactory dbProviderFactory, 
            DbConnection dbConnection, 
            string tableName, 
            MissingSchemaAction missingSchemaAction, 
            bool selectOnly)
        {
            DbDataAdapter result = dbProviderFactory.CreateDataAdapter();
            result.SelectCommand = dbConnection.CreateCommand();
            result.SelectCommand.CommandText = string.Format("select * from [{0}]", tableName);
            result.SelectCommand.Connection = dbConnection;
            result.MissingSchemaAction = missingSchemaAction;

            DbCommandBuilder commandBuilder = dbProviderFactory.CreateCommandBuilder();
            commandBuilder.DataAdapter = result;

            if (!selectOnly)
            {
                result.InsertCommand = commandBuilder.GetInsertCommand(true);
                result.InsertCommand.Connection = dbConnection;
                result.UpdateCommand = commandBuilder.GetUpdateCommand(true);
                result.UpdateCommand.Connection = dbConnection;
                result.DeleteCommand = commandBuilder.GetDeleteCommand(true);
                result.DeleteCommand.Connection = dbConnection;
            }

            return result;
        }

        public static DbDataAdapter CreateDbDataAdapter(
            ConnectionStringSettings connectionSettings, 
            string tableName,
            MissingSchemaAction missingSchemaAction,
            bool selectOnly)
        {
            return CreateDbDataAdapter(
                DbProviderFactories.GetFactory(connectionSettings.ProviderName),
                CreateDbConnection(connectionSettings),
                tableName,
                missingSchemaAction,
                selectOnly);
        }

        public static SqlParameter CreateParameter(string paramName, SqlDbType dbType, int size,
            object value, ParameterDirection direction)
        {
            SqlParameter result = new SqlParameter
            {
                ParameterName = paramName,
                SqlDbType = dbType,
                Size = size,
                Value = value ?? DBNull.Value,
                Direction = direction,
                IsNullable = true
            };
            return result;
        }

        public static SqlParameter CreateParameter(string paramName, SqlDbType dbType, int size, object value)
        {
            return CreateParameter(paramName, dbType, size, value, ParameterDirection.Input);
        }
        #endregion Data Access Object Utilities

        #region Data Fetching Utlities
        public static DataTable GetTable(ConnectionStringSettings connectionSettings, string tableName)
        {
            using (DbDataAdapter sourceAdapter = CreateDbDataAdapter(
                connectionSettings, tableName, MissingSchemaAction.AddWithKey, true))
            {
                DataTable result = new DataTable(tableName);
                sourceAdapter.Fill(result);
                return result;
            }
        }

        public static T ExeucteScalar<T>(string connectionString, string query)
        {
            //TODO: Do NOT assume SqlConnection - change connectionString to a ConnectionSettings object
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection) { CommandType = CommandType.Text })
                {
                    connection.Open();
                    return (T)command.ExecuteScalar();
                }
            }
        }

        public static DataSet GetStoredProcedureData(
            SqlConnection connection, 
            string storedProcedureName,
            params SqlParameter[] parameters)
        {
            using (SqlCommand selectCommand = connection.CreateCommand())
            {
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.CommandText = storedProcedureName;
                if (parameters != null)
                    selectCommand.Parameters.AddRange(parameters);

                DataSet result = new DataSet();
                using (SqlDataAdapter adapter = new SqlDataAdapter(selectCommand))
                    adapter.Fill(result);
                return result;
            }
        }

        public static DataSet GetStoredProcedureData(
            string connectionString, 
            string storedProcedureName, 
            params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
                return GetStoredProcedureData(connection, storedProcedureName, parameters);
        }
        #endregion Data Fetching Utlities

        #region Data Copying Utilities
        public static void CopyRowData(DataRow source, DataRow destination, NameValueCollection columnMap)
        {
            // Pre-conditions
            if (source == null)
                throw new ArgumentNullException("source", "source is null.");
            if (destination == null)
                throw new ArgumentNullException("destination", "destination is null.");

            columnMap = GetColumnMapOrDefault(columnMap, source.Table);

            foreach (string sourceColumnName in columnMap.AllKeys)
                destination[columnMap[sourceColumnName]] = source[sourceColumnName];
        }

        /// <summary>
        /// Copies data from a <see cref="DataRow"/> to an <see cref="object"/>.
        /// </summary>
        /// <param name="source">The source row.</param>
        /// <param name="destination">The destination object.</param>
        /// <param name="ignoreBlankValues">When true, empty values in the source are ignored.</param>
        /// <param name="columnMap">A <see cref="NameValueCollection"/> for mapping the name of a 
        /// column in the source to the name of a property in the destination.</param>
        /// <returns>A <see cref="Dictionary<string, object>"/> of changed properties and their 
        /// original values from the destination object.</returns>
        public static Dictionary<string, object> CopyRowData(DataRow source, object destination, bool ignoreBlankValues, NameValueCollection columnMap, params string[] ignoreColumnNames)
        {
            // Pre-conditions
            if (source == null)
                throw new ArgumentNullException("source", "source is null.");
            if (destination == null)
                throw new ArgumentNullException("destination", "destination is null.");

            columnMap = GetColumnMapOrDefault(columnMap, source.Table);
            Type destinationType = destination.GetType();
            System.Reflection.PropertyInfo[] destinationProperties = destinationType.GetProperties();
            Dictionary<string, object> result = new Dictionary<string, object>(columnMap.Count);
            foreach (string sourceColumnName in columnMap.AllKeys)
            {
                string propertyName = columnMap[sourceColumnName];
                object sourceValue = source[sourceColumnName] ?? string.Empty;
                if ((ignoreBlankValues) && (sourceValue.ToString() == string.Empty))
                    continue;
                if (destinationProperties.Count(prop => prop.Name == propertyName) == 1)
                {
                    result.Add(propertyName, sourceValue);
                    object destinationValue = destination.GetPropertyValue(propertyName);
                    if (!sourceValue.Equals(destinationValue) && !ignoreColumnNames.Contains(sourceColumnName))
                        destination.SetPropertyValue(propertyName, sourceValue);
                }
            }

            return result;
        }

        public static void CopyTableData(DataTable source, DataTable destination, NameValueCollection columnMap, bool insert, bool update, bool delete)
        {
            if (source == null)
                throw new ArgumentNullException("source", "source is null.");
            if (destination == null)
                throw new ArgumentNullException("destination", "destination is null.");

            columnMap = GetColumnMapOrDefault(columnMap, source);
            bool destinationPrimaryKeyWasForced = false;
            bool sourcePrimaryKeyWasForced = false;

            destination.BeginLoadData();
            try
            {
                if (destination.PrimaryKey.Length == 0)
                {
                    SetPrimaryKeys(destination, GetColumnNames(destination));
                    destinationPrimaryKeyWasForced = true;
                }
                if (source.PrimaryKey.Length == 0)
                {
                    SetPrimaryKeys(source, GetColumnNames(source));
                    sourcePrimaryKeyWasForced = true;
                }
                // Inserts & Updates
                foreach (DataRow sourceRow in source.Rows)
                {
                    object[] sourceKey = GetRowPrimaryKeyValues(sourceRow);
                    DataRow destinationRow = destination.Rows.Find(sourceKey);
                    bool inserted = false;
                    if ((destinationRow == null) && (insert))
                    {
                        destinationRow = destination.Rows.Add();
                        CopyRowData(sourceRow, destinationRow, columnMap);
                        inserted = true;
                    }
                    if ((update) && (!inserted) && (destinationRow != null))
                        CopyRowData(sourceRow, destinationRow, columnMap);
                }

                // Deletes
                if (delete)
                {
                    for (int i = destination.Rows.Count - 1; i >= 0; i--)
                    {
                        object[] destinationKey = GetRowPrimaryKeyValues((DataRow)destination.Rows[i]);
                        DataRow sourceRow = source.Rows.Find(destinationKey);
                        if (sourceRow == null)
                            ((DataRow)destination.Rows[i]).Delete();
                    }
                }
            }
            finally
            {
                if (destinationPrimaryKeyWasForced)
                    destination.PrimaryKey = new DataColumn[] { };
                if (sourcePrimaryKeyWasForced)
                    source.PrimaryKey = new DataColumn[] { };

                destination.EndLoadData();
            }
        }

        public static void CopyTableData(ConnectionStringSettings sourceConnectionSettings, string sourceTableName, ConnectionStringSettings destinationConnectionSettings, string destinationTableName, NameValueCollection columnMap, bool insert, bool update, bool delete)
        {
            // Pre-conditions
            if (sourceConnectionSettings == null)
                throw new ArgumentNullException("sourceConnectionSettings", "sourceConnectionSettings is null.");
            if (String.IsNullOrEmpty(sourceTableName))
                throw new ArgumentException("sourceTableName is null or empty.", "sourceTableName");
            if (destinationConnectionSettings == null)
                throw new ArgumentNullException("destinationConnectionSettings", "destinationConnectionSettings is null.");
            if (String.IsNullOrEmpty(destinationTableName))
                throw new ArgumentException("destinationTableName is null or empty.", "destinationTableName");
            if (!insert && !update && !delete)
                throw new InvalidOperationException("Insert, update, and delete all dissalowed. Nothing to do.");

            DataTable source = GetTable(sourceConnectionSettings, sourceTableName);

            columnMap = GetColumnMapOrDefault(columnMap, source);

            DbDataAdapter destinationAdapter = CreateDbDataAdapter(
                destinationConnectionSettings,
                destinationTableName,
                MissingSchemaAction.AddWithKey,
                false);

            using (DataTable destination = new DataTable(destinationTableName))
            {
                destinationAdapter.Fill(destination);
                CopyTableData(source, destination, columnMap, insert, update, delete);
                destinationAdapter.Update(destination);
            }
        }
        #endregion Data Copying Utilities

        #region Data Structure Utlities
        public static NameValueCollection GetColumnMapOrDefault(NameValueCollection columnMap, DataTable source)
        {
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
        }

        public static string[] GetColumnNames(DataTable table)
        {
            List<string> result = new List<string>(table.Columns.Count);
            foreach (DataColumn column in table.Columns)
                result.Add(column.ColumnName);
            return result.ToArray();
        }

        public static string[] GetColumnNames(DbDataReader reader)
        {
            List<string> result = new List<string>(reader.FieldCount);
            for (int i = 0; i < reader.FieldCount; i++ )
                result.Add(reader.GetName(i));
            return result.ToArray();
        }
        #endregion Data Structure Utlities
    }
}
