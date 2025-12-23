using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace OrdersService.Services;

public class RabbitMqPublisher
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMqPublisher()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public void Publish(string eventName, string payload)
    {
        _channel.QueueDeclare(queue: eventName, durable: false, exclusive: false, autoDelete: false, arguments: null);

        var body = Encoding.UTF8.GetBytes(payload);
        _channel.BasicPublish(exchange: "", routingKey: eventName, basicProperties: null, body: body);
    }
}