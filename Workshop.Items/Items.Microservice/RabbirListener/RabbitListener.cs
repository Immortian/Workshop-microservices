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
using Items.Microservice.Commands.CheckItemOwner;
using Items.Microservice.Commands.ItemTransfer;

namespace Items.Microservice.RabbirListener
{
    public class RabbitListener
    {
        IConfiguration Configuration;
        ConnectionFactory factory { get; set; }
        IConnection connection { get; set; }
        IModel channel { get; set; }
        IServiceProvider Services { get; set; }
        [NonAction]
        public void Register()
        {
            channel.ExchangeDeclare(exchange: "topic_transaction", type: ExchangeType.Topic);
            channel.QueueDeclare(queue: "topic_items_queue", autoDelete: false);
            channel.QueueBind(queue: "topic_items_queue",
                              exchange: "topic_transaction",
                              routingKey: "Items.#");

            channel.ExchangeDeclare(exchange: "topic_run_transaction", type: ExchangeType.Topic);
            channel.QueueBind(queue: "topic_items_queue",
                              exchange: "topic_run_transaction",
                              routingKey: "Items.#");
            
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var replyProps = channel.CreateBasicProperties();
                replyProps.CorrelationId = ea.BasicProperties.CorrelationId;
                replyProps.ReplyTo = ea.BasicProperties.ReplyTo;

                var message = Encoding.UTF8.GetString(body.ToArray());

                if (replyProps.CorrelationId == "ConfirmItemOwner")
                {
                    var messegeContent = (CheckItemOwnerCommand)JsonSerializer.Deserialize(message, typeof(CheckItemOwnerCommand));
                    messegeContent.Properties = replyProps;
                    using (var scope = Services.CreateScope())
                    {
                        var serviceProvider = scope.ServiceProvider;
                        Mediator = serviceProvider.GetRequiredService<IMediator>();
                        Mediator.Send(messegeContent).Wait();
                    }
                }
                else if(replyProps.CorrelationId == "ItemTransfer")
                {
                    var messegeContent = (ItemTransferCommand)JsonSerializer.Deserialize(message, typeof(ItemTransferCommand));
                    SendMessegeToMediator(messegeContent, typeof(ItemTransferCommand));
                }
            };

            channel.BasicConsume(queue: "topic_items_queue", 
                                 autoAck: true, 
                                 consumer: consumer);
        }

        private void SendMessegeToMediator(object messegeContent, Type contentType)
        {
            var content = Convert.ChangeType(messegeContent, contentType);
            using (var scope = Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                Mediator = serviceProvider.GetRequiredService<IMediator>();
                Mediator.Send(content).Wait();
            }
        }
        public void Deregister()
        {
            this.connection.Close();
        }
        private IMediator Mediator;
        public RabbitListener(IConfiguration configuration, IServiceProvider Services)
        {
            this.Services = Services;

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
