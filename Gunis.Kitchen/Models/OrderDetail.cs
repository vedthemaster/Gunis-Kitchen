using System.ComponentModel.DataAnnotations.Schema;

namespace Gunis.Kitchen.Models
{
    public class OrderDetail
    {

        public int OrderDetailId { get; set; }
        public string OrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public int ItemPrice { get; set; }

 
        public Order Order { get; set; }

        public Item Item { get; set; }


    }
}
