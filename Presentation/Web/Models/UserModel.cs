using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Web.Validators;

namespace Web.Models
{
    [Validator(typeof(UserValidator))]
    public class UserModel
    {
        public int ID { get; set; }

        [DisplayName("用户名")]
        public string Name { get; set; }

        [DisplayName("密码")]
        public string Password { get; set; }
    }
}