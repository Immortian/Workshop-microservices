using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfirmTransactions.Microservice.Commands.SendConfirmedTransaction
{
    public class SendConfirmedTransactionCommand : IRequest
    {
        public int TransactionId { get; set; }
        public int SellerId { get; set; }
        public int BuyerId { get; set; }
        public int? CollectionId { get; set; }
        public int? ItemId { get; set; }
        public decimal Price { get; set; }
        public bool AreChecksPassed { get; set; }
    }
}
