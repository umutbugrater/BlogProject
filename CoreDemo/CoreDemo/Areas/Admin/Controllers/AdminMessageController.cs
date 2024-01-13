using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminMessageController : Controller
    {
        Message2Manager mm = new Message2Manager(new EfMessage2Repository());
        Context c = new Context();
        public IActionResult InBox()
        {
            //var username = User.Identity.Name;
            //var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            //var writerid = c.Writers.Where(y => y.WriterMail == usermail).Select(z => z.WriterID).FirstOrDefault();
            var username = User.Identity.Name;
            var writerid = c.Users.Where(x => x.UserName == username).Select(x => x.Id).FirstOrDefault();
            var values = mm.GetInboxListByWriter(writerid);
            return View(values);
        }

        public IActionResult SendBox()
        {
            //var username = User.Identity.Name;
            //var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            //var writerid = c.Writers.Where(y => y.WriterMail == usermail).Select(z => z.WriterID).FirstOrDefault();
            var username = User.Identity.Name;
            var writerid = c.Users.Where(x => x.UserName == username).Select(x => x.Id).FirstOrDefault();
            var values = mm.GetSendBoxListByWriter(writerid);
            return View(values);
        }
        [HttpGet]
        public IActionResult ComposeMessage()
        {
            List<SelectListItem> recieverUsers = (from x in c.Users.ToList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.Email,
                                                       Value = x.Id.ToString()
                                                   }).ToList();
            ViewBag.RecieverUser = recieverUsers;
            return View();
        }
        [HttpPost]
        public IActionResult ComposeMessage(Message2 p)
        {
            //var username = User.Identity.Name;
            //var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            //var writerid = c.Writers.Where(y => y.WriterMail == usermail).Select(z => z.WriterID).FirstOrDefault();
            var username = User.Identity.Name;
            var writerid = c.Users.Where(x => x.UserName == username).Select(x => x.Id).FirstOrDefault();
            p.SenderID = writerid;
            //p.ReceiverID = p.ReceiverID;
            p.MessageDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            p.MessageStatus = true;
            mm.TAdd(p);
            return RedirectToAction("SendBox");
        }
    }
}
