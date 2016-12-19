using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.Framework.Configuration;

namespace Web.Framework.Mvc.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AllowCrossDomainLoginAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var origin = request.Headers.Get("Origin");

            if (origin != null)
            {
                var allowHost = LoginAddressConfiguration.FindHost(origin);
                if (allowHost != null)
                {
                    var response = filterContext.HttpContext.Response;

                    response.Headers.Add("Access-Control-Allow-Origin", allowHost);
                    response.Headers.Add("Access-Control-Allow-Credentials", "true");
                }
            }
        }
    }
}