using System;
using System.Collections.Generic;

namespace Transactions.Microservice
{
    public partial class TransactionInfo
    {
        public int TransactionId { get; set; }
        public int SellerId { get; set; }
        public int BuyerId { get; set; }
        public int? CollectionId { get; set; }
        public int? ItemId { get; set; }
        public decimal Price { get; set; }
        public DateTime TransactionDatetime { get; set; }
    }
}
