using MediatR;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Users.Microservice.Commands.CheckWalletBalance;
using Users.Microservice.Controllers.Base;

namespace Users.Microservice.RabbirListener
{
    public class RabbitListener : BaseController
    {
        IConfiguration Configuration;
        ConnectionFactory factory { get; set; }
        IConnection connection { get; set; }
        IModel channel { get; set; }
        private IMediator _mediator;
        [NonAction]
        public void Register()
        {
            channel.ExchangeDeclare(exchange: "topic_transaction", type: ExchangeType.Topic);
            channel.QueueDeclare(queue: "topic_transaction_queue", autoDelete: false);
            channel.QueueBind(queue: "topic_transaction_queue",
                              exchange: "topic_transaction",
                              routingKey: "#.WalletBalance.#");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                var messegeContent = JsonSerializer.Deserialize(message, typeof(CheckWalletbalanceCommand));
                Mediator.Send(messegeContent);
            };

            channel.BasicConsume(queue: "topic_transaction_queue", 
                                 autoAck: true, 
                                 consumer: consumer);
        }

        [NonAction]
        public void Deregister()
        {
            this.connection.Close();
        }

        public RabbitListener(IConfiguration configuration)
        {
            Configuration = configuration;
            this.factory = new ConnectionFactory() 
            {
                HostName = Configuration["RabbitMQ:HostName"],
                UserName = Configuration["RabbitMQ:UserName"],
                Password = Configuration["RabbitMQ:Password"],
                Port = int.Parse(Configuration["RabbitMQ:Port"])
            };

            this.connection = factory.CreateConnection();
            this.channel = connection.CreateModel();
        }
    }
}
