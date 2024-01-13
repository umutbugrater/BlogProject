using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.ViewComponents.Blog
{
    public class WriterLastBlog : ViewComponent
    {
        BlogManager bm = new BlogManager(new EfBlogRepository());
        Context c = new Context();

        public IViewComponentResult Invoke(int id)
        {
            var value = bm.GetList().Where(x => x.BlogID == id).Select(x => x.AppUserId).FirstOrDefault();
            var values = bm.GetBlogListByWriter(value);
            return View(values);
        }
    }
}
