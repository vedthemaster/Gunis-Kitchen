
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
    }
}
