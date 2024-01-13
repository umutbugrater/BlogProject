using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.ViewComponents.Writer
{
    public class WriterHeader : ViewComponent
    {
        Context c = new Context();
        public IViewComponentResult Invoke()
        {
            var username = User.Identity.Name;
            var usernamesurname = c.Users.Where(x => x.UserName == username).Select(x => x.NameSurname).FirstOrDefault();
            var userimage = c.Users.Where(x=>x.UserName==username).Select(z=>z.ImageUrl).FirstOrDefault();
            ViewBag.image = userimage;
            ViewBag.name = usernamesurname.ToUpper() ;
            return View();
        }
    }
}
