using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class UserEditPasswordModel
    {
        public string Code
        {
            get;
            set;
        }


        public string UserName
        {
            get;
            set;
        }


        public string Password
        {
            get;
            set;
        }


        public string NewPassword
        {
            get;
            set;
        }

        public string ConfirmNewPassword
        {
            get;
            set;
        }
    }
}