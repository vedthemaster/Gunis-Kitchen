using Microsoft.AspNetCore.Mvc;
using Gunis.Kitchen.Models;
using Gunis.Kitchen.Data;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using Gunis.Kitchen.ViewModel;

namespace Gunis.Kitchen.Controllers
{
    public class UserController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public UserController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Items
        public async Task<IActionResult> Index(string category)
        {
           var applicationDbContext = _context.Items.Include(i => i.Category);
            return View(await applicationDbContext.ToListAsync());
        }

    //    public ViewResult List(string category)
    //    {
    //        string _category = category;
    //        IEnumerable<Item> items;

    //        string currentCategory = string.Empty;

    //        if (string.IsNullOrEmpty(category))
    //        {
    //            items = _context.Items.OrderBy(n => n.ItemId);
    //            currentCategory = "All Items";

    //        }
    //        else
    //        {
    //            if (string.Equals("Pizza", _category, StringComparison.OrdinalIgnoreCase))
    //            {
    //                items = _context.Items.Where(p => p.Category.CategoryName.Equals("Pizza"));
    //            }
    //            else
    //            {
    //                items = _context.Items.Where(p => p.Category.CategoryName.Equals("Burger"));
    //            }
    //            currentCategory = _category;
    //        }

    //        var itemListViewModel = new ItemListViewModel
    //        {
    //            Items = items,
    //            CurrentCategory = currentCategory,

    //        };
    //        return View(itemListViewModel);
    //    }
    }
}
