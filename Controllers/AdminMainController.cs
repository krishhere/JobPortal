using ClinetraSolutions.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ClinetraSolutions.Controllers
{
    public class AdminMainController : Controller
    {
        public IActionResult Index()
        {
            DataTable allCourses = DbData.GetAllCourses();
            if (allCourses != null && allCourses.Rows.Count > 0)
            {

            }
            return View();
        }
        [HttpPost]
        public JsonResult SetNewBatch(IFormCollection form)
        {
            try
            {
                string courseId = form["CourseId"];
                string batchStartDate = form["BatchDate"];
                string courseName = form["CourseName"];
                bool flag = DbData.SetNewBatch(courseId, batchStartDate);
                if (flag)
                {
                    return Json(new { message = $"New batch dates for '{courseName}' have been set successfully!" });
                }
                else
                {
                    return Json(new { message = $"Failed to set new batch dates for '{courseName}'." });
                }
            }
            catch
            {
                return Json(new { message = "An error occurred. Contact developer" });
            }
        }
    }
}