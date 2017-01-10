using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Validators;

namespace Web.Models
{
    [Validator(typeof(ClientTypeValidator))]
    public class ClientTypeModel:BasicModel
    {
    }
}