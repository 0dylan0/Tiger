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
                    // ���ð汾�ͽӿ�����
                    c.SingleApiVersion("v1", "��Ա�ҳ϶�ϵͳWebApi�ӿ��ĵ�");

                    // �������ɵ�xml�ĵ���·��
                    c.IncludeXmlComments(GetXmlCommentsPath());

                    // �ڽӿ��ࡢ����������� [HiddenApi]��������ֹ��Swagger�ĵ�������
                    c.DocumentFilter<HiddenApiFilter>();

                    // ͨ������CachingSwaggerProvider��������ʾ�������������ĵ�����
                    c.CustomProvider((defaultProvider) => new CachingSwaggerProvider(defaultProvider));

                    // ����ͬ���౨������⣨���� MembershipCard �� Profile �¶�����Ϊ ChangePasswordRequest ���࣬Ĭ�����þͻᱨ��
                    c.UseFullTypeNameInSchemaIds();

                    c.SchemaFilter<FluentValidationRules>();


                })
                .EnableSwaggerUi("docs/{*assetPath}", c =>
                {
                    // ���÷����ļ�
                    // ·��������Ŀ�����ռ�.�ļ�������.js�ļ�����
                    c.InjectJavaScript(thisAssembly, "WebApi.Scripts.swaggerui.swagger_lang.js");

                    // ����swagger.io���и�ʽ��֤
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
