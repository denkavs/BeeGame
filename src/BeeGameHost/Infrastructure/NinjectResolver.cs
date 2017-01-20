using GameLogic.Infrastructure;
using Ninject;

namespace BeeGameHost.Infrastructure
{
    public class NinjectResolver : BeeGameResolver
    {
        private IKernel kernel;

        public NinjectResolver() : base(new StandardKernel()) { }
        public NinjectResolver(IKernel ninjectKernel, bool scope = false):base(ninjectKernel, scope)
        {
        }
        protected override void AddDescendantBinding()
        {
        }

        protected override IKernel AddDescendantRequestBindings(IKernel kernel)
        {
            return kernel;
        }
    }
}
