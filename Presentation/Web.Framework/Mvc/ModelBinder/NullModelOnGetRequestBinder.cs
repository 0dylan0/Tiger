using System;
using System.Web.Mvc;

namespace Web.Framework.Mvc.ModelBinder
{
    public class NullModelOnGetRequestBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;
            if (request.RequestType == "GET" && request.QueryString.Count == 0)
            {
                if (bindingContext.ModelType.IsValueType)
                {
                    return Activator.CreateInstance(bindingContext.ModelType);
                }
                else
                {
                    return null;
                }
            }

            return base.BindModel(controllerContext, bindingContext);
        }
    }
}
