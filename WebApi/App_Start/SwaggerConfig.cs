using System.Web.Http;
using WebActivatorEx;
using WebApi;
using Swashbuckle.Application;
using WebApi.App_Start;
using Web.Framework.WebAPI.Swashbuckle.Filters;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace WebApi
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    // 设置版本和接口描述
                    c.SingleApiVersion("v1", "会员忠诚度系统WebApi接口文档");

                    // 设置生成的xml文档的路径
                    c.IncludeXmlComments(GetXmlCommentsPath());

                    // 在接口类、方法标记属性 [HiddenApi]，可以阻止【Swagger文档】生成
                    c.DocumentFilter<HiddenApiFilter>();

                    // 通过调用CachingSwaggerProvider类设置显示控制器描述和文档缓存
                    c.CustomProvider((defaultProvider) => new CachingSwaggerProvider(defaultProvider));

                    // 避免同名类报错的问题（比如 MembershipCard 和 Profile 下都有名为 ChangePasswordRequest 的类，默认配置就会报错）
                    c.UseFullTypeNameInSchemaIds();

                    c.SchemaFilter<FluentValidationRules>();


                })
                .EnableSwaggerUi("docs/{*assetPath}", c =>
                {
                    // 设置翻译文件
                    // 路径规则，项目命名空间.文件夹名称.js文件名称
                    c.InjectJavaScript(thisAssembly, "WebApi.Scripts.swaggerui.swagger_lang.js");

                    // 不在swagger.io进行格式验证
                    c.DisableValidator();
                });
        }

        protected static string GetXmlCommentsPath()
        {
            var re = System.String.Format(@"{0}\App_Data\WebApi.XML", System.AppDomain.CurrentDomain.BaseDirectory);
            return re;
        }
    }
}
