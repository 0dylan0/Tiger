using Autofac;
using Core.Infrastructure;
using Core.Infrastructure.DependencyManagement;
using Services.Common;
using Services.Security;
using Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<UserValidateService>().InstancePerLifetimeScope();
            builder.RegisterType<ClaimsAuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();
            builder.RegisterType<HotelAuthenticationService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().InstancePerLifetimeScope();
            builder.RegisterType<HotelService>().InstancePerLifetimeScope();
            builder.RegisterType<UserHotelRangeService>().InstancePerLifetimeScope();
            builder.RegisterType<LicenseService>().InstancePerLifetimeScope();
            builder.RegisterType<GoodsDataService>().InstancePerLifetimeScope();
            builder.RegisterType<ClientDataService>().InstancePerLifetimeScope(); 

        }


        public int Order
        {
            get { return 1; }
        }
    }
}