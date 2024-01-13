using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CoreDemo.Controllers
{
    public class MessageController : Controller
    {
        Message2Manager mm = new Message2Manager(new EfMessage2Repository());
        Context c = new Context();

        public IActionResult InBox()
        {
            //var username = User.Identity.Name;
            //var usermail = c.Users.Where(x => x.UserName == username).Select(x => x.Email).FirstOrDefault();
            //var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
            var username = User.Identity.Name;
            var writerid = c.Users.Where(x => x.UserName == username).Select(x => x.Id).FirstOrDefault();
            var values = mm.GetInboxListByWriter(writerid);
            return View(values);
        }

        public IActionResult SendBox()
        {
            //var username = User.Identity.Name;
            //var usermail = c.Users.Where(x => x.UserName == username).Select(x => x.Email).FirstOrDefault();
            //var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
            var username = User.Identity.Name;
            var writerid = c.Users.Where(x => x.UserName == username).Select(x => x.Id).FirstOrDefault();
            var values = mm.GetSendBoxListByWriter(writerid);
            return View(values);
        }
        public IActionResult MessageDetails(int id)
        {
            var value = mm.TGetById(id);
           
            return View(value);
        }
        [HttpGet]
        public async Task<IActionResult> SendMessage()
        {
            List<SelectListItem> recieverUsers = (from x in await c.Users.ToListAsync()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.Email.ToString(),
                                                      Value = x.Id.ToString()
                                                  }).ToList();
            ViewBag.RecieverUser = recieverUsers;
            return View();
        }
        [HttpPost]
        public IActionResult SendMessage(Message2 p)
        {
            //var username = User.Identity.Name;
            //var usermail = c.Users.Where(x => x.UserName == username).Select(x => x.Email).FirstOrDefault();
            //var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
            var username = User.Identity.Name;
            var writerid = c.Users.Where(x => x.UserName == username).Select(x => x.Id).FirstOrDefault();
            p.SenderID = writerid;
            //p.ReceiverID = p.ReceiverID;
            p.MessageStatus = true;
            p.MessageDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            mm.TAdd(p);
            return RedirectToAction("InBox");
        }
        public IActionResult InBoxMessageDelete(int id)
        {
            var message = mm.GetList().Where(x=>x.MessageID == id).FirstOrDefault();
            mm.TDelete(message);
            return RedirectToAction("Inbox", "Message");
        }

        public IActionResult SendBoxMessageDelete(int id)
        {
            var message = mm.GetList().Where(x=>x.MessageID==id).FirstOrDefault();
            mm.TDelete(message);
            return RedirectToAction("SendBox", "Message");
        }
    }
}
