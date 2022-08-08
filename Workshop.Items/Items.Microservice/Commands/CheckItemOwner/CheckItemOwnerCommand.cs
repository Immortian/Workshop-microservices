using MediatR;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Items.Microservice.Commands.CheckItemOwner
{
    public class CheckItemOwnerCommand : IRequest
    {
        public int TransactionId { get; set; }
        public int SellerId { get; set; }
        public int? CollectionId { get; set; }
        public int? ItemId { get; set; }
        public IBasicProperties Properties { get; set; }
    }
}
