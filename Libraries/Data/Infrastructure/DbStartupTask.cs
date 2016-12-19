using Core.Infrastructure;
using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data.Infrastructure
{
    public class DbStartupTask : IStartupTask
    {
        public void Execute()
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes().Where(type => !String.IsNullOrEmpty(type.Namespace)).Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(Dapper.FluentMap.Dommel.Mapping.DommelEntityMap<>));
            FluentMapper.Initialize(config =>
            {
                foreach (var type in typesToRegister)
                {
                    dynamic configurationInstance = Activator.CreateInstance(type);
                    config.AddMap(configurationInstance);
                }

                config.ForDommel();
            });
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
