
using System;
using System.IO;
using System.Windows.Forms;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Configuration;
using System.Text;

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
        HostName = configuration.GetSection("RabbitMQ:HostName").Value,
        UserName = configuration.GetSection("RabbitMQ:Username").Value,
        Password = configuration.GetSection("RabbitMQ:Password").Value,
        VirtualHost = configuration.GetSection("RabbitMQ:Vhost").Value,
      };

      _connection = factory.CreateConnection();
      _channel = _connection.CreateModel();

      _channel.QueueDeclare(queue: "report-request", durable: false, exclusive: false, autoDelete: false, arguments: null);
      _logger("Waiting for messages.");

      var consumer = new EventingBasicConsumer(_channel);
      consumer.Received += (model, ea) =>
      {
        var message = Encoding.UTF8.GetString(ea.Body.ToArray());
        _logger(message);
      };
      _channel.BasicConsume(queue: "report-request", autoAck: true, consumer: consumer);
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
