
using Gunis.Kitchen.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gunis.Kitchen.Controllers
{
    [Authorize(Roles = "User")]
    public class OrderController : Controller
    {

        private readonly ShoppingCart _shoppingCart;

        public OrderController(ShoppingCart shoppingCart)
        {
            
            _shoppingCart = shoppingCart;
        }
        public IActionResult Checkout()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var units = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = units;

            if(_shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError(" ", "Your cart is empty,add some items first.");
            }
            if (ModelState.IsValid)
            {
                _orderRepository.CreateOrder(order);
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
