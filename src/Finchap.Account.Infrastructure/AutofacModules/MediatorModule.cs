using Autofac;
using Finchap.Account.Application.Commands.Register;
using MediatR;

namespace Finchap.Account.Infrastructure.AutofacModules
{
  public class MediatorModule : Autofac.Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterAssemblyTypes(typeof(IMediator).Assembly)
        .AsImplementedInterfaces();

      // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
      builder.RegisterAssemblyTypes(typeof(RegisterCommand).Assembly)
        .AsClosedTypesOf(typeof(IRequestHandler<,>));

      builder.RegisterAssemblyTypes(typeof(IMediator).Assembly).AsImplementedInterfaces();

      var mediatrOpenTypes = new[]
      {
        typeof(IRequestHandler<,>),
        typeof(INotificationHandler<>),
      };

      foreach (var mediatrOpenType in mediatrOpenTypes)
      {
        builder
          .RegisterAssemblyTypes(typeof(RegisterCommand).Assembly)
          .AsClosedTypesOf(mediatrOpenType)
          .AsImplementedInterfaces();
      }

      /*// It appears Autofac returns the last registered types first
      builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
      builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
      builder.RegisterGeneric(typeof(GenericRequestPreProcessor<>)).As(typeof(IRequestPreProcessor<>));
      builder.RegisterGeneric(typeof(GenericRequestPostProcessor<,>)).As(typeof(IRequestPostProcessor<,>));
      builder.RegisterGeneric(typeof(GenericPipelineBehavior<,>)).As(typeof(IPipelineBehavior<,>));
      builder.RegisterGeneric(typeof(ConstrainedRequestPostProcessor<,>)).As(typeof(IRequestPostProcessor<,>));
      builder.RegisterGeneric(typeof(ConstrainedPingedHandler<>)).As(typeof(INotificationHandler<>));*/

      builder.Register<ServiceFactory>(ctx =>
      {
        var c = ctx.Resolve<IComponentContext>();
        return t => c.Resolve(t);
      });

      // Make sure our mediator is the last registered
      builder.RegisterType<Mediator>().As<IMediator>();
    }
  }
}