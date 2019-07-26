using Autofac;
using Finchap.Account.Infrastructure.Exceptions;

namespace Finchap.Account.Infrastructure.AutofacModules
{
  public class InfrastructureModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterAssemblyTypes(typeof(InfrastructureException).Assembly)
          .AsImplementedInterfaces()
          .InstancePerLifetimeScope();
    }
  }
}