using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfirmTransactions.Microservice.Commands.ConfirmTransaction
{
    public class Transaction
    {
        public int SellerId { get; set; }
        public int BuyerId { get; set; }
        public int? CollectionId { get; set; }
        public int? ItemId { get; set; }
        public decimal Price { get; set; }
    }
}
