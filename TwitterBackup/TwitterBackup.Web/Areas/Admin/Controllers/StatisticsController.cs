using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TwitterBackup.Web.Areas.Admin.Controllers
{
	public class StatisticsController : Controller
    {
		[Area("Admin")]
		[Authorize(Roles = "Administrators")]
		public IActionResult Index()
        {
            return View();
        }
    }
}