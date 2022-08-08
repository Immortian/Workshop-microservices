using System;
using System.Collections.Generic;

namespace ConfirmTransactions.Microservice
{
    public partial class TransactionConfirmation
    {
        public int TransactionId { get; set; }
        public int SellerId { get; set; }
        public int BuyerId { get; set; }
        public int? CollectionId { get; set; }
        public int? ItemId { get; set; }
        public decimal Price { get; set; }
        public bool? IsWalletBalanceOk { get; set; }
        public bool? IsItemOwnerOk { get; set; }
    }
}
