using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using CoreDemo.Models;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{

    public class WriterController : Controller
    {
        WriterManager wm = new WriterManager(new EfWriterRepository());
        UserManager userManager = new UserManager(new EfUserRepository());
        Context c = new Context();

        private readonly UserManager<AppUser> _userManager;

        public WriterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            var usermail = User.Identity.Name;
            ViewBag.v = usermail;
            Context c = new Context();
            var writerName = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterName).FirstOrDefault();
            ViewBag.v2 = writerName;

            return View();
        }


        [AllowAnonymous]
        public PartialViewResult WriterFooterPartial()
        {
            return PartialView();
        }

        [HttpGet]
        public async Task<IActionResult> WriterEditProfile()
        {
            //var username = User.Identity.Name;
            //var usermail = c.Users.Where(x=>x.UserName==username).Select(y=>y.Email).FirstOrDefault();
            //var id = c.Users.Where(x=>x.Email==usermail).Select(y=>y.Id).FirstOrDefault();
            //var values = userManager.TGetById(id);
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            UserUpdateViewModel model = new UserUpdateViewModel();
            model.mail = values.Email;
            model.namesurname = values.NameSurname;
            model.username = values.UserName;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> WriterEditProfile(UserUpdateViewModel model, IFormFile img)
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);

            if (img != null)
            {
                string uzantı = Path.GetExtension(img.FileName);
                string resimAdi = Guid.NewGuid() + uzantı;
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/WriterImageFiles/{resimAdi}");
                using var stream = new FileStream(path, FileMode.Create);
                img.CopyTo(stream);
                values.ImageUrl = resimAdi;
            }
            values.Email = model.mail;
            values.NameSurname = model.namesurname;
            if (!model.ChangePassword)
            {
                values.PasswordHash = _userManager.PasswordHasher.HashPassword(values, model.password);
            }
            var result = await _userManager.UpdateAsync(values);
            return RedirectToAction("Index", "Dashboard");

        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult WriterAdd()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        //İlk başta Imageyi string tanımladığımız için, değiştirmemek adına bu sınıfı oluşturduk.
        public IActionResult WriterAdd(AddProfileImage p)
        {
            Writer w = new Writer();
            if (p.WriterImage != null)
            {
                var extension = Path.GetExtension(p.WriterImage.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterImageFiles/", newimagename);
                var stream = new FileStream(location, FileMode.Create);
                p.WriterImage.CopyTo(stream);
                w.WriterImage = newimagename;
            }
            w.WriterMail = p.WriterMail;
            w.WriterName = p.WriterName;
            w.WriterPassword = p.WriterPassword;
            w.WriterStatus = true;
            w.WriterAbout = p.WriterAbout;
            wm.TAdd(w);
            return RedirectToAction("Index", "Dashboard");
        }

    }
}
