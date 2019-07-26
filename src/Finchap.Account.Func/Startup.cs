using Autofac;
using Autofac.Extensions.DependencyInjection;
using Finchap.Account.Infrastructure.AutofacModules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Finchap.Account.Func
{
  public static class Startup
  {
    ///<summary>
    /// Sets up the app before running any other code
    /// </summary>
    public static IServiceProvider Configure()
    {
      IServiceCollection serviceCollection = new ServiceCollection();
      serviceCollection.AddLogging(configure => configure.AddConsole());

      var configBuilder = new ConfigurationBuilder()
        .AddEnvironmentVariables();

      // Consume config info
      var configRoot = configBuilder.Build();
      //var cosmosDBSettings = new CosmosDBSettings();
      //configRoot.GetSection("AppConfiguration").Bind(cosmosDBSettings);

      // Register modules
      var containerBuilder = new ContainerBuilder();
      containerBuilder.Populate(serviceCollection);
      containerBuilder.RegisterModule(new ApplicationModule());
      containerBuilder.RegisterModule(new InfrastructureModule());

      return serviceCollection.BuildServiceProvider();
    }
  }
}