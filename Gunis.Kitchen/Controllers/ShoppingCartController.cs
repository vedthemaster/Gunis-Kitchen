using Gunis.Kitchen.Data;
using Gunis.Kitchen.Models;
using Gunis.Kitchen.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Gunis.Kitchen.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ShoppingCart _shoppingCart;
        public ShoppingCartController(ApplicationDbContext context, ShoppingCart shoppingCart)
        {
            _context = context;
            _shoppingCart = shoppingCart;

        }
        public ViewResult Index()
        {
            var units = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = units;
            var sCVM = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(sCVM);
        }

        public RedirectToActionResult AddToShoppingCart(int itemId)
        {
            var selectedItem = _context.Items.FirstOrDefault(p=>p.ItemId == itemId);
            if(selectedItem != null)
            {
                _shoppingCart.AddToCart(selectedItem, 1);
            }
            return RedirectToAction("Index");
        }
        public RedirectToActionResult RemoveFromShoppingCart(int itemId)
        {
            var selectedItem = _context.Items.FirstOrDefault(p=>p.ItemId == itemId);
            if (selectedItem != null)
            {
                _shoppingCart.RemoveFromCart(selectedItem);
            }
            return RedirectToAction("Index");
        }

    }
}
