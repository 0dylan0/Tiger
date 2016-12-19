using Core;
using Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Web.Framework.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class HotelSelectCheckAttribute : FilterAttribute, IAuthorizationFilter
    {
        private readonly bool _ignore;

        public HotelSelectCheckAttribute(bool ignore = false)
        {
            _ignore = ignore;
            Order = 2;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            IWorkContext workContext = EngineContext.Current.Resolve<IWorkContext>();

                HandleHotelNotSelectRequest(filterContext);

        }

        private void HandleHotelNotSelectRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult(String.Format("~/Hotel/ChooseList?returnUrl={0}", HttpUtility.UrlEncode(filterContext.HttpContext.Request.Path)));
        }
    }
}
