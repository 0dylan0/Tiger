using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Validators
{
    public class PurchaseDataValidator : AbstractValidator<PurchaseDataModel>
    {
        public PurchaseDataValidator()
        {
            
            RuleFor(m => m.GoodsID)
                 .NotEmpty().WithMessage("商品不能为空");
            RuleFor(m => m.Date)
                 .NotEmpty().WithMessage("日期不能为空");
            RuleFor(m => m.Specification)
                 .NotEmpty().WithMessage("商品规格不能为空");
            RuleFor(m => m.GoodsType)
                 .NotEmpty().WithMessage("商品类别不能为空");
            RuleFor(m => m.Quantity)
                 .NotEmpty().WithMessage("数量不能为空");
            RuleFor(m => m.UnitPrice)
                 .NotEmpty().WithMessage("成本价不能为空");
            RuleFor(m => m.Sum)
                 .NotEmpty().WithMessage("总金额不能为空");
            RuleFor(m => m.SupplierID)
                 .NotEmpty().WithMessage("供应商不能为空");
            RuleFor(m => m.WarehouseID)
                 .NotEmpty().WithMessage("仓库不能为空");
            RuleFor(m => m.Freight)
                 .NotEmpty().WithMessage("仓库不能为空");            
        }
    }
}