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
    public class RabbitListener
    {
        IConfiguration Configuration;
        IServiceScopeFactory Services { get; }
        ConnectionFactory factory { get; set; }
        IConnection connection { get; set; }
        IModel channel { get; set; }
        IMediator Mediator { get; set; }

        [NonAction]
        public void Register()
        {
            //channel.ExchangeDeclare(exchange: "topic_user", type: ExchangeType.Topic);
            //channel.QueueDeclare(queue: "topic_confirmation_WalletBalance_queue", autoDelete: false);
            //channel.QueueBind(queue: "topic_confirmation_WalletBalance_queue",
            //                  exchange: "topic_user",
            //                  routingKey: "#.Transaction.WalletBalance.#");
            channel.QueueDeclare(queue: "reply_confirmation_transaction_queue", autoDelete: false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                ConfirmationCommand messegeContent = (ConfirmationCommand)JsonSerializer.Deserialize(message, typeof(ConfirmationCommand));
                if (ea.BasicProperties.CorrelationId == "TransactionResult")
                {
                    //user notification
                }
                else
                {
                    using (var scope = Services.CreateScope())
                    {
                        var serviceProvider = scope.ServiceProvider;
                        var _context = serviceProvider.GetRequiredService<transactionconfirmationdbContext>();
                        var transaction = _context.TransactionConfirmations.Where(x => x.TransactionId == messegeContent.TransactionId).FirstOrDefault();
                        if (ea.BasicProperties.CorrelationId == "WalletBalanceConfirmation")
                            transaction.IsWalletBalanceOk = messegeContent.IsOk;
                        if (ea.BasicProperties.CorrelationId == "ItemOwnerConfirmation")
                            transaction.IsItemOwnerOk = messegeContent.IsOk;

                        _context.SaveChanges();

                        FinalCheck(transaction);
                    }
                }
            };

            channel.BasicConsume(queue: "reply_confirmation_transaction_queue", 
                                 autoAck: true, 
                                 consumer: consumer);
        }
        /// <summary>
        /// Check if all confirmation have passed. true: send mediatR request to run and save transaction
        /// </summary>
        /// <param name="transaction"></param>
        private void FinalCheck(TransactionConfirmation transaction)
        {
            if (transaction.IsItemOwnerOk && transaction.IsWalletBalanceOk)
            {
                using (var scope = Services.CreateScope())
                {
                    var serviceProvider = scope.ServiceProvider;
                    Mediator = serviceProvider.GetRequiredService<IMediator>();
                    Mediator.Send(transaction).Wait();
                }
            }
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
