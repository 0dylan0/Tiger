using Core.Infrastructure.DependencyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure
{
    public interface IEngine
    {
        ContainerManager ContainerManager { get; }

        void Initialize();

        T Resolve<T>() where T : class;

        object Resolve(Type type);

        T[] ResolveAll<T>();
    }
}
