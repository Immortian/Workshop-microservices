using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Users.Microservice.Commands
{
    public class RabbitSender
    {
        private readonly IConfiguration Configuration;
        private readonly string _hostname;
        private readonly string _password;
        private readonly string _username;
        private readonly string _port;
        private IConnection _connection;

        public RabbitSender(IConfiguration configuration)
        {
            Configuration = configuration;
            _hostname = Configuration["RabbitMQ:HostName"];
            _username = Configuration["RabbitMQ:UserName"];
            _password = Configuration["RabbitMQ:Password"];
            _port = Configuration["RabbitMQ:Port"];

            CreateConnection();
        }
        /// <summary>
        /// Send messege using topic exchange
        /// </summary>
        /// <param name="value"></param>
        /// <param name="routingKey">topic exchange routing key</param>
        public void Send(object value, string routingKey)
        {
            if (ConnectionExists())
            {
                using (var channel = _connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: "topic_user", type: ExchangeType.Topic);

                    string _routingKey = routingKey;

                    string messege = JsonSerializer.Serialize(value, value.GetType());

                    var body = Encoding.UTF8.GetBytes(messege);

                    channel.BasicPublish(exchange: "topic_user",
                                         routingKey: routingKey,
                                         basicProperties: null,
                                         body: body);
                }
            }
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostname,
                    Port = int.Parse(_port),
                    UserName = _username,
                    Password = _password,
                    RequestedHeartbeat = new TimeSpan(0, 0, 60)
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not create connection: {ex.Message}");
            }
        }

        private bool ConnectionExists()
        {
            if (_connection != null)
            {
                return true;
            }

            CreateConnection();

            return _connection != null;
        }
    }
}

