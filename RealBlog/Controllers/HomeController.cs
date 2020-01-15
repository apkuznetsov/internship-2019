using RealBlog.Dal;
using RealBlog.Models.ViewModel;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RealBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly MotoblogDbContext dbContext = new MotoblogDbContext();

        public ActionResult Index()
        {
            var cookie = Request.Cookies["UserInfo"];

            if (cookie != null)
            {
                Session["UserId"] = cookie["UserId"];
                Session["UserNick"] = cookie["UserNick"];
                Session["UserPhotoUrl"] = cookie["UserPhotoUrl"];

                return RedirectToAction("Index", "Feed");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var user = dbContext.Users.FirstOrDefault(
                    u => u.Login == login.Login &&
                    u.Password == login.Password);

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(login.Login, login.RememberMe);

                    Session["UserId"] = user.Id.ToString();
                    Session["UserNick"] = user.Login;
                    Session["UserPhotoUrl"] = user.PhotoUrl;


                    if (login.RememberMe)
                    {
                        HttpCookie userInfo = new HttpCookie("UserInfo");

                        userInfo["UserId"] = user.Id.ToString();
                        userInfo["UserNick"] = user.Login.ToString();

                        if (user.PhotoUrl != null)
                            userInfo["UserPhotoUrl"] = user.PhotoUrl.ToString();

                        userInfo.Expires = DateTime.Now.AddYears(1);

                        Response.Cookies.Add(userInfo);
                    }

                    return RedirectToAction("Index", "Feed");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Неверная пара юзернэйма и пароля");
                }
            }

            return View("Index", login);
        }

        public ActionResult Logout(LoginViewModel login_)
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            var cookie = Request.Cookies["UserInfo"];
            if (cookie != null)
            {
                var newCookie = new HttpCookie("UserInfo");
                newCookie.Expires = DateTime.Now.AddDays(-1);

                Response.Cookies.Add(newCookie);
            }

            return RedirectToAction("Index");
        }
    }
}