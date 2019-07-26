using System.Threading.Tasks;

namespace Finchap.Account.Application.Requests
{
  public interface IRequestPublisher
  {
    Task PublishRequestAsync(IRequest command);
  }
}