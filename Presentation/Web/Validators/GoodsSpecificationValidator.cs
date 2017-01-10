using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Validators
{
    public class GoodsSpecificationValidator : AbstractValidator<GoodsSpecificationModel>
    {
        public GoodsSpecificationValidator()
        {
            RuleFor(m => m.Name)
                 .NotEmpty().WithMessage("商品规格不能为空");

        }
    }
}