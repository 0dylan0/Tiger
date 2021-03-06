﻿using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Web.Validators;

namespace Web.Models
{
    public class ClientTypeListModel
    {
        [DisplayName("客户类别名")]
        public string Name { get; set; }

        public IList<ClientTypeModel> ClientType { get; set; }
    }
}