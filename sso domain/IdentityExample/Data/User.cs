using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityExample.Data
{
    public class Users
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }
    }

    public static class UserTable
    {
        public static List<Users> Users { get; set; } = new List<Users>();
    }
}
