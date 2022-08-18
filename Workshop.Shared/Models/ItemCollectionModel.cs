using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop.Shared.Models
{
    public class ItemCollectionModel
    {
        public int CollectionId { get; set; }
        public int CollectionOwnerId { get; set; }
        public string? CollectionName { get; set; }
        public decimal CollectionPrice { get; set; }
        public string? CollectionDescription { get; set; }
        public int ItemCount { get; set; }
    }
}
