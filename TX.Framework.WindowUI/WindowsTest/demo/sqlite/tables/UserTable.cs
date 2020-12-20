using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB.Bean;
using DBUtility.SQLite;
using System.Reflection;
using System.Data;
using System.Data.SQLite;

namespace SQLite.Tables {
    public class UserTable : DBManager {
        private static UserTable userTable = new UserTable();
        public static UserTable Instance {
            get { return userTable; }
        }

        private UserTable() {
            if (!this.tableExists(getTableName())) {
                this.createTable(getDBName(), getTableName(), typeof(UserPo));
            }
        }

        public String getTableName() {
            return "user";
        }

        public Boolean IsExist(User user) {
            if (user == null) {
                return false;
            }
            using (var dr = SQLiteHelper2.ExecuteReader(
                String.Format(connectionString1, getDBName()),
                String.Format("select count(*) from `{0}` where UserName={1} and PassWord={2}",
                    this.getTableName(), user.UserName, user.PassWord),
                CommandType.Text)) {
                return (dr != null && dr.Read()) ? dr.GetBoolean(0) : false;
            }
        }

        public Boolean Insert(User user) {
            if (user == null) {
                return false;
            }
            // SQL拼接
            int recordsAffected = SQLiteHelper2.ExecuteNonQuery(
                String.Format(connectionString1, getDBName()),
                String.Format("insert into `{0}` ({1}) values ({2})",
                    this.getTableName(), this.getFieldString(string.Empty), this.getValueString(user, "'")),
                CommandType.Text);
            return recordsAffected > 0;
        }

        public Boolean InsertSafe(User user) {
            if (user == null) {
                return false;
            }

            int recordsAffected = SQLiteHelper2.ExecuteNonQuery(
                String.Format(connectionString1, getDBName()),
                this.getSafeSqlForInsert(),
                CommandType.Text,
                this.getSafeInsertParams(user));
            // 防止SQL注入
            //int recordsAffected = SQLiteHelper2.ExecuteNonQuery(
            //    String.Format(connectionString1, getDBName()),
            //    "insert into `user` (username,password) values (@username,@password)",
            //    CommandType.Text,
            //    new SQLiteParameter[] {
            //        new SQLiteParameter("@username", user.UserName),
            //        new SQLiteParameter("@password", user.PassWord),
            //    });

            return recordsAffected > 0;
        }

        private String getFieldString(String prefix) {
            StringBuilder sb = new StringBuilder();
            Type type = typeof(UserPo);
            foreach (PropertyInfo pi in type.GetProperties()) {
                foreach (Attribute attr in pi.GetCustomAttributes(true)) {
                    ColumnAttribute column = attr as ColumnAttribute;
                    if (column != null && !column.IsPrimaryKey) {
                        sb.AppendFormat("{0}{1},", prefix, column.Name);
                    }
                }
            }
            String fieldString = (sb.Length > 1) ? sb.ToString(0, sb.Length - 1) : "";
            return fieldString;
        }

        private String getValueString(User user, String mask) {
            StringBuilder sb = new StringBuilder();
            UserPo userPo = new UserPo(user);
            Type type = userPo.GetType();
            foreach (PropertyInfo pi in type.GetProperties()) {
                foreach (Attribute attr in pi.GetCustomAttributes(true)) {
                    ColumnAttribute column = attr as ColumnAttribute;
                    if (column != null && !column.IsPrimaryKey) {
                        sb.AppendFormat("{0}{1}{2},", mask, pi.GetValue(userPo, null), mask);
                    }
                }
            }
            String fieldString = (sb.Length > 1) ? sb.ToString(0, sb.Length - 1) : "";
            return fieldString;
        }

        private String getValueStringSafe(User user) {
            StringBuilder sb = new StringBuilder();
            UserPo userPo = new UserPo(user);
            Type type = userPo.GetType();
            foreach (PropertyInfo pi in type.GetProperties()) {
                foreach (Attribute attr in pi.GetCustomAttributes(true)) {
                    ColumnAttribute column = attr as ColumnAttribute;
                    if (column != null && !column.IsPrimaryKey) {
                        sb.AppendFormat("'{0}',", pi.GetValue(userPo, null));
                    }
                }
            }
            String fieldString = (sb.Length > 1) ? sb.ToString(0, sb.Length - 1) : "";
            return fieldString;
        }

        private String getSafeSqlForInsert() {
            return this.createSafeSqlForInsert(this.getTableName(), this.getFieldString(""), this.getFieldString("@"));
        }

        private SQLiteParameter[] getSafeInsertParams(User user) {
            String[] paramNames = this.getFieldString("@").Split(',');
            String[] paramValues = this.getValueString(user, "").Split(',');

            SQLiteParameter[] parameters = new SQLiteParameter[paramNames.Length];
            for (Int32 i = 0; i < paramNames.Length; ++i) {
                parameters[i] = this.createSafeInsertParam(paramNames[i], paramValues[i]);
            }

            return parameters;
        }

        #region 构建安全插入SQL
        private String createSafeSqlForInsert(String tableName, String fields, String valueMask) {
            return String.Format("insert into `{0}` ({1}) values ({2})", tableName, fields, valueMask);
        }

        private SQLiteParameter createSafeInsertParam(String valueMask, String value) {
            return new SQLiteParameter(valueMask, value);
        }
        #endregion
    }
}
