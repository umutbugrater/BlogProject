using BusinessLayer.Concrete;
using CoreDemo.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]
    public class RegisterUserController : Controller
    {
        private readonly UserManager<AppUser> _userManager; //UserManager, sisteme identity üzerinden kayıt olmak için kullandığımız komut

        public RegisterUserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserSignUpViewModel p, IFormFile img)
        {
            if (img != null)
            {
                string uzantı = Path.GetExtension(img.FileName);
                string resimAdi = Guid.NewGuid() + uzantı;
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/WriterImageFiles/{resimAdi}");
                using var stream = new FileStream(path, FileMode.Create);
                img.CopyTo(stream);

                AppUser user = new AppUser()
                {
                    Email = p.Mail,
                    UserName = p.UserName,
                    NameSurname = p.NameSurname
                };
                user.ImageUrl = resimAdi;
                //identity kütüphanesinde, şifre metot çağrılırken giriliyor
                var result = await _userManager.CreateAsync(user, p.Password);
                await _userManager.AddToRoleAsync(user, "Writer");

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(p);
        }
    }
}
