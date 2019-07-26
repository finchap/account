using Autofac;
using Finchap.Account.Application;

namespace Finchap.Account.Infrastructure.AutofacModules
{
  public class ApplicationModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      //
      // Register all Types in Application
      //
      builder.RegisterAssemblyTypes(typeof(ApplicationException).Assembly)
          .AsImplementedInterfaces()
          .InstancePerLifetimeScope();
    }
  }
}