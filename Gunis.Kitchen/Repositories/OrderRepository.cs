using Gunis.Kitchen.Data;
using Gunis.Kitchen.Interfaces;
using Gunis.Kitchen.Models;
using System;

namespace Gunis.Kitchen.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ShoppingCart _shoppingCart;

        public OrderRepository(ApplicationDbContext applicationDbContext, ShoppingCart shoppingCart)
        {
            _applicationDbContext = applicationDbContext;
            _shoppingCart = shoppingCart;
        }
        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;
            _applicationDbContext.Orders.Add(order);

            var shoppingCartItems  = _shoppingCart.ShoppingCartItems;

            foreach(var unit in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Quantity = unit.Quantity,
                    ItemId = unit.Item.ItemId,
                    OrderId = order.OrderId,
                    ItemPrice = unit.Item.ItemPrice

                };
                _applicationDbContext.OrderDetails.Add(orderDetail);
            }
            _applicationDbContext.SaveChanges();
        }
    }
}
