using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Validators
{
    public class ClientTypeValidator : AbstractValidator<ClientTypeModel>
    {
        public ClientTypeValidator()
        {

            RuleFor(m => m.Name)
                 .NotEmpty().WithMessage("客户类别名不能为空");

        }
    }
}