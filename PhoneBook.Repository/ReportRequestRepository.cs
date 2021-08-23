using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PhoneBook.Repository.Entities;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PhoneBook.Repository
{
  public class ReportRequestRepository : IReportRequestRepository
  {
    private readonly DataContext _ctx;

    private readonly string _hostname;
    private readonly string _password;
    private readonly string _vhost;
    private readonly string _username;
    
    public ReportRequestRepository(DataContext ctx, IOptions<RabbitMqConfiguration> rabbitMqOptions)
    {
      _ctx = ctx;
      _hostname = rabbitMqOptions.Value.Hostname;
      _username = rabbitMqOptions.Value.UserName;
      _password = rabbitMqOptions.Value.Password;
      _vhost = rabbitMqOptions.Value.Vhost;
    }

    public async Task<ReportRequest> Get(Guid id)
    {
      var entity = await _ctx.ReportRequests.FindAsync(id);
      return entity;
    }

    public async Task<IEnumerable<ReportRequest>> GetAll()
    {
      var entities = await _ctx.ReportRequests.ToListAsync();
      return entities;
    }

    public async Task<Guid> Add(Location location)
    {
      var request = new ReportRequest
      {
        Id = Guid.NewGuid(),
        DateRequested = DateTime.Now,
        RequestLocation = JsonSerializer.Serialize<Location>(location),
        Status = ReportStatus.InProgress
      };

      _ctx.ReportRequests.Add(request);
      await _ctx.SaveChangesAsync();
      return request.Id;
    }

    public async Task PatchAsync(ReportPatch patch)
    {
      var report = _ctx.ReportRequests.Find(patch.Id);
      report.DateCompleted = patch.DateCompleted;
      report.cntPeopleNearBy = patch.cntPeopleNearBy;
      report.cntPhonesNearBy = patch.cntPhonesNearBy;
      report.Status = patch.Status;

      await _ctx.SaveChangesAsync();
    }

    public Task Request(Location location, Guid reportId, string contactApiUrl, string reportApiUrl)
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
          
          string message = JsonSerializer.Serialize(new MqPayload { Location = location, ReportId = reportId, ContactInfoApiUrl = contactApiUrl, ReportApiUrl = reportApiUrl }) ;
          var body = Encoding.UTF8.GetBytes(message);

          channel.BasicPublish(exchange: "", routingKey: "report-request", basicProperties: null, body: body);
        }
      });

    }


  }
}
