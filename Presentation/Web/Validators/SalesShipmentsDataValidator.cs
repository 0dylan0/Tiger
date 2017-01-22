using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Validators
{
    public class SalesShipmentsDataValidator : AbstractValidator<SalesShipmentsDataModel>
    {
        public SalesShipmentsDataValidator()
        {
            RuleFor(m => m.Quantity)
                .NotEmpty().WithMessage("数量不能为空");
            RuleFor(m => m.UnitPrice)
                .NotEmpty().WithMessage("实际单价不能为空");
            RuleFor(m => m.ClientDataID)
                .NotEmpty().WithMessage("客户不能为空");
            RuleFor(m => m.GoodsName)
                .NotEmpty().WithMessage("商品名称不能为空");
            RuleFor(m => m.Date)
                .NotEmpty().WithMessage("送货日期不能为空");
        }
    }
}