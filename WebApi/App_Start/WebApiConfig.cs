using Web.Framework.Exceptions;
using Web.Framework.WebAPI.Handler;
using Microsoft.Web.Http.Routing;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Routing;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var constraintResolver = new DefaultInlineConstraintResolver()
            {
                ConstraintMap =
                {
                    ["apiVersion"] = typeof( ApiVersionRouteConstraint )
                }
            };

            config.MapHttpAttributeRoutes(constraintResolver);

            config.AddApiVersioning();

            config.MessageHandlers.Add(new AddRequestIdHandler());
            config.MessageHandlers.Add(new LoggingHandler());

            config.Services.Add(typeof(IExceptionLogger), new GlobalErrorLogger());
        }
    }
}
