using Autofac;
using Core.Infrastructure;
using FluentValidation.Mvc;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Web.App_Start;
using Web.Framework;
using Web.Framework.Configuration;
using Web.Framework.Mvc.Filters;

namespace Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            EngineContext.Initialize(false);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(new ValidatorFactory()));
            FilterProviders.Providers.Add(new IMFilterProvider());

            AntiForgeryConfig.CookieName = CookieNameConfiguration.AntiForgeryCookieName;
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            LogException(exception);
        }

        protected void LogException(Exception exc)
        {
            if (exc == null)
                return;
            try
            {
                var logger = EngineContext.Current.ContainerManager.Scope().Resolve<ILog>(new TypedParameter(typeof(Type), typeof(MvcApplication)));
                logger.Error(exc.Message, exc);
            }
            catch (Exception)
            {
            }
        }
    }
}
