using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using TX.Framework.DbHelper.Bean;

namespace TX.Framework.DbHelper.SQLite {
    public class DBManager {
        protected String connectionString1 = "Data Source={0};";
        protected String connectionString2 = "Data Source={0};Version=3;Password=123;";
        private static readonly String defaultDbDir = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly String defaultDbName = "SqliteHelper.db";
        private String dbName = String.Empty;

        protected DBManager() : this(defaultDbName) { }

        protected DBManager(String dbName) {
            if (!String.IsNullOrWhiteSpace(dbName)) {
                if (!dbName.Contains("\\")) {
                    dbName = defaultDbDir + dbName;
                }
            }
            else {
                dbName = defaultDbDir + defaultDbName;
            }
            this.dbName = dbName;
            this.createDB(getDBName());
        }

        protected String getDBName() {
            return this.dbName;
        }

        /// <summary>
        /// 数据库是否存在
        /// </summary>
        public Boolean dbExists(String dbName) {
            return File.Exists(dbName);
        }

        /// <summary>
        /// 当前连接，是否存在表
        /// </summary>
        public Boolean tableExists(String tableName) {
            return this.tableExists(getDBName(), tableName);
        }

        /// <summary>
        /// 当前连接，是否存在表
        /// </summary>
        public Boolean tableExists(string dbName, String tableName) {
            using (var dr = SQLiteHelper2.ExecuteReader(
                String.Format(connectionString1, dbName),
                String.Format(Sql.TABLE_EXISTS, tableName),
                CommandType.Text)) {
                return dr != null && dr.HasRows;
            }
        }

        /// <summary>
        /// 获取数据库中所有表名
        /// </summary>
        public List<String> getTableNames(String dbName) {
            List<String> tables = new List<String>();
            var dr = SQLiteHelper2.ExecuteReader(
                String.Format(connectionString1, dbName),
                Sql.GET_ALL_TABLE_NAME,
                CommandType.Text);
            while (dr != null && dr.Read()) {
                tables.Add(dr[0].ToString());
            }
            return tables;
        }

        /// <summary>
        /// 创建数据库
        /// </summary>
        public void createDB(String dbName) {
            if (!File.Exists(dbName)) {
                File.Create(dbName).Close();
            }
        }

        /// <summary>
        /// 创建表
        /// </summary>
        public Boolean createTable(String dbName, String tableName, Type type) {
            using (var dr = SQLiteHelper2.ExecuteReader(
                String.Format(connectionString1, dbName),
                String.Format(Sql.CREATE_TABLE, tableName, getTableColumnDefinition(type)),
                CommandType.Text)) {}
            return tableExists(dbName, tableName);
        }

        /// <summary>
        /// 通过反射，获取表定义
        /// </summary>
        public String getTableColumnDefinition(Type type) {
            String columnString = null;
            List<String> keys = new List<String>();
            StringBuilder sb = new StringBuilder();

            // 遍历属性特性，生成列
            foreach (PropertyInfo pi in type.GetProperties()) {
                foreach (Attribute attr in pi.GetCustomAttributes(true)) {
                    ColumnAttribute column = attr as ColumnAttribute;
                    if (column != null) {
                        System.Diagnostics.Debug.Assert(column.Name != null);
                        System.Diagnostics.Debug.Assert(column.ColumnDefinition != null);
                        sb.AppendFormat("`{0}` {1},", column.Name, column.ColumnDefinition);
                    }
                }
            }

            columnString = sb.ToString(0, sb.Length - 1);
            return columnString;
        }
    }
}
