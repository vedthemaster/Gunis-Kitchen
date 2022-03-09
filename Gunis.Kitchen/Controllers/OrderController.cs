using Gunis.Kitchen.ViewModel;
using Gunis.Kitchen.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Gunis.Kitchen.Data;
using System.Threading.Tasks;

namespace Gunis.Kitchen.Controllers
{
    [Authorize(Roles = "User")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        private readonly ShoppingCart _shoppingCart;

        public OrderController(ApplicationDbContext applicationDbContext,ShoppingCart shoppingCart)
        {
            _applicationDbContext = applicationDbContext;
            _shoppingCart = shoppingCart;
        }
        public IActionResult Checkout()
        {
            return View();
        }
        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;
            _applicationDbContext.Orders.Add(order);

            var shoppingCartItems = _shoppingCart.ShoppingCartItems;

            foreach (var unit in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Quantity = unit.Quantity,
                    ItemId = unit.Item.ItemId,
                    OrderId = order.Id,
                    ItemPrice = unit.Item.ItemPrice

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

            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError(" ", "Your cart is empty,add some items first.");
            }
            if (ModelState.IsValid)
            {
                CreateOrder(order);
                _shoppingCart.ClearCart();
                return RedirectToAction("CheckoutComplete");
            }
            return View(order);
        }



        //public async Task<IActionResult> Checkout(Order order)
        //{
        //    order.OrderPlaced = DateTime.Now;
        //    _applicationDbContext.Orders.Add(order);

        //    var shoppingCartItems = _shoppingCart.ShoppingCartItems;
        //   // List<Products> products = HttpContext.Session.Get<List<Products>>("products");

        //        foreach (var product in shoppingCartItems)
        //        {
        //            OrderDetails orderDetails = new OrderDetails();
        //            orderDetails.PorductId = product.Id;
        //            anOrder.OrderDetails.Add(orderDetails);
        //        }


        //    anOrder.OrderNo = GetOrderNo();
        //    _db.Orders.Add(anOrder);
        //    await _db.SaveChangesAsync();
        //    HttpContext.Session.Set("products", new List<Products>());
        //    return View();
        //}


        public IActionResult CheckoutComplete()
        {
            return View();
        }


    }
}
