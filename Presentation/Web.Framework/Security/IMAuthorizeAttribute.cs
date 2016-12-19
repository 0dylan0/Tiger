using Core.Infrastructure;
using Services.Security;
using System;
using System.Web.Mvc;

namespace Web.Framework.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class IMAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        private readonly string _permission;

        public IMAuthorizeAttribute(string permission)
        {
            _permission = permission;
            Order = 4;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException(nameof(filterContext));
            }
            if (filterContext.IsChildAction)
            {
                return;
            }
            var permissionService = EngineContext.Current.Resolve<PermissionService>();
            if (!permissionService.Authorize(_permission))
            {
                ViewResult result = new ViewResult
                {
                    ViewName = "AccessDenied"
                };
                filterContext.Result = result;
            }
        }
    }
}
