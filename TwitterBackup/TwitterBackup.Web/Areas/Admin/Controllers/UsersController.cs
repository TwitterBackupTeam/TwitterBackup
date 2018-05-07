using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
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
		private readonly ICascadeDeleteEntityService cascadeDeleteEntityService;

		public UsersController(UserManager<User> userManager,
					RoleManager<IdentityRole> roleManager,
					IAdminUserService userService,
					ICascadeDeleteEntityService cascadeDeleteEntityService)
		{
			this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
			this.roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
			this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
			this.cascadeDeleteEntityService = cascadeDeleteEntityService ?? throw new ArgumentNullException(nameof(cascadeDeleteEntityService));
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> DeleteUser(string userId)
		{
			var user = await this.userManager.GetUserAsync(HttpContext.User);

			if (user.Id == userId)
			{
				return this.Json(false);
			}

			var userRoles = await this.userManager.GetRolesAsync(user);
			var userToBeDeleted = await this.userService.GetUserByUsernameAsync(userId);
			var userToDeleteRoles = await this.userManager.GetRolesAsync(userToBeDeleted);

			if (userToDeleteRoles.Contains("Administrator"))
			{
				return this.Json(false);
			}

			this.cascadeDeleteEntityService.CascadeDeleteUser(userToBeDeleted.Id);

			return this.Json(true);
		}
	}
}