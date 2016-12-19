using System;
using System.Web.Mvc;

namespace Web.Framework.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class IMAntiForgeryAttribute : FilterAttribute, IAuthorizationFilter
    {
        private readonly bool _ignore;

        public IMAntiForgeryAttribute(bool ignore = false)
        {
            _ignore = ignore;
            Order = 8;
        }

        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (_ignore)
            {
                return;
            }
            if (filterContext.IsChildAction)
            {
                return;
            }

            if (!String.Equals(filterContext.HttpContext.Request.HttpMethod, "POST", StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            var validator = new ValidateAntiForgeryTokenAttribute();
            validator.OnAuthorization(filterContext);
        }
    }
}

