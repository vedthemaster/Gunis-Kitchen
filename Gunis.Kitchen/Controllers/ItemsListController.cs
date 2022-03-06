using Gunis.Kitchen.Data;
using Gunis.Kitchen.Models;
using Gunis.Kitchen.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gunis.Kitchen.Controllers
{
    public class ItemsListController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ItemsListController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ViewResult List(string category)
        {
            //var applicationDbContext = _context.Items.Include(i => i.Category);
            string _category = category;
            IEnumerable<Item> items;

            string currentCategory = string.Empty;
            if (string.IsNullOrEmpty(_category))
            {
                items = _context.Items.OrderBy(p => p.ItemId);
                currentCategory = "All Items";
            }
            else
            {
                if (string.Equals("Pizza",_category, StringComparison.OrdinalIgnoreCase))
                {
                    items = _context.Items.Where(p => p.Category.CategoryName.Equals("Pizza")).OrderBy(p => p.ItemName);

                }
                else
                {
                    items = _context.Items.Where(p => p.Category.CategoryName.Equals("Burger")).OrderBy(p => p.ItemName);

                }
                    
                currentCategory = _category;
            }
            var itemListViewModel = new ItemListViewModel
            {
                Items = items,
                CurrentCategory = currentCategory

            };
            return View(itemListViewModel);
        }
    }
}
