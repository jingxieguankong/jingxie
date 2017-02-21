namespace cn.tdr.policeequipment.module
{
    using System;
    using Autofac;

    public abstract class Module:IDisposable
    {
        public static readonly IContainer Container;

        static Module()
        {
            var builder = new ContainerBuilder();
            builder
                .RegisterType<data.oracle.EfRepository>()
                .As<interfaces.IRepository>();

            Container = builder.Build();
        }

        public interfaces.IRepository Repository { get; private set; }

        public ILifetimeScope Scope { get; private set; }

        protected Module()
        {
            Scope = Container.BeginLifetimeScope();
            Repository = Scope.Resolve<interfaces.IRepository>();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            Scope.Dispose();
            Repository.Dispose();
            Repository = null;
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
