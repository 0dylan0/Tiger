using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Validators
{
    public class SupplierDataValidator : AbstractValidator<SupplierDataModel>
    {
        public SupplierDataValidator()
        {
            RuleFor(m => m.SupplierName)
                 .NotEmpty().WithMessage("供应商名称不能为空");
            RuleFor(m => m.SupplierType)
                 .NotEmpty().WithMessage("供应商类别不能为空");

            RuleFor(m => m.Arrears)
                 .NotEmpty().WithMessage("欠款不能为空，没有则为0");
            RuleFor(m => m.RepaymentDate)
                 .NotEmpty().WithMessage("还款日期不能为空.没有则默认当前日期");
            RuleFor(m => m.Seq)
                 .NotEmpty().WithMessage("排序号不能为空");
        }
    }
}