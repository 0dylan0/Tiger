using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Validators
{
    public class UserValidator : AbstractValidator<UserModel>
    {
        public UserValidator()
        {
            RuleFor(m => m.Name)
                 .NotEmpty().WithMessage("用户名不能为空");
            RuleFor(m => m.Password)
               .NotEmpty().WithMessage("用户密码不能为空");;
        }
    }
}