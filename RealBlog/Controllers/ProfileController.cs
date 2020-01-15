using RealBlog.Dal;
using RealBlog.Helpers;
using RealBlog.Models;
using System;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;

namespace RealBlog.Controllers
{
    public class ProfileController : Controller
    {
        private readonly MotoblogDbContext dbContext = new MotoblogDbContext();

        public ActionResult Index(int id)
        {
            var user = dbContext.Users.
                FirstOrDefault(u => u.Id == id);

            var userPosts = dbContext.Posts.
                Where(p => p.Author.Id == user.Id).
                OrderByDescending(p => p.Date).ToList();

            ViewBag.Posts = userPosts;

            return View(user);
        }

        public ActionResult UpdateProfile(User updatedUser, HttpPostedFileBase imageData)
        {
            if (ModelState.IsValid)
            {
                if (updatedUser.Password != updatedUser.PasswordConfirm)
                    ModelState.AddModelError(string.Empty, "Пароли не совпадают");

                var user = dbContext.Users.
                    FirstOrDefault(
                    u => u.Login == updatedUser.Login &&
                    u.Id != updatedUser.Id);

                if (user != null)
                    ModelState.AddModelError(string.Empty, "Пользователь с таким логином уже существует");

                if (ModelState.IsValid)
                {
                    if (imageData != null)
                        updatedUser.PhotoUrl = ImageSaveHelper.SaveImage(imageData);

                    dbContext.Entry(updatedUser).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }

            var userPosts = dbContext.Posts.
                Where(p => p.Author.Id == updatedUser.Id).
                OrderByDescending(p => p.Date).ToList();

            ViewBag.Posts = userPosts;

            return View("Index", updatedUser);
        }

        public ActionResult ProfilePost()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddPost(Post post, HttpPostedFileBase imageData)
        {
            if (ModelState.IsValid)
            {
                if (imageData == null)
                {
                    ModelState.AddModelError(string.Empty, "Загрузите изображение");

                    return View("Index", post);
                }

                post.PhotoUrl = ImageSaveHelper.SaveImage(imageData);

                var userId = Convert.ToInt32(Session["UserId"]);
                var user = dbContext.Users.
                    FirstOrDefault(u => u.Id == userId);

                post.Author = user ?? throw new Exception("Пользователь не найден");

                post.Date = System.DateTime.Now;

                dbContext.Posts.Add(post);
                dbContext.SaveChanges();
            }

            return RedirectToAction("Index", new { id = Convert.ToInt32(Session["UserId"]) });
        }
    }
}