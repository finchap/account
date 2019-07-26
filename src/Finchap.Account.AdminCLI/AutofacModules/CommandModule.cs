using Autofac;

namespace Finchap.AdminCLI
{
  public class CommandModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      //
      // Register all Types in Application
      //
      builder.RegisterAssemblyTypes(typeof(Program).Assembly)
          .AsImplementedInterfaces()
          .InstancePerLifetimeScope();
    }
  }
}