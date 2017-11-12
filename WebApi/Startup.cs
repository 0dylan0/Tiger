using IdentityServer3.AccessTokenValidation;
using WebApi;
using Web.Framework.Configuration;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                 //填写身份验证服务器的根 URL
                 //要验证填写的 URL 是否是根 URL，可以访问 URL/.well-known/openid-configuration
                 //如果能获取到资源列表，URL 就没有问题
                Authority = IdentityServerConfiguration.IdentityServerUrl,

                RequiredScopes = new[] { "Web_API" }
            });
        }
    }
}