using Autofac;
using Core.Enum;
using Core.Infrastructure;
using Core.Infrastructure.DependencyManagement;
using Services.Common;
using Services.Security;
using Services.Tasks;
using Services.Users;
using Quartz;
using Quartz.Impl;
using Services.Test;

namespace WindowsService.Infrastructure
{
    public class DependencyRegistrar: IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {

            builder.Register(c => new StdSchedulerFactory().GetScheduler()).As<IScheduler>().SingleInstance();
            builder.RegisterType<ScheduleTaskService>().As<IScheduleTaskService>().InstancePerLifetimeScope();
            //builder.RegisterType<EncryptionService>().As<IEncryptionService>().InstancePerLifetimeScope();

            builder.RegisterType<UserValidateService>().InstancePerLifetimeScope();
            builder.RegisterType<ClaimsAuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();
            builder.RegisterType<HotelAuthenticationService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().InstancePerLifetimeScope();
            builder.RegisterType<HotelService>().InstancePerLifetimeScope();
            builder.RegisterType<UserHotelRangeService>().InstancePerLifetimeScope();
            builder.RegisterType<LicenseService>().InstancePerLifetimeScope();
            builder.RegisterType<GoodsDataService>().InstancePerLifetimeScope();
            builder.RegisterType<ClientDataService>().InstancePerLifetimeScope();
            builder.RegisterType<SupplierDataService>().InstancePerLifetimeScope();
            builder.RegisterType<PurchaseDataService>().InstancePerLifetimeScope();
            builder.RegisterType<SalesShipmentsDataService>().InstancePerLifetimeScope();
            builder.RegisterType<InventoryDataService>().InstancePerLifetimeScope();
            builder.RegisterType<WarehouseService>().InstancePerLifetimeScope();
            builder.RegisterType<GoodsSpecificationService>().InstancePerLifetimeScope();
            builder.RegisterType<GoodsTypeService>().InstancePerLifetimeScope();
            builder.RegisterType<ClientTypeService>().InstancePerLifetimeScope();
            builder.RegisterType<SupplierTypeService>().InstancePerLifetimeScope();
            builder.RegisterType<GoodsUnitService>().InstancePerLifetimeScope();
            builder.RegisterType<TransferCargoDataService>().InstancePerLifetimeScope();
            builder.RegisterType<ArrearsDataService>().InstancePerLifetimeScope();
            builder.RegisterType<ArrearsDetailsService>().InstancePerLifetimeScope();
            builder.RegisterType<SalesShipmentsStatisticsService>().InstancePerLifetimeScope();
            builder.RegisterType<ArrearsStatisticsService>().InstancePerLifetimeScope();

            builder.Register<EntryPoint>(c => EntryPoint.WindowsService).SingleInstance();

            builder.RegisterType<TestService>().As<ITestService>().InstancePerLifetimeScope();
            
        }

        public int Order => 1;
    }
}

