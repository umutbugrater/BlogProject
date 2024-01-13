using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace CoreDemo.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder application)
        {
            Context context = application.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<Context>();

            AboutManager aboutManager = new AboutManager(new EfAboutRepository());
            BlogManager bm = new BlogManager(new EfBlogRepository());
            CategoryManager cm = new CategoryManager(new EfCategoryRepository());
            CommentManager commentManager = new CommentManager(new EfCommentRepository());
            Message2Manager message2Manager = new Message2Manager(new EfMessage2Repository());

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!aboutManager.GetList().Any())
            {
                aboutManager.TAdd(new About()
                {
                    AboutDetails1 = "Genellikle güncelden eskiye doğru sıralanmış yazı ve yorumların yayınlandığı, web tabanlı bir yayını belirtir. Çoğunlukla her gönderinin sonunda yazarın adı ve gönderi zamanı belirtilir. Yayıncının seçimine göre okuyucular yazılara yorum yapılabilir. Yorumlar, blog kültürünün çok önemli bir dinamiğidir; bu sayede yazar ve okuyucular arasında iletişim sağlanır. Bunun dışında, geri izleme (trackback) mekanizmasıyla, belirli bir yazı hakkında yazılan diğer yazıların belirlenebilmesi de mümkündür. İlk bloglar elle yazılıp güncellenirken, bugün bu iş için özel yazılmış yazılımlar kullanılmaktadır. Bu yazılımlardan bazıları bir blog servisi sağlayıcı sitenin alt alan adları olarak yaratılabilen, bazıları ise kullanıcının kendi sunucusuna kurup çalıştırması gereken Blogger, Blogcu.com, WordPress, SpinMedia, joomla, Drupal gibi yazılımlardır.",
                    AboutDetails2 = "Blogların içeriği geleneksel internet içeriğinden farklılık gösterdiği için sadece bloglar için kurulmuş özel indeksleme mekanizmaları ve arama motorları bulunmaktadır. Technorati en başarılı blog teknolojilerinden biridir. Ayrıca Google Blog Search adında bir blog arama motoru işletmektedir. 2005 yılında Verisign tarafından satın alınan Weblogs.com, dünyanın en büyük blog ping servisi olarak tüm internet indeksleme mekanizmalarına veri sağlamaktadır.",
                    AboutImage1 = "aa", AboutMapLocation = "a",
                    AboutImage2 = "aaaa", AboutStatus = true
                }) ;
            }
            if (!cm.GetList().Any())
            {
                cm.TAdd(new Category() { CategoryName = "Yazılım", CategoryDescription = "Burası Açıklama Alanıdır." });
                cm.TAdd(new Category() { CategoryName = "Teknoloji", CategoryDescription = "Burası Açıklama Alanıdır." });
                cm.TAdd(new Category() { CategoryName = "Film & Dizi", CategoryDescription = "Burası Açıklama Alanıdır." });
                cm.TAdd(new Category() { CategoryName = "Oyun", CategoryDescription = "Burası Açıklama Alanıdır." });
                cm.TAdd(new Category() { CategoryName = "Spor", CategoryDescription = "Burası Açıklama Alanıdır." });
                cm.TAdd(new Category() { CategoryName = "Seyahat", CategoryDescription = "Burası Açıklama Alanıdır." });
                cm.TAdd(new Category() { CategoryName = "Donanım", CategoryDescription = "Burası Açıklama Alanıdır." });
            }

            if (!bm.GetList().Any())
            {
                bm.TAdd(new Blog() { BlogTitle = "C# ile Asenkron Metotlar", BlogContent = "Lorem ipsum dolor sit amet consectetur adipisicing elit sedc dnmo eiusmod tempor incididunt..Lorem ipsum dolor sit amet consectetur adipisicing elit sedc dnmo eiusmod tempor incididunt..Lorem ipsum dolor sit amet consectetur adipisicing elit sedc dnmo eiusmod tempor incididunt..", BlogThumbnailImage = "küçük reism", BlogImage = "582fc30c-8389-4af3-a2ca-ff6d3533f7db.jpg", BlogCreateDate = DateTime.Now, CategoryID = 1, AppUserId = 1 });
                bm.TAdd(new Blog() { BlogTitle = "Python ile Veri Analizi", BlogContent = "Python programlama dilini kullanarak veri analinizi gerçekleştirebilmek oldukça mümkün. Bunun için pekçok hazır kütüphane ve bileşen var. Lorem ipsum dolor sit amet consectetur adipisicing elit sedc dnmo eiusmod tempor incididunt..Lorem ipsum dolor sit amet consectetur adipisicing elit sedc dnmo eiusmod tempor incididunt..", BlogThumbnailImage = "küçük resim", BlogImage = "9137271d-4045-4723-8d9f-19a7fc24fc1f.jpg", BlogCreateDate = DateTime.Now, CategoryID = 1, AppUserId = 2 });
                bm.TAdd(new Blog() { BlogTitle = "Kimyager Walter White", BlogContent = "İzlediğim en iyi dizi olan Breaking Bad yapımı. Mutalaka izlemenizi tavsiye ediyorum. IMDB sıralaması içinde de tüm zamanların en yüksek puanına sahip dizisi olarak geçmektedir. Lorem ipsum dolor sit amet consectetur adipisicing elit sedc dnmo eiusmod tempor incididunt..Lorem ipsum dolor sit amet consectetur adipisicing elit sedc dnmo eiusmod tempor incididunt.. ", BlogThumbnailImage = "küçük resim", BlogImage = "d8713e03-729c-4407-8301-01044a4f1993.jpg", BlogCreateDate = DateTime.Now, CategoryID = 3, AppUserId = 1 });
                bm.TAdd(new Blog() { BlogTitle = "Into The Night Dizisi", BlogContent = "Lorem ipsum dolor sit amet consectetur adipisicing elit sedc dnmo eiusmod tempor incididunt..Lorem ipsum dolor sit amet consectetur adipisicing elit sedc dnmo eiusmod tempor incididunt..Lorem ipsum dolor sit amet consectetur adipisicing elit sedc dnmo eiusmod tempor incididunt..", BlogThumbnailImage = "küçük resim", BlogImage = "cb61e473-5c10-44da-a5ee-156420245681.jpg", BlogCreateDate = DateTime.Now, CategoryID = 3, AppUserId = 2 });
                bm.TAdd(new Blog() { BlogTitle = "Apple 15 Sürümü Tanıtıldı", BlogContent = "Lorem ipsum dolor sit amet consectetur adipisicing elit sedc dnmo eiusmod tempor incididunt..Lorem ipsum dolor sit amet consectetur adipisicing elit sedc dnmo eiusmod tempor incididunt..Lorem ipsum dolor sit amet consectetur adipisicing elit sedc dnmo eiusmod tempor incididunt..", BlogThumbnailImage = "küçük resim", BlogImage = "c62af091-600a-43e4-91f0-c7fdaf4eeae9.jpg", BlogCreateDate = DateTime.Now, CategoryID = 2, AppUserId = 1 });
                bm.TAdd(new Blog() { BlogTitle = "Fifa 22 Ekim Sonunda çıkıyor", BlogContent = "Lorem ipsum dolor sit amet consectetur adipisicing elit sedc dnmo eiusmod tempor incididunt..Lorem ipsum dolor sit amet consectetur adipisicing elit sedc dnmo eiusmod tempor incididunt..Lorem ipsum dolor sit amet consectetur adipisicing elit sedc dnmo eiusmod tempor incididunt..", BlogThumbnailImage = "küçük resim", BlogImage = "2b33f617-7378-40ec-88c2-734d5c8a5332.png", BlogCreateDate = DateTime.Now, CategoryID = 5, AppUserId = 2 });
                bm.TAdd(new Blog() { BlogTitle = "C# Repository Pattern", BlogContent = "Lorem ipsum dolor sit amet consectetur adipisicing elit sedc dnmo eiusmod tempor incididunt..Lorem ipsum dolor sit amet consectetur adipisicing elit sedc dnmo eiusmod tempor incididunt..Lorem ipsum dolor sit amet consectetur adipisicing elit sedc dnmo eiusmod tempor incididunt..", BlogThumbnailImage = "küçük resim", BlogImage = "1b296b9a-dedc-49ac-815a-095d2a5d41c8.jpg", BlogCreateDate = DateTime.Now, CategoryID = 2, AppUserId = 1 });
                bm.TAdd(new Blog() { BlogTitle = "AspNet Core 6.0 Proje Kampı", BlogContent = "Lorem ipsum dolor sit amet consectetur adipisicing elit sedc dnmo eiusmod tempor incididunt..Lorem ipsum dolor sit amet consectetur adipisicing elit sedc dnmo eiusmod tempor incididunt..Lorem ipsum dolor sit amet consectetur adipisicing elit sedc dnmo eiusmod tempor incididunt..", BlogThumbnailImage = "küçük resim", BlogImage = "32aff4cf-b7c6-4cdf-8871-8880722b670c.jpg", BlogCreateDate = DateTime.Now, CategoryID = 2, AppUserId = 2 });
                bm.TAdd(new Blog() { BlogTitle = "REsimli blog ekleme 323", BlogContent = "REsimli blog ekleme 323REsimli blog ekleme 323REsimli blog ekleme 323REsimli blog ekleme 323REsimli blog ekleme 323REsimli blog ekleme 323REsimli blog ekleme 323REsimli blog ekleme 323REsimli blog ekleme 323REsimli blog ekleme 323REsimli blog ekleme 323REsimli blog ekleme 323REsimli blog ekleme 323REsimli blog ekleme 323REsimli blog ekleme 323REsimli blog ekleme 323REsimli blog ekleme 323REsimli blog ekleme 323REsimli blog ekleme 323REsimli blog ekleme 323REsimli blog ekleme 323", BlogThumbnailImage = "küçük resim", BlogImage = "751c69e6-b6b8-4a8c-9919-99a159c635da.jpg", BlogCreateDate = DateTime.Now, CategoryID = 7, AppUserId = 1 });
            }

            if (!commentManager.GetList().Any())
            {
                commentManager.TAdd(new Comment() { CommentUserName = "Ali Yıldırım", CommentTitle = "Teşekkürler", CommentContent = "Çok faydalı bir yazı olmuş. Emeğinize sağlık", CommentDate = new DateTime(), BlogID = 4, BlogScore = 7 });
                commentManager.TAdd(new Comment() { CommentUserName = "Gizem Çınar", CommentTitle = "Harika", CommentContent = "Çok ama çok beğendim devamını merakla bekliyorum", CommentDate = new DateTime(), BlogID = 3, BlogScore = 3 });
                commentManager.TAdd(new Comment() { CommentUserName = "Aslı Yıldız", CommentTitle = "Tavsiye", CommentContent = "Bu bloktan paylaştığınız projenin kaynak dosyasına nereden ulaşabilirm. Yol haritası noktasında tavsiyelerinize ihtiyacımız olacak.", CommentDate = new DateTime(), BlogID = 2, BlogScore = 6 });
                commentManager.TAdd(new Comment() { CommentUserName = "Mert Kaya", CommentTitle = "Öneri", CommentContent = "Merhaba Diziyi çok beğendim. en kısa sürede yeni sezonunu bekliyorum. Teşekkürler", CommentDate = new DateTime(), BlogID = 4, BlogScore = 5 });
                commentManager.TAdd(new Comment() { CommentUserName = "Nihat Kayalı", CommentTitle = "Teşekkür", CommentContent = "Çok beğendiğim bir dizi oldu. İyiki izlemişim", CommentDate = new DateTime(), BlogID = 4, BlogScore = 2 });
                commentManager.TAdd(new Comment() { CommentUserName = "Ayşegül Sarı", CommentTitle = "İstek", CommentContent = "Bu diziyi geçen sene izlemiştim. Buna benzer olarak Sally filmini izleyebilirsiniz", CommentDate = new DateTime(), BlogID = 4, BlogScore = 4 });
                commentManager.TAdd(new Comment() { CommentUserName = "Ali Güneş", CommentTitle = "Deneme", CommentContent = "Merhaba. Bu bir test yorumudur", CommentDate = new DateTime(), BlogID = 2, BlogScore = 1 });
            }
            if (!message2Manager.GetList().Any())
            {
                message2Manager.TAdd(new Message2 { SenderID = 1, ReceiverID = 2, Subject = "Seyahat Blog Sorunu", MessageDetails = "What is Lorem Ipsum?d du", MessageDate = DateTime.Now, MessageStatus = true });
                message2Manager.TAdd(new Message2 { SenderID = 2, ReceiverID = 1, Subject = "test", MessageDetails = "aaa", MessageDate = DateTime.Now, MessageStatus = true });
                message2Manager.TAdd(new Message2 { SenderID = 1, ReceiverID = 2, Subject = "Deneme", MessageDetails = "merhaba test yapıyoruz", MessageDate = DateTime.Now, MessageStatus = true });
                message2Manager.TAdd(new Message2 { SenderID = 2, ReceiverID = 1, Subject = "Deneme", MessageDetails = "dedededede", MessageDate = DateTime.Now, MessageStatus = true });
            }
        }
    }
}
