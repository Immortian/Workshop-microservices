using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Microservice.Commands.MoneyTransfer
{
    public class MoneyTransferCommand : IRequest
    {
        public int TransactionId { get; set; }
        public int SellerId { get; set; }
        public int BuyerId { get; set; }
        public decimal Price { get; set; }
    }
}
