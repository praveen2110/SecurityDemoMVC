using SecurityDemoMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SecurityDemoMVC.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts/Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserModel model)
        {
            using (Employee_DB_Entites context = new Employee_DB_Entites())
            {
                bool isvalidUser = context.Users.Any(user => user.UserName.ToLower() == model.UserName.ToLower() && user.UserPassword.ToLower() == model.UserPassword.ToLower());
                if (isvalidUser)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "Employees");
                }

                ModelState.AddModelError("", "Invalid UserName or Password");
                
            }
            return View();
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(User model)
        {
            using (Employee_DB_Entites context = new Employee_DB_Entites())
            {
                context.Users.Add(model);
                context.SaveChanges();

            }
            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}