using Gunis.Kitchen.Data;
using Gunis.Kitchen.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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

        public async void CreateOrder(Order order)
        {
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
        public async Task<IActionResult>Checkout(Order order)
        {
            var units = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = units;
            if (ModelState.IsValid)
            {
             var user =  await _userManager.GetUserAsync(this.User);
         
             order.OrderPlaced = DateTime.Now;

             order.Id = user.Id;
             order.OrderTotal = _shoppingCart.GetShoppingCartTotal();
             _applicationDbContext.Orders.Add(order);
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
