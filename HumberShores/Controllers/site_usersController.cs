using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HumberShores.Models;

namespace HumberShores.Controllers
{
    [Authorize]
    public class site_usersController : Controller
    {
        private MattDatabaseEntities db = new MattDatabaseEntities();

        // GET: site_users
        [Authorize(Roles = "Super Admin")]
        public ActionResult Index()
        {
            var site_users = db.site_users.Include(s => s.employee).Include(s => s.site_roles).OrderBy(s => s.user_id);
            return View(site_users.ToList());
        }

        // GET: site_users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            site_users site_users = db.site_users.Find(id);
            if (site_users == null)
            {
                return HttpNotFound();
            }
            return View(site_users);
        }

        // GET: site_users/Create
        public ActionResult Create()
        {
            ViewBag.emp_id = new SelectList(db.employees, "emp_id", "emp_id");
            ViewBag.role_code = new SelectList(db.site_roles, "role_code", "role_name");
            return View();
        }

        // POST: site_users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "user_id,emp_id,user_first_name,user_last_name,user_dob,user_gender,user_address,user_city,user_province,user_postal_code,user_email,user_username,user_password,user_phone,user_date_joined")] site_users site_users)
        {
            if (ModelState.IsValid)
            {
                site_users.role_code = "USR";
                db.site_users.Add(site_users);
                db.SaveChanges();
                return RedirectToAction("UserHome", "Home");
            }

            ViewBag.emp_id = new SelectList(db.employees, "emp_id", "emp_id", site_users.emp_id);
            ViewBag.role_code = new SelectList(db.site_roles, "role_code", "role_name", site_users.role_code);
            return View(site_users);
        }

        // GET: site_users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            site_users site_users = db.site_users.Find(id);
            if (site_users == null)
            {
                return HttpNotFound();
            }
            ViewBag.emp_id = new SelectList(db.employees, "emp_id", "emp_position", site_users.emp_id);
            ViewBag.role_code = new SelectList(db.site_roles, "role_code", "role_name", site_users.role_code);
            return View(site_users);
        }

        // POST: site_users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "user_id,emp_id,role_code,user_first_name,user_last_name,user_dob,user_gender,user_address,user_city,user_province,user_postal_code,user_email,user_username,user_password,user_phone,user_date_joined")] site_users site_users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(site_users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.emp_id = new SelectList(db.employees, "emp_id", "emp_position", site_users.emp_id);
            ViewBag.role_code = new SelectList(db.site_roles, "role_code", "role_name", site_users.role_code);
            return View(site_users);
        }

        // GET: site_users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            site_users site_users = db.site_users.Find(id);
            if (site_users == null)
            {
                return HttpNotFound();
            }
            return View(site_users);
        }

        // POST: site_users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            site_users site_users = db.site_users.Find(id);
            db.site_users.Remove(site_users);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
