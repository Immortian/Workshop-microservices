using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transactions.Microservice.Commands
{
    public class MoneyTransferCommand
    {
        public int TransactionId { get; set; }
        public int SellerId { get; set; }
        public int BuyerId { get; set; }
        public decimal Price { get; set; }
        public bool? Success { get; set; }
    }
}
