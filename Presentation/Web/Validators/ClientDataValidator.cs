using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Validators
{
    public class ClientDataValidator : AbstractValidator<ClientDataModel>
    {
        public ClientDataValidator()
        {

            RuleFor(m => m.ClientName)
                 .NotEmpty().WithMessage("客户名不能为空");
            RuleFor(m => m.ClientType)
                 .NotEmpty().WithMessage("客户类型不能为空");

            RuleFor(m => m.Arrears)
                .NotEmpty().WithMessage("欠款不能为空，可以为0");
            RuleFor(m => m.ReceiptDate)
                 .NotEmpty().WithMessage("日期不能为空，没有就默认当前日期");
            RuleFor(m => m.Seq)
                 .NotEmpty().WithMessage("排序号不能为空");
        }
    }
}