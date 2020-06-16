using ASPNETMVCCRUD.Models;
using ASPNETMVCCRUD.ViewModels;
using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ASPNETMVCCRUD.Controllers
{
    public class LoginController : Controller
    {
        private UserInfoEntities db = new UserInfoEntities();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLogin userLogin)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Where(x => x.Name.Equals(userLogin.Name)).Count() == 0)
                {
                    ViewBag.UserError = "Username does not exists";

                    return View(userLogin);
                }

                string encrypted_pass = Convert.ToBase64String(System.Security.Cryptography.SHA256.Create()
                    .ComputeHash(Encoding.UTF8.GetBytes(userLogin.Password)));

                if (db.Users.Where(x => x.Name.Equals(userLogin.Name) && x.Password.Equals(encrypted_pass)).Count() == 0)
                {
                    ViewBag.PasswordError = "You entered an incorrect password";

                    return View(userLogin);
                }

                User user = db.Users.Where(m => m.Name == userLogin.Name).FirstOrDefault();

                Session["Name"] = user.Name;
                Session["Role"] = user.isAdmin;

                System.Diagnostics.Debug.WriteLine(Session["Name"]);
                System.Diagnostics.Debug.WriteLine(Session["Role"]);

                return RedirectToAction("ShowAllUsers", "User");
            }

            return View(userLogin);
        }

        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("Login");
        }
    }
}