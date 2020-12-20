using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB.Bean;
using DBUtility.SQLite;
using System.Reflection;
using System.Data;

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

        public Boolean Insert(User user) {
            if (user == null) {
                return false;
            }
            int recordsAffected = SQLiteHelper2.ExecuteNonQuery(
                String.Format(connectionString1, getDBName()),
                String.Format("insert into `{0}` ({1}) values ({2})",
                    this.getTableName(), this.getFieldString(), this.getValueString(user)),
                CommandType.Text);
            return recordsAffected > 0;
        }

        private String getFieldString() {
            StringBuilder sb = new StringBuilder();
            Type type = typeof(UserPo);
            foreach (PropertyInfo pi in type.GetProperties()) {
                foreach (Attribute attr in pi.GetCustomAttributes(true)) {
                    ColumnAttribute column = attr as ColumnAttribute;
                    if (column != null && !column.IsPrimaryKey) {
                        sb.AppendFormat("{0},", column.Name);
                    }
                }
            }
            String fieldString = (sb.Length > 1) ? sb.ToString(0, sb.Length - 1) : "";
            return fieldString;
        }

        private String getValueString(User user) {
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
    }
}
