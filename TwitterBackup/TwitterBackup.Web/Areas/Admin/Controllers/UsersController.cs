using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TwitterBackup.Data.Models;
using TwitterBackup.Data.Services.ServiceInterfaces;

namespace TwitterBackup.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Administrators")]
	public class UsersController : Controller
    {
		private readonly UserManager<User> userManager;
		private readonly RoleManager<IdentityRole> roleManager;
		private readonly IAdminUserService userService;


		public UsersController()
		{

		}
        public IActionResult Index()
        {
            return View();
        }

		// delete user - cascade, entities as well!

		// update user role
	}
}