using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Validators
{
    public class GoodsUnitValidator : AbstractValidator<GoodsUnitModel>
    {
        public GoodsUnitValidator()
        {

            RuleFor(m => m.Name)
                 .NotEmpty().WithMessage("仓库名不能为空");

        }
    }
}