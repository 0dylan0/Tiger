using FluentValidation;
using FluentValidation.Attributes;
using FluentValidation.Validators;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Framework.WebAPI.Swashbuckle.Filters
{
    public class FluentValidationRules : ISchemaFilter
    {
        public void Apply(Schema schema, SchemaRegistry schemaRegistry, Type type)
        {
            var validatorAttribute = type.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(ValidatorAttribute));
            if (validatorAttribute != null)
            {

                Type validatorTypeDef = validatorAttribute.ConstructorArguments.First().Value as Type;
                dynamic validator = validatorTypeDef.Assembly.CreateInstance(validatorTypeDef.FullName);

                schema.required = new List<string>();

                IValidatorDescriptor validatorDescriptor = validator.CreateDescriptor();

                if (schema.properties != null)
                {
                    foreach (var key in schema.properties.Keys)
                    {
                        var memberValidators = validatorDescriptor.GetMembersWithValidators().Where(i => i.Key.Equals(key, StringComparison.OrdinalIgnoreCase));

                        foreach (var validatorType in memberValidators)
                        {
                            if (validatorType.Any(v => v is NotEmptyValidator))
                            {
                                schema.required.Add(key);
                            }
                        }
                    }
                }
            }
        }
    }
}

