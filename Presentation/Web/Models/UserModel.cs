using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Validators;

namespace Web.Models
{
    [Validator(typeof(UserValidator))]
    public class UserModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }
    }
}