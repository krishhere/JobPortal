using ClinetraSolutions.Models;
using ClinetraSolutions.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ClinetraSolutions.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var userSummary = DbData.GetUserCountByType();
            var categorySummary = DbData.GetJobPostCountByCategory();

            ViewBag.UserSummary = JsonConvert.SerializeObject(userSummary);
            ViewBag.CategorySummary = JsonConvert.SerializeObject(categorySummary);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
