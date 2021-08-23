using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PhoneBook.Repository
{
  public class ReportRequestRepository : IReportRequestRepository
  {
    private readonly string _hostname;
    private readonly string _password;
    private readonly string _vhost;
    private readonly string _username;

    public ReportRequestRepository(IOptions<RabbitMqConfiguration> rabbitMqOptions)
    {
      _hostname = rabbitMqOptions.Value.Hostname;
      _username = rabbitMqOptions.Value.UserName;
      _password = rabbitMqOptions.Value.Password;
      _vhost = rabbitMqOptions.Value.Vhost;
    }

    public Task Add(Location location)
    {
      var factory = new ConnectionFactory
      {
        HostName = _hostname,
        UserName = _username,
        Password = _password,
        VirtualHost = _vhost
      };

      return Task.Run(() =>
      {
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
          channel.QueueDeclare(queue: "report-request", durable: false, exclusive: false, autoDelete: false, arguments: null);

          string message = JsonSerializer.Serialize(location);
          var body = Encoding.UTF8.GetBytes(message);

          channel.BasicPublish(exchange: "", routingKey: "report-request", basicProperties: null, body: body);
        }
      });

    }


  }
}
