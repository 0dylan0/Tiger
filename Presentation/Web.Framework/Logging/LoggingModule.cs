using Autofac;
using Autofac.Core;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Module = Autofac.Module;

namespace Web.Framework.Logging
{
    public class LoggingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log.ini");
            XmlConfigurator.Configure(fileInfo);
            builder.Register(CreateLogger).As<ILog>().InstancePerDependency();
        }

        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            registration.Preparing += OnComponentPreparing;
            registration.Activated += (sender, e) => InjectLoggerProperties(e.Instance);
        }

        private static void InjectLoggerProperties(object instance)
        {
            Type instanceType = instance.GetType();
            IEnumerable<PropertyInfo> properties = instanceType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.PropertyType == typeof(ILog) && p.CanWrite && p.GetIndexParameters().Length == 0);
            foreach (PropertyInfo propToSet in properties)
            {
                propToSet.SetValue(instance, LogManager.GetLogger(instanceType), null);
            }
        }

        private static void OnComponentPreparing(object sender, PreparingEventArgs e)
        {
            Type type = e.Component.Activator.LimitType;
            e.Parameters = e.Parameters.Union(new[] { new ResolvedParameter((p, i) => p.ParameterType == typeof(ILog), (p, i) => LogManager.GetLogger(type)) });
        }

        private static ILog CreateLogger(IComponentContext context, IEnumerable<Parameter> parameters)
        {
            var containingType = parameters.TypedAs<Type>();
            return LogManager.GetLogger(containingType);
        }
    }
}
