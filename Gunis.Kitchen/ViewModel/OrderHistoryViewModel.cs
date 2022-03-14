using Gunis.Kitchen.Models;
using System.Collections;
using System.Collections.Generic;

namespace Gunis.Kitchen.ViewModel
{
    public class OrderHistoryViewModel
    {
        public IEnumerable<Order> Orders { get; set; }

        public  string OrderId { get; set; }

        public IEnumerable<OrderDetail> OrderDetails { get; set; }

    }
}
