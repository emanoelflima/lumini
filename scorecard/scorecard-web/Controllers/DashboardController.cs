using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scorecard_web.Models;
using System.Diagnostics;

namespace scorecard_web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class DashboardController : Controller
    {
        /// <summary>
        /// Default method for main route
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Error page view
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
