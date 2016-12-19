using log4net;
using System;
using System.Web.Mvc;
using IFilterProvider = Web.Framework.Mvc.Filters.IFilterProvider;
using Autofac;
using Core.Infrastructure;

namespace Web.Framework.Exceptions.Filters
{
    public class UnhandledExceptionFilter : IFilterProvider, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled && filterContext.Exception != null)
            {
                var logger = EngineContext.Current.ContainerManager.Scope().Resolve<ILog>(new TypedParameter(typeof(Type), filterContext.Controller.GetType()));
                logger.Error(filterContext.Exception.Message, filterContext.Exception);
            }
        }
    }
}
