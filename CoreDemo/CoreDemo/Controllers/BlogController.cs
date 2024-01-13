using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]

    public class BlogController : Controller
    {
        BlogManager bm = new BlogManager(new EfBlogRepository());
        CategoryManager cm = new CategoryManager(new EfCategoryRepository());
        CommentManager commentManager = new CommentManager(new EfCommentRepository());
        Context c = new Context();

        public IActionResult Index()
        {
            int[] blogYorum = new int[bm.GetList().OrderByDescending(x => x.BlogID).FirstOrDefault().BlogID + 1];
            for (int i = 0; i < blogYorum.Length; i++)
            {
                blogYorum[i] = commentManager.GetList().Where(x => x.BlogID == i).Count();
            }
            var values = bm.GetBlogListWithCategory();
            ViewBag.blogSayisi = blogYorum.Length;
            ViewBag.yorumSayilari = blogYorum;
            return View(values);
        }
        public IActionResult BlogReadAll(int id)
        {
            ViewBag.i = id;
            var values = bm.GetBlogByID(id);
            ViewBag.commentCount = commentManager.GetList(id).Count();
            var scores = commentManager.GetList(id);
            int? toplamPuan = 0;
            foreach (var score in scores)
            {
                toplamPuan += score.BlogScore;
            }
            ViewBag.begeni = scores.Count() > 0 ? Math.Round((double)toplamPuan / scores.Count(), 2) : 0;//Virgülden sonra 2 basamak alması için
            return View(values);
        }
        public IActionResult BlogListByWriter()
        {
            //var username = User.Identity.Name;
            //var usermail = c.Users.Where(x => x.UserName == username).Select(x => x.Email).FirstOrDefault();
            //var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
            var username = User.Identity.Name;
            var writerid = c.Users.Where(x => x.UserName == username).Select(x => x.Id).FirstOrDefault();
            var values = bm.GetListWithCategoryByWriterBm(writerid);

            return View(values);
        }

        [HttpGet]
        public IActionResult BlogAdd()
        {
            List<SelectListItem> categoryvalues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString(),
                                                   }).ToList();

            ViewBag.cv = categoryvalues;
            return View();
        }

        [HttpPost]
        public IActionResult BlogAdd(Blog p, IFormFile img)
        {
            if (img != null)
            {
                string uzantı = Path.GetExtension(img.FileName);
                string resimAdi = Guid.NewGuid() + uzantı;
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/BlogImages/{resimAdi}");
                using var stream = new FileStream(path, FileMode.Create);
                img.CopyTo(stream);
                p.BlogImage = resimAdi;
            }
            //var username = User.Identity.Name;
            //var usermail = c.Users.Where(x => x.UserName == username).Select(x => x.Email).FirstOrDefault();
            //var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
            var username = User.Identity.Name;
            var writerid = c.Users.Where(x => x.UserName == username).Select(x => x.Id).FirstOrDefault();
            BlogValidator bv = new BlogValidator();
            ValidationResult results = bv.Validate(p);
            List<SelectListItem> categoryvalues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString(),
                                                   }).ToList();

            ViewBag.cv = categoryvalues;

            if (results.IsValid)
            {
                p.BlogStatus = true;
                p.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.AppUserId = writerid;
                bm.TAdd(p);
                return RedirectToAction("BlogListByWriter", "Blog");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        public IActionResult DeleteBlog(int id)
        {
            var blogvalue = bm.TGetById(id);
            bm.TDelete(blogvalue);
            return RedirectToAction("BlogListByWriter");
        }

        [HttpGet]
        public IActionResult EditBlog(int id)
        {
            var blogvalue = bm.TGetById(id);
            List<SelectListItem> categoryvalues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString(),
                                                   }).ToList();

            ViewBag.cv = categoryvalues;
            return View(blogvalue);
        }
        [HttpPost]
        public IActionResult EditBlog(Blog p, IFormFile img)
        {
            var blogvalue = bm.TGetById(p.BlogID); //
            if (img != null)
            {
                string uzantı = Path.GetExtension(img.FileName);
                string resimAdi = Guid.NewGuid() + uzantı;
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/BlogImages/{resimAdi}");
                using var stream = new FileStream(path, FileMode.Create);
                img.CopyTo(stream);
                p.BlogImage = resimAdi;
            }
            else
            {
                p.BlogImage = blogvalue.BlogImage;
            }
            //var username = User.Identity.Name;
            //var usermail = c.Users.Where(x => x.UserName == username).Select(x => x.Email).FirstOrDefault();
            //var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
            var username = User.Identity.Name;
            var writerid = c.Users.Where(x => x.UserName == username).Select(x => x.Id).FirstOrDefault();
            p.AppUserId = writerid;
            p.BlogCreateDate = blogvalue.BlogCreateDate;
            p.BlogStatus = blogvalue.BlogStatus;
            bm.TUpdate(p);
            return RedirectToAction("BlogListByWriter");
        }
    }
}
