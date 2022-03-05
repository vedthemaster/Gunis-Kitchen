using Gunis.Kitchen.Models;
using Gunis.Kitchen.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Gunis.Kitchen.Components
{
    public class ShoppingCartSummary :ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }
         public IViewComponentResult Invoke()
        {
            var units =  _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = units;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart =_shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal(),
            };
            return View(shoppingCartViewModel);
        }
    }
}
