using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gunis.Kitchen.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }

        public  int OrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public int ItemPrice { get; set; }
        
        [ForeignKey("ItemId")]
        public  Item Item { get; set; }

        [ForeignKey("OrderId")]
        public  Order Order { get; set; }    
    }
}
