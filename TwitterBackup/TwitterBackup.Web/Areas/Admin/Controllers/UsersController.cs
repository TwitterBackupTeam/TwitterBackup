using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TwitterBackup.Data.Models;
using TwitterBackup.Data.Services.ServiceInterfaces;

namespace TwitterBackup.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Administrator")]
	public class UsersController : Controller
    {
		private readonly UserManager<User> userManager;
		private readonly RoleManager<IdentityRole> roleManager;
		private readonly IAdminUserService userService;

		public UsersController(UserManager<User> userManager,
					RoleManager<IdentityRole> roleManager,
					IAdminUserService userService/*,
					ICascadeDeleteService cascadeDeleteService*/)
		{
			this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
			this.roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
			this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
			//this.cascadeDeleteService = cascadeDeleteService ?? throw new ArgumentNullException(nameof(cascadeDeleteService));
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> DeleteUser(string id)
		{
			var user = await this.userManager.GetUserAsync(HttpContext.User);

			if (user.Id == id)
			{
				return this.Json(false);
			}

			var userRoles = await this.userManager.GetRolesAsync(user);
			var userToDelete = await this.userService.GetUserByUsernameAsync(id);
			var userToDeleteRoles = await this.userManager.GetRolesAsync(userToDelete);

			if (userToDeleteRoles.Contains("Administrator"))
			{
				return this.Json(false);
			}

			//this.cascadeDeleteService.DeleteUserAndHisEntities(userToDelete.Id);

			return this.Json(true);
		}
	}
}