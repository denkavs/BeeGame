using GameLogic.Impl;
using GameLogic.Interfaces;
using Ninject;
using Ninject.Extensions.ChildKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

namespace GameLogic.Infrastructure
{
    public class BeeGameResolver : IDependencyResolver
    {
        private IKernel kernel;

        public BeeGameResolver() : this(new StandardKernel()) { }
        public BeeGameResolver(IKernel ninjectKernel, bool scope = false)
        {
            kernel = ninjectKernel;
            if (!scope)
            {
                AddBindings(kernel);
            }
        }
        public IDependencyScope BeginScope()
        {
            return new BeeGameResolver(AddRequestBindings(new ChildKernel(kernel)), true);
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        public void Dispose()
        {
            // do nothing
        }
        private void AddBindings(IKernel kernel)
        {
            // singleton and transient bindings go here
            kernel.Bind<IBeeCatcher>().To<BeeCatcher>();
            kernel.Bind<IConfiguration>().To<Configuration>();
            kernel.Bind<IGameFactory>().To<GameFactory>();
            kernel.Bind<IRepository>().To<Repository>().InSingletonScope();
            kernel.Bind<IGamePlayEngine>().To<GamePlayEngine>();
            kernel.Bind<IService>().To<GameLogic.Model.Service>();

            AddDescendantBinding();
        }
        private IKernel AddRequestBindings(IKernel kernel)
        {
            // request object scope bindings go here
            return AddDescendantRequestBindings(kernel);
        }

        protected virtual void AddDescendantBinding()
        {
        }

        protected virtual IKernel AddDescendantRequestBindings(IKernel kernel)
        {
            return kernel;
        }
    }
}
