using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            Context c = new Context();
            var username = User.Identity.Name;
            var writerid = c.Users.Where(x => x.UserName == username).Select(x => x.Id).FirstOrDefault();
            ViewBag.v1 = c.Blogs.Count();
            ViewBag.v2 = c.Blogs.Where(x => x.AppUserId == writerid).Count();
            ViewBag.v3 = c.Categories.Count();
            return View();
        }
    }
}
