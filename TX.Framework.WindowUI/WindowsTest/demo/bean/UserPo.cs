using System;

namespace DB.Bean {
    public class UserPo {
        [Column("id", "integer PRIMARY KEY", true)]
        public int ProductId { get; set; }

        [Column("username", "varchar(32) NOT NULL")]
        public String UserName { get; set; }

        [Column("password", "varchar(32)")]
        public String PassWord { get; set; }

        public UserPo(User user) {
            this.UserName = user.UserName;
            this.PassWord = user.PassWord;
        }
    }
}
