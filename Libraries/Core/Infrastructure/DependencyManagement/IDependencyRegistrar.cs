using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.DependencyManagement
{
    public interface IDependencyRegistrar
    {
        void Register(ContainerBuilder builder, ITypeFinder typeFinder);

        int Order { get; }
    }
}
