using Gunis.Kitchen.Data;
using Gunis.Kitchen.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace Gunis.Kitchen.Controllers
{
    public class OrderController : Controller
    {
        private readonly UserManager<MyIdentityUser> _userManager;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ShoppingCart _shoppingCart;

        public OrderController(
            ApplicationDbContext applicationDbContext, 
            ShoppingCart shoppingCart,
            UserManager<MyIdentityUser> userManager)
        {
            _shoppingCart = shoppingCart;
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }
        public IActionResult Checkout()
        {
            return View();
        }

        public void CreateOrder(Order order)
        {
            //var user =  _userManager.GetUserAsync(this.User);
            //var NewOrderId = user.Id + DateTime.Now.ToString("yymmssfff");
            //order.OrderId = NewOrderId;
          
            order.OrderPlaced = DateTime.Now;
            _applicationDbContext.Orders.Add(order);
            var shoppingCartItems = _shoppingCart.ShoppingCartItems;
          
          


            foreach (var unit in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Quantity = unit.Quantity,
                    ItemId = unit.Item.ItemId,
                    ItemPrice = unit.Item.ItemPrice,
                    OrderId= order.OrderId,

                };
                _applicationDbContext.OrderDetails.Add(orderDetail);
            }
            _applicationDbContext.SaveChanges();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var units = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = units;
            if (ModelState.IsValid)
            {
                CreateOrder(order);
                _shoppingCart.ClearCart();
                return RedirectToAction("CheckoutComplete");
            }
            return View(order);

        }
        public IActionResult CheckoutComplete()
        {
            return View();
        }
    }
}
