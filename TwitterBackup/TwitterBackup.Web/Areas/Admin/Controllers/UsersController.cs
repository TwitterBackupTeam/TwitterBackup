using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TwitterBackup.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Administrators")]
	public class UsersController : Controller
    {
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