using Finchap.Account.Application.Requests;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Finchap.Account.Infrastructure.Requests
{
  public class RequestPublisher : IRequestPublisher
  {
    private QueueClient _client;

    public RequestPublisher(string connectionString, string queueName)
    {
      _client = new QueueClient(connectionString, queueName);
    }

    public async Task PublishRequestAsync(IRequest command)
    {
      var message = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(command)))
      {
        ContentType = "application/json",
        Label = "Scientist",
        TimeToLive = TimeSpan.FromMinutes(2)
      };

      await _client.SendAsync(message);
    }
  }
}