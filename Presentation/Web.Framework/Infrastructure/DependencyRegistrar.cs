using System.Data;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Core;
using Core.Caching;
using Core.Infrastructure;
using Core.Infrastructure.DependencyManagement;
using Data;
using Services.Localization;
using Web.Framework.Exceptions.Filters;
using Web.Framework.Mvc.Filters;
using System.Linq;
using System.Web;
using LoggingModule = Web.Framework.Logging.LoggingModule;
using Core.Data;
using Core.Fakes;
using Microsoft.Owin;

namespace Web.Framework.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterModule(new LoggingModule());

            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());

            //builder.Register(c => new HttpContextWrapper(HttpContext.Current) as HttpContextBase).As<HttpContextBase>().InstancePerLifetimeScope();
            builder.Register(c =>
               HttpContext.Current != null ?
               (new HttpContextWrapper(HttpContext.Current) as HttpContextBase) :
               (new FakeHttpContext("~/") as HttpContextBase)).As<HttpContextBase>().InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Request).As<HttpRequestBase>().InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Response).As<HttpResponseBase>().InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Server).As<HttpServerUtilityBase>().InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Session).As<HttpSessionStateBase>().InstancePerLifetimeScope();

            //builder.Register(c => c.Resolve<HttpContextBase>().GetOwinContext().Authentication).InstancePerLifetimeScope();
            builder.Register(c =>
            {
                if (c.Resolve<HttpContextBase>().Items["owin.Environment"] == null)
                {
                    return new OwinContext().Authentication;
                }

                return c.Resolve<HttpContextBase>().GetOwinContext().Authentication;
            }).InstancePerLifetimeScope();

            builder.RegisterType<UnhandledExceptionFilter>().As<IFilterProvider>().InstancePerLifetimeScope();
            

            //builder.Register<IDbConnection>(c => new System.Data.SqlClient.SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["KunlunConnection"].ConnectionString)).InstancePerLifetimeScope();


            builder.Register<IDbConnection>(c =>
            {
                return (EngineContext.Current.Resolve<IDbContext>() as IMObjectContext).Database.Connection;
            }).InstancePerLifetimeScope();
            builder.Register<IDbConnection>(c =>
            {
                var connection = new System.Data.SqlClient.SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["KunlunConnection"].ConnectionString);
                connection.Open();
                return connection;

            }).Named<IDbConnection>("SignalRDbConnection").InstancePerLifetimeScope();

            builder.RegisterType<DapperRepository>().InstancePerLifetimeScope();
            builder.RegisterType<DbHelper>().InstancePerLifetimeScope();

            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().Named<ICacheManager>("kunlun_cache_static").SingleInstance();
            builder.RegisterType<PerRequestCacheManager>().As<ICacheManager>().Named<ICacheManager>("kunlun_cache_per_request").InstancePerLifetimeScope();

            builder.RegisterType<LocalizationService>().WithParameter(ResolvedParameter.ForNamed<ICacheManager>("kunlun_cache_static")).InstancePerLifetimeScope();
            builder.RegisterType<LanguageService>().InstancePerLifetimeScope();
            builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.Register<IDbContext>(c => new IMObjectContext()).InstancePerLifetimeScope();
        }

        public int Order
        {
            get
            {
                return 0;
            }
        }
    }
}
