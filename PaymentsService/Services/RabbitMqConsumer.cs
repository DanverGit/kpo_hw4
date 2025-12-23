using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace PaymentsService.Services;

public class RabbitMqConsumer
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMqConsumer()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public void StartConsuming(string queueName, Action<string> handleMessage)
    {
        _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            handleMessage(message);
        };

        _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
    }
}