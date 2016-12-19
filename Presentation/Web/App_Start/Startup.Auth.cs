using Web.Framework.Configuration;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace Web
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Index"),
                LogoutPath = new PathString("/Index"),
                CookieName = CookieNameConfiguration.AuthenticationCookieName
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
        }
    }
}