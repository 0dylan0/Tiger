using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Web.Validators;

namespace Web.Models
{
    [Validator(typeof(WarehouseValidator))]
    public class WarehouseModel
    {
        public int ID { get; set; }

        [DisplayName("仓库名")]
        public string Name { get; set; }
    }
}