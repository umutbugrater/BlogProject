using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]
    public class CommentController : Controller
    {
        CommentManager cm = new CommentManager(new EfCommentRepository());
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CommentAddByBlog( Comment p, int id)
        {
            p.CommentDate = DateTime.Parse(DateTime.Now.ToString());
            p.CommentStatus = true;
            p.BlogID = id;
            cm.CommentAdd(p);
            return RedirectToAction("BlogReadAll","Blog", new { id = id });
        }
        
    }
}
