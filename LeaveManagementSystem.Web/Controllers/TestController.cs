using Microsoft.AspNetCore.Mvc;

namespace LeaveManagementSystem.Web.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            var data= new Models.TestViewModel
            {
                Name = "Learner",
                DOB = new DateTime(1990, 1, 1)
            };
            return View(data);
        }
    }
}
