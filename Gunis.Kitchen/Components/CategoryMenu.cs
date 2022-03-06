using Gunis.Kitchen.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gunis.Kitchen.Components
{
    public class CategoryMenu : ViewComponent
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public CategoryMenu(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _applicationDbContext.Categories.OrderBy(p => p.CategoryName);
            return View(categories);

        }
    }
}
