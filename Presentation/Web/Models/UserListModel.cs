using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class UserListModel
    {
        [DisplayName("用户名")]
        public string Name { get; set; }

        public IList<UserModel> UserData { get; set; } 

    }
}