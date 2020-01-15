using RealBlog.Dal;
using RealBlog.Helpers;
using RealBlog.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RealBlog.Controllers
{
    public class RegisterController : Controller
    {
        private readonly MotoblogDbContext dbContext = new MotoblogDbContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User userToRegister, HttpPostedFileBase imageData)
        {
            if (Session["UserId"] != null)
                return RedirectToAction("Index", "Profile");

            if (ModelState.IsValid)
            {
                if (userToRegister.Password != userToRegister.PasswordConfirm)
                    ModelState.AddModelError(string.Empty, "Пароли не совпадают");

                var user = dbContext.Users.
                    FirstOrDefault(u => u.Login == userToRegister.Login);

                if (user != null)
                    ModelState.AddModelError(string.Empty, "Пользователь с таким логином уже существует");

                user = dbContext.Users.FirstOrDefault(
                     u => u.Email == userToRegister.Email);

                if (user != null)
                    ModelState.AddModelError(string.Empty, "Пользователь с такой почтой уже существует");

                if (!ModelState.IsValid)
                    return View("Index", userToRegister);

                if (imageData != null)
                    userToRegister.PhotoUrl = ImageSaveHelper.SaveImage(imageData);

                dbContext.Users.Add(userToRegister);
                dbContext.SaveChanges();

                FormsAuthentication.SetAuthCookie(userToRegister.Login, false);
                Session["UserId"] = userToRegister.Id.ToString();
                Session["UserNick"] = userToRegister.Login;

                return RedirectToAction("Index", "Feed");
            }

            return View("Index", userToRegister);
        }
    }
}