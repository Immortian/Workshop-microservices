using MediatR;
using RabbitMQ.Client;

namespace Transactions.Microservice.Commands.TransactionExecution
{
    public class ConfirmedTransaction : IRequest
    {
        public int TransactionId { get; set; }
        public int SellerId { get; set; }
        public int BuyerId { get; set; }
        public int? CollectionId { get; set; }
        public int? ItemId { get; set; }
        public decimal Price { get; set; }
        public bool AreChecksPassed { get; set; }
        public IBasicProperties Properties { get; set; }
    }
}
