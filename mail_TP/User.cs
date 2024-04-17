using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mail_TP
{
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }

        public User(string email, string password, string nickname)
        {
            Email = email;
            Password = password;
            Nickname = nickname;
            DataBase.Users.Add(this);
        }
    }
}
