using System;
using TX.Framework.DbHelper;
using TX.Framework.DbHelper.SQLite;

namespace WindowsTest {
    class DbHelper {
        private static UserTable instance = new UserTable();

        public static Boolean Login(String username, String password) {
            Boolean ret = instance.IsMatch(
                    new User { UserName = username, PassWord = password });
            return ret;
        }

        public static Boolean Register(String username, String password) {
            Boolean ret = instance.Insert(
                    new User { UserName = username, PassWord = password });
            return ret;
        }

        public static Boolean IsUserExist(String username) {
            Boolean ret = instance.IsExist(username);
            return ret;
        } 
    }
}
