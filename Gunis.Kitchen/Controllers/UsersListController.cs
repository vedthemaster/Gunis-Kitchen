using Gunis.Kitchen.Data;
using Gunis.Kitchen.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Gunis.Kitchen.ViewModel;

namespace Gunis.Kitchen.Controllers
{
    public class UsersListController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<MyIdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UsersListController(
            ILogger<UserController> logger,
            UserManager<MyIdentityUser> userManager,
            ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = from user in _context.Users
                        select user;
            List<UserListViewModel> viewModelList = new List<UserListViewModel>();
            foreach(var user in users)
            {
                var viewModel = new UserListViewModel
                {
                    Id = user.Id,
                    UserName= user.UserName,
                    Email= user.Email,
                    Name= user.Name,
                    RolesOfUser = await _userManager.GetRolesAsync(user) as List<string>

                };
                viewModelList.Add(viewModel);

            }

            return View(viewModelList);
        }
    }
}
