namespace cn.tdr.policeequipment.module
{
    using Autofac;

    public abstract class Module
    {
        //private static readonly string 
        private static readonly IContainer Container;

        static Module()
        {
            var builder = new ContainerBuilder();
            builder
                .RegisterType<data.oracle.EfRepository>()
                .As<interfaces.IRepository>();

            Container = builder.Build();
        }

        protected interfaces.IRepository Repository { get; private set; }

        protected Module()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                Repository = scope.Resolve<interfaces.IRepository>();
            }
        }
    }
}
