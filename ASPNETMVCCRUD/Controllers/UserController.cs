using ASPNETMVCCRUD.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ASPNETMVCCRUD.Controllers
{
    public class UserController : Controller
    {
        private UserInfoEntities db = new UserInfoEntities();
        private string[] colors = { "Blue", "Red", "Green" };

        public ActionResult ShowAllUsers()
        {
            return View(db.Users.ToList());
        }

        public ActionResult AddUser()
        {
            ViewBag.Colors = new SelectList(colors);

            return View();
        }

        [HttpPost]
        public ActionResult AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = Convert.ToBase64String(System.Security.Cryptography.SHA256.Create()
                    .ComputeHash(Encoding.UTF8.GetBytes(user.Password)));

                db.Users.Add(user);
                db.SaveChanges();

                return RedirectToAction("ShowAllUsers");
            }

            ViewBag.Colors = new SelectList(colors);

            return View(user);
        }

        public ActionResult UpdateUser(int id)
        {
            ViewBag.Colors = new SelectList(colors);

            return View(db.Users.Find(id));
        }

        [HttpPost]
        public ActionResult UpdateUser(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.ConfirmPassword == null || !user.ConfirmPassword.Equals(user.Password))
                {
                    ViewBag.Error = "Password doesn't match";
                    ViewBag.Colors = new SelectList(colors);

                    return View(user);
                }

                user.Password = Convert.ToBase64String(System.Security.Cryptography.SHA256.Create()
                    .ComputeHash(Encoding.UTF8.GetBytes(user.Password)));

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("ShowAllUsers");
            }

            ViewBag.Colors = new SelectList(colors);

            return View(user);
        }

        public ActionResult UserDetails(int id)
        {
            return View(db.Users.Find(id));
        }

        public ActionResult DeleteUser(int id)
        {
            return View(db.Users.Find(id));
        }

        [HttpPost]
        public ActionResult DeleteUser(int? id)
        {
            db.Users.Remove(db.Users.Find(id));
            db.SaveChanges();

            return RedirectToAction("ShowAllUsers");
        }
    }
}