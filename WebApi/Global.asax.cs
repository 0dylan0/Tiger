using Autofac;
using FluentValidation.WebApi;
using Core.Infrastructure;
using Web.Framework;
using Web.Framework.Mvc.Filters;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApi.App_Start;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            EngineContext.Initialize(false);
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            FluentValidationModelValidatorProvider.Configure(GlobalConfiguration.Configuration, p => p.ValidatorFactory = new ValidatorFactory());
            FilterProviders.Providers.Add(new IMFilterProvider());

            var jsonSerializerSettings = GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings;
            var xmlSerializerSettings = GlobalConfiguration.Configuration.Formatters.XmlFormatter;
            // 忽略循环引用
            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            // 并采用驼峰命名法
            jsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            // 枚举输出为 String
            jsonSerializerSettings.Converters.Add(new StringEnumConverter(true));
            // 时间格式指定为local
            jsonSerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;

            jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;

            // 设置 XML 序列化器使用 XmlSerializer 而非 DataContractSerializer
            // 这样可以自定义集合内的节点名称，而不会生成诸如 <d2p1:string> 这样的节点
            xmlSerializerSettings.UseXmlSerializer = true;
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
                var logger = EngineContext.Current.ContainerManager.Scope().Resolve<ILog>(new TypedParameter(typeof(Type), typeof(WebApiApplication)));
                logger.Error(exc.Message, exc);
            }
            catch (Exception)
            {
            }
        }
    }
}

