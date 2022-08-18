using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop.Shared.Models
{
    public class ItemModel
    {
        public int Id { get; set; }
        public string ItemName { get; set; } = null!;
        public string ItemType { get; set; } = null!;
        public decimal ItemPrice { get; set; }
        public string? ItemDescription { get; set; }
        public int? ItemCollectionId { get; set; }
        public int ItemOwnerId { get; set; }
    }
}
