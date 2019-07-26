using Autofac;
using Autofac.Extensions.DependencyInjection;
using Finchap.Account.Infrastructure;
using Finchap.Account.Infrastructure.AutofacModules;
using Finchap.Account.Infrastructure.Repositories;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Finchap.AdminCLI
{
    internal static class Program
    {
        public static IConfigurationSection AppConfiguration { get; set; }
        public static IConfigurationRoot ConfigurationRoot { get; set; }

        public static IServiceProvider Configure()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            ///
            /// Configuration
            ///
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            if (environmentName == "Development")
            {
                configBuilder = configBuilder.AddJsonFile("appsettings.Development.json", true);
            }
            ConfigurationRoot = configBuilder.Build();
            AppConfiguration = ConfigurationRoot.GetSection("AppConfiguration");

            ///
            /// Dependency injection
            ///
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(configure => configure.AddConsole());

            serviceCollection.AddDbContext<Context>(options =>
                options.UseSqlServer(ConfigurationRoot.GetConnectionString("AccountDatabase"))
                );

            // Register autofac modules
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(serviceCollection);
            containerBuilder.RegisterModule(new ApplicationModule());
            containerBuilder.RegisterModule(new InfrastructureModule());
            containerBuilder.RegisterModule(new CommandModule());
            containerBuilder.RegisterModule(new MediatorModule());

            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }

        private static int Main(string[] args)
        {
            var serviceProvider = Configure();
            var context = serviceProvider.GetService<Context>();
            context.Database.Migrate();

            var app = new CommandLineApplication
            {
                Name = "fc-admin",
                Description = "Finchap Admin Console"
            };

            app.HelpOption();

            var commands = serviceProvider.GetServices<ICommandLineCommand>();
            foreach (var command in commands)
            {
                command.Configure(app);
            }

            app.OnExecute(() =>
            {
                Console.WriteLine("Specify a subcommand");
                app.ShowHelp();
                return 1;
            });

            return app.Execute(args);
        }
    }
}