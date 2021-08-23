
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using PhoneBook.Repository;
using System.Text.Json;
using System.Collections.Generic;
using PhoneBook.Repository.Entities;
using System.Linq;
using Newtonsoft.Json;

namespace PhoneBook.ReportHandler
{
  public partial class frmMain : Form
  {

    private IConnection _connection;
    private IModel _channel;

    public frmMain()
    {
      InitializeComponent();
      InitializeConsumer();
    }

    public void InitializeConsumer()
    {
      var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

      var factory = new ConnectionFactory()
      {
        HostName = configuration.GetSection("RabbitMq:HostName").Value,
        UserName = configuration.GetSection("RabbitMq:Username").Value,
        Password = configuration.GetSection("RabbitMq:Password").Value,
        VirtualHost = configuration.GetSection("RabbitMq:Vhost").Value
      };

      _connection = factory.CreateConnection();
      _channel = _connection.CreateModel();

      _channel.QueueDeclare(queue: "report-request", durable: false, exclusive: false, autoDelete: false, arguments: null);
      _logger("Waiting for messages.");

      var consumer = new EventingBasicConsumer(_channel);
      consumer.Received += (model, ea) =>
      {
        var mqPayload = Encoding.UTF8.GetString(ea.Body.ToArray());
        
        HandleReportRequest(mqPayload);
        _logger(mqPayload);
      };
      _channel.BasicConsume(queue: "report-request", autoAck: true, consumer: consumer);
    }

    private void HandleReportRequest(string mqPayload)
    {
      var payload = JsonConvert.DeserializeObject<MqPayload>(mqPayload);

      var client = new HttpClient();
      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

      var contactsJson = client.GetStringAsync(payload.ContactInfoApiUrl).Result;
      var contacts = JsonConvert.DeserializeObject<List<ContactInfo>>(contactsJson);

      var contactsNearBy  = contacts.Where(x => !string.IsNullOrEmpty(x.Info) && x.ContactInfoType == 8 && CalculateDistance(payload.Location, x.Info) < 10)
                                    .GroupBy(x => x.PersonId)
                                    .Select(x => new { Id = x.Key } )
                                    .ToList();
      var cntPeopleNearBy = contactsNearBy.Count;
      
      var phones = contacts.Where(x => !string.IsNullOrEmpty(x.Info) && x.ContactInfoType == 2);
      var q = from n in contactsNearBy
              join p in phones on n.Id equals p.PersonId into ps
              where ps.Any()
              select new { Key = n.Id };
      var cntPhonesNearBy = q.ToList().Count;

      var patch = new ReportPatch {Id= payload.ReportId, DateCompleted = DateTime.Now, Status = ReportStatus.Completed
        , cntPeopleNearBy = cntPeopleNearBy, cntPhonesNearBy = cntPhonesNearBy};

      var patchJson = JsonConvert.SerializeObject(patch);
      var requestContent = new StringContent(patchJson, Encoding.UTF8, "application/json-patch+json");
      var response = client.PatchAsync(payload.ReportApiUrl, requestContent).Result;
      response.EnsureSuccessStatusCode();
    }

    public double CalculateDistance(Location point1, string location)
    {
      var point2 = JsonConvert.DeserializeObject<Location>(location);
      return CalculateDistance(point1, point2);
    }

    public double CalculateDistance(Location point1, Location point2)
    {
      var oneDegree = Math.PI / 180.0;
      var d1 = point1.Latitude * (oneDegree);
      var num1 = point1.Longitude * (oneDegree);
      var d2 = point2.Latitude * (oneDegree);
      var num2 = point2.Longitude * (oneDegree) - num1;

      var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) +
               Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
      return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
    }



    private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
    {
      _channel.Close();
      _connection.Close();
    }

    public void _logger(string x)
    {
      if (txtLog.InvokeRequired)
      {
        txtLog.Invoke(new Action<string>(x => _logger(x)), x);
      }
      else
      {
        txtLog.AppendText($"{DateTime.Now:dd.MM.yyyy HH:mm:ss} {x}{Environment.NewLine}");
      }
    }
  }
}
