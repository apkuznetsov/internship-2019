using RealBlog.Dal;
using RealBlog.Helpers;
using RealBlog.Models;
using RealBlog.Models.ViewModel;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealBlog.Controllers
{
    public class FeedController : Controller
    {
        private readonly MotoblogDbContext dbContext = new MotoblogDbContext();

        public ActionResult Index()
        {
            var posts = dbContext.Posts.
                OrderByDescending(p => p.Date).ToList();

            ViewBag.Posts = posts;

            return View();
        }

        [HttpPost]
        public ActionResult Search(SearchViewModel search)
        {
            var posts = dbContext.Posts.
                Where(
                m => m.Text.Contains(search.SearchString) ||
                m.Author.Username.Contains(search.SearchString)).ToList();

            ViewBag.Posts = posts;

            return View("Index");
        }

        [HttpPost]
        public ActionResult AddPost(Post post, HttpPostedFileBase imageData)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Index", "Home");

            if (imageData == null)
                ModelState.AddModelError(String.Empty, "Добавьте изображение");

            if (post.Text == null)
            {
                ModelState.AddModelError(String.Empty, "Введите текст");
            }
            else
            {
                post.PhotoUrl = ImageSaveHelper.SaveImage(imageData);

                var id = Convert.ToInt32(Session["UserId"]);

                var author = dbContext.Users.
                    FirstOrDefault(u => u.Id == id);
                post.Author = author ?? throw new Exception("Пользователь не найден");

                post.Date = DateTime.Now;

                dbContext.Posts.Add(post);
                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            var posts = dbContext.Posts.
                OrderByDescending(p => p.Date).ToList();

            ViewBag.Posts = posts;

            return View("Index", post);
        }
    }
}