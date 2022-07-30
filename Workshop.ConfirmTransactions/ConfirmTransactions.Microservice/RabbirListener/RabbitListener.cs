using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ConfirmTransactions.Microservice.Controllers.Base;
using ConfirmTransactions.Microservice.Commands.ConfirmTransaction;
using Microsoft.AspNetCore.Mvc;

namespace ConfirmTransactions.Microservice.RabbirListener
{
    public class RabbitListener : BaseController
    {
        IConfiguration Configuration;
        IServiceScopeFactory Services { get; }
        ConnectionFactory factory { get; set; }
        IConnection connection { get; set; }
        IModel channel { get; set; }

        [NonAction]
        public void Register()
        {
            channel.ExchangeDeclare(exchange: "topic_user", type: ExchangeType.Topic);
            channel.QueueDeclare(queue: "topic_confirmation_WalletBalance_queue", autoDelete: false);
            channel.QueueBind(queue: "topic_confirmation_WalletBalance_queue",
                              exchange: "topic_user",
                              routingKey: "#.Transaction.WalletBalance.#");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                ConfirmationCommand messegeContent = (ConfirmationCommand)JsonSerializer.Deserialize(message, typeof(ConfirmationCommand));
                using (var scope = Services.CreateScope())
                {
                    var serviceProvider = scope.ServiceProvider;
                    var _context = serviceProvider.GetRequiredService<transactionconfirmationdbContext>();
                    _context.TransactionConfirmations.Where(x => x.TransactionId == messegeContent.TransactionId).FirstOrDefault().IsWalletBalanceOk = true;
                    _context.SaveChanges();
                }
                
            };

            channel.BasicConsume(queue: "topic_confirmation_WalletBalance_queue", 
                                 autoAck: true, 
                                 consumer: consumer);
        }

        [NonAction]
        public void Deregister()
        {
            this.connection.Close();
        }

        public RabbitListener(IConfiguration configuration, IServiceScopeFactory services)
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
            Services = services;
        }
    }
}
