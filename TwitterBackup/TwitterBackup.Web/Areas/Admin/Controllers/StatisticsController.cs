using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TwitterBackup.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Administrators")]
	public class StatisticsController : Controller
    {
		public IActionResult Index()
        {
            return View();
        }
    }
}