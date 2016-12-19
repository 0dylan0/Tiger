using Core.Infrastructure;
using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Framework
{
    public class ValidatorFactory : AttributedValidatorFactory
    {
        public override IValidator GetValidator(Type type)
        {
            if (type != null)
            {
                var attribute = (ValidatorAttribute)Attribute.GetCustomAttribute(type, typeof(ValidatorAttribute));
                if ((attribute != null) && (attribute.ValidatorType != null))
                {
                    var instance = EngineContext.Current.ContainerManager.ResolveUnregistered(attribute.ValidatorType);
                    return instance as IValidator;
                }
            }
            return null;
        }
    }
}
