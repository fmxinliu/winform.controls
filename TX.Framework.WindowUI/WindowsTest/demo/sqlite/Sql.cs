using System;

namespace SQLite {
    public static class Sql {
        public readonly static String TABLE_EXISTS = "select * from sqlite_master where type='table' and name='{0}'";
        public readonly static String GET_ALL_TABLE_NAME = "select name from sqlite_master where type='table'";
        public readonly static String CREATE_TABLE = "create table `{0}` ({1})";
        public readonly static String DELETE_TABLE = "drop table `{0}`";
    }
}
