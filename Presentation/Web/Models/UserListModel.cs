using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class UserListModel
    {
        public string Name { get; set; }

        public IList<UserModel> UserData { get; set; } 

    }
}