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
    [Authorize(Roles = "Admin, Super Admin")]
    public class announcementsController : Controller
    {
        private MattDatabaseEntities db = new MattDatabaseEntities();

        // GET: announcements
        public ActionResult Index()
        {
            var announcements = db.announcements.Include(a => a.site_users).OrderByDescending(a => a.ann_date);
            return View(announcements.ToList());
        }

        // GET: announcements/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                announcement announcement = db.announcements.Find(id);
                if (announcement == null)
                {
                    return RedirectToAction("Error");
                }
                return View(announcement);
            }
            catch (Exception Error)
            {
                ViewBag.Error = Error.GetBaseException().Message;
            }
            return View("Error");
        }

        // GET: announcements/Create
        public ActionResult Create()
        { 
            return View();
        }

        // POST: announcements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ann_id,ann_text,ann_date,ann_severity,ann_visible")] announcement announcement)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    announcement.emp_id = Convert.ToInt32(Session["empId"]);
                    db.announcements.Add(announcement);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(announcement);
            }
            catch (Exception Error)
            {
                ViewBag.Error = Error.GetBaseException().Message;
                if (ViewBag.Error.Contains("fk_announcements_emp_id"))
                {
                    //throw new Exception("You must enter a valid User Id to create an announcement.");
                    ViewBag.Error = "You must enter a valid User Id to create an announcement.";
                }
                else if (ViewBag.Error.Contains("ann_emp_id_check"))
                {
                    ViewBag.Error = "You must enter a valid User Id to create an announcement.";
                }
                else if (ViewBag.Error.Contains("ann_severity_check"))
                {
                    ViewBag.Error = "The announcement severity value must be one of Emergency, Important, or Cautionary.";
                }
            }
            return View("Error");
           
        }

        // GET: announcements/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                announcement announcement = db.announcements.Find(id);
                if (announcement == null)
                {
                    return RedirectToAction("Error");
                }
                return View(announcement);
            }
            catch (Exception Error)
            {
                ViewBag.Error = Error.GetBaseException().Message;
            }
            return View("Error");
        }

        // POST: announcements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ann_id,ann_text,ann_date,ann_severity,ann_visible")] announcement announcement)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    announcement.emp_id = Convert.ToInt32(Session["userId"]);
                    db.Entry(announcement).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(announcement);
            }
            catch (Exception Error)
            {
                ViewBag.Error = Error.GetBaseException().Message;
                if (ViewBag.Error.Contains("fk_announcements_emp_id"))
                {
                    //throw new Exception("You must enter a valid User Id to create an announcement.");
                    ViewBag.Error = "You must enter a valid User Id to edit an announcement.";
                }
                else if (ViewBag.Error.Contains("ann_emp_id_check"))
                {
                    ViewBag.Error = "You must enter a valid User Id to edit an announcement.";
                }
                else if (ViewBag.Error.Contains("ann_severity_check"))
                {
                    ViewBag.Error = "The announcement severity value must be one of Emergency, Important, or Cautionary.";
                }
            }
            return View("Error");
           
        }

        // GET: announcements/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                announcement announcement = db.announcements.Find(id);
                if (announcement == null)
                {
                    return RedirectToAction("Error");
                }
                return View(announcement);
            }
            catch (Exception Error)
            {
                ViewBag.Error = Error.GetBaseException().Message;
            }
            return View("Error");
           
        }

        // POST: announcements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                announcement announcement = db.announcements.Find(id);
                db.announcements.Remove(announcement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception Error)
            {
                ViewBag.Error = Error.GetBaseException().Message;
            }
            return View("Error");
           
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
