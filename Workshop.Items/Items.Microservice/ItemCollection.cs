using System;
using System.Collections.Generic;

namespace Items.Microservice
{
    public partial class ItemCollection
    {
        public ItemCollection()
        {
            Items = new HashSet<Item>();
        }

        public int CollectionId { get; set; }
        public int CollectionOwnerId { get; set; }
        public string? CollectionName { get; set; }
        public decimal CollectionPrice { get; set; }
        public string? CollectionDescription { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
