using Gunis.Kitchen.Data;
using Gunis.Kitchen.Models;
using Gunis.Kitchen.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Gunis.Kitchen.Controllers
{
    public class OrderHistoryController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<MyIdentityUser> _userManager;

        public OrderHistoryController(ApplicationDbContext applicationDbContext,UserManager<MyIdentityUser> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {

            var orderHVM = new OrderHistoryViewModel();
            var user = await _userManager.GetUserAsync(this.User);

            var order = _applicationDbContext.Orders.FirstOrDefault(o => o.OrderId == orderHVM.OrderId);

            return View(orderHVM);
        }
    }
}
