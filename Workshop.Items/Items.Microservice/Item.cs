using System;
using System.Collections.Generic;

namespace Items.Microservice
{
    public partial class Item
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; } = null!;
        public string ItemType { get; set; } = null!;
        public decimal ItemPrice { get; set; }
        public string? ItemDescription { get; set; }
        public int? ItemCollectionId { get; set; }
        public int ItemOwnerId { get; set; }

        public virtual ItemCollection? ItemCollection { get; set; }
    }
}
