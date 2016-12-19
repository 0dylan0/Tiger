using System;
using System.Collections.Generic;
using System.Linq;
using Core.Infrastructure.DependencyManagement;
using Autofac;
using System.Web.Mvc;
using Autofac.Integration.Mvc;

namespace Core.Infrastructure
{
    public class Engine : IEngine
    {
        private ContainerManager _containerManager;

        protected virtual void RunStartupTasks()
        {
            var typeFinder = _containerManager.Resolve<ITypeFinder>();
            var startUpTaskTypes = typeFinder.FindClassesOfType<IStartupTask>();
            var startUpTasks = new List<IStartupTask>();
            foreach (var startUpTaskType in startUpTaskTypes)
            {
                startUpTasks.Add((IStartupTask)Activator.CreateInstance(startUpTaskType));
            }
            //sort
            startUpTasks = startUpTasks.AsQueryable().OrderBy(st => st.Order).ToList();
            foreach (var startUpTask in startUpTasks)
            {
                startUpTask.Execute();
            }
        }

        protected virtual void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();

            //dependencies
            var typeFinder = new WebAppTypeFinder();
            builder = new ContainerBuilder();

            builder.RegisterInstance(this).As<IEngine>().SingleInstance();
            builder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();
            builder.Update(container);

            builder = new ContainerBuilder();
            var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
            var drInstances = new List<IDependencyRegistrar>();
            foreach (var drType in drTypes)
            {
                drInstances.Add((IDependencyRegistrar)Activator.CreateInstance(drType));
            }
            //sort
            drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
            foreach (var dependencyRegistrar in drInstances)
            {
                dependencyRegistrar.Register(builder, typeFinder);
            }
            builder.Update(container);

            _containerManager = new ContainerManager(container);

            //set dependency resolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        public void Initialize()
        {
            //register dependencies
            RegisterDependencies();
            RunStartupTasks();
        }

        public T Resolve<T>() where T : class
        {
            return ContainerManager.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return ContainerManager.Resolve(type);
        }

        public T[] ResolveAll<T>()
        {
            return ContainerManager.ResolveAll<T>();
        }

        public ContainerManager ContainerManager
        {
            get
            {
                return _containerManager;
            }
        }


    }
}
