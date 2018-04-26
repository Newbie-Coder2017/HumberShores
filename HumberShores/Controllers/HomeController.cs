using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HumberShores.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace HumberShores.Controllers
{
    public class HomeController : Controller
    {
        private MattDatabaseEntities db = new MattDatabaseEntities();

        public PartialViewResult ShowAnn()
        {
            var announce = db.announcements.Where(a => a.ann_visible == true).OrderByDescending(a => a.ann_date).ToList();
            return PartialView(announce);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated && (User.IsInRole("Admin") || User.IsInRole("Super Admin")))
            { 
                return RedirectToAction("AdminHome");
            }
            else if (User.Identity.IsAuthenticated && User.IsInRole("User"))
            {
                return RedirectToAction("UserHome");
            }
            return View();
        }

        [Authorize(Roles = "Admin, Super Admin")]
        public ActionResult AdminHome()
        {
            if(User.IsInRole("User"))
            {
                return RedirectToAction("Login");
            }
            //Session["userId"] = User.Identity.GetUserId();
            return View();
        }

        [Authorize(Roles = "User")]
        public ActionResult UserHome()
        {
            if(User.IsInRole("Admin") || User.IsInRole("Super Admin"))
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(site_users user)
        {
            //Hash the incoming password with SHA256. The hash of what is stored in the database will be compared to the hash of what a login attempt will use as a password
            //employee.emp_password = Encryptor.Sha256String(employee.emp_password);

            var count = db.site_users.Where(
                u => u.user_username == user.user_username
                &&
                u.user_password == user.user_password).FirstOrDefault();

            if (count != null)
            {
                Session["empId"] = count.emp_id.ToString();
                Session["userId"] = count.user_id.ToString();
                Session["first"] = count.user_first_name;
                FormsAuthentication.SetAuthCookie(user.user_username, false);
                return RedirectToAction("Login");
            }

            ViewBag.Message = "Invalid username and/or password";
            return View(user);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}