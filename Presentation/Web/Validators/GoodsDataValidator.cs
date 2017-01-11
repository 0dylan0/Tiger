﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Validators
{
    public class GoodsDataValidator : AbstractValidator<GoodsDataModel>
    {
        public GoodsDataValidator()
        {

            RuleFor(m => m.GoodsName)
                 .NotEmpty().WithMessage("商品名不能为空");
            RuleFor(m => m.GoodType)
                 .NotEmpty().WithMessage("日期不能为空");
            RuleFor(m => m.ActualPurchasePrice)
                 .NotEmpty().WithMessage("实际进价不能为空");
            RuleFor(m => m.Warehouse)
                 .NotEmpty().WithMessage("日期不能为空");
            RuleFor(m => m.Cost)
                 .NotEmpty().WithMessage("成本不能为空");
            RuleFor(m => m.SingleProfit)
                 .NotEmpty().WithMessage("单品利润不能为空");
            

        }
    }
}