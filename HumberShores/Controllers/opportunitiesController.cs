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
    public class opportunitiesController : Controller
    {
        private MattDatabaseEntities db = new MattDatabaseEntities();

        // GET: opportunities
        public ActionResult Index()
        {
            var oPPORTUNITies = db.OPPORTUNITies.Include(o => o.department);
            return View(oPPORTUNITies.ToList());
        }

        // GET: opportunities/Details/5
        [Authorize(Roles = "Admin, Super Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OPPORTUNITY oPPORTUNITY = db.OPPORTUNITies.Find(id);
            if (oPPORTUNITY == null)
            {
                return HttpNotFound();
            }
            return View(oPPORTUNITY);
        }

        // GET: opportunities/Create
        [Authorize(Roles = "Admin, Super Admin")]
        public ActionResult Create()
        {
            ViewBag.DEPT_ID = new SelectList(db.departments, "dept_id", "dept_name");
            return View();
        }

        // POST: opportunities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Super Admin")]
        public ActionResult Create([Bind(Include = "OPPORTUNITY_ID,OPPORTUNITY_TITLE,OPPORTUNITY_DESC,DEPT_ID,START_DATE,CONTACT_FN,CONTACT_LN,CONTACT_EMAIL")] OPPORTUNITY oPPORTUNITY)
        {
            if (ModelState.IsValid)
            {
                db.OPPORTUNITies.Add(oPPORTUNITY);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DEPT_ID = new SelectList(db.departments, "dept_id", "dept_name", oPPORTUNITY.DEPT_ID);
            return View(oPPORTUNITY);
        }

        // GET: opportunities/Edit/5
        [Authorize(Roles = "Admin, Super Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OPPORTUNITY oPPORTUNITY = db.OPPORTUNITies.Find(id);
            if (oPPORTUNITY == null)
            {
                return HttpNotFound();
            }
            ViewBag.DEPT_ID = new SelectList(db.departments, "dept_id", "dept_name", oPPORTUNITY.DEPT_ID);
            return View(oPPORTUNITY);
        }

        // POST: opportunities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Super Admin")]
        public ActionResult Edit([Bind(Include = "OPPORTUNITY_ID,OPPORTUNITY_TITLE,OPPORTUNITY_DESC,DEPT_ID,START_DATE,CONTACT_FN,CONTACT_LN,CONTACT_EMAIL")] OPPORTUNITY oPPORTUNITY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oPPORTUNITY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DEPT_ID = new SelectList(db.departments, "dept_id", "dept_name", oPPORTUNITY.DEPT_ID);
            return View(oPPORTUNITY);
        }

        // GET: opportunities/Delete/5
        [Authorize(Roles = "Admin, Super Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OPPORTUNITY oPPORTUNITY = db.OPPORTUNITies.Find(id);
            if (oPPORTUNITY == null)
            {
                return HttpNotFound();
            }
            return View(oPPORTUNITY);
        }

        // POST: opportunities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Super Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            OPPORTUNITY oPPORTUNITY = db.OPPORTUNITies.Find(id);
            db.OPPORTUNITies.Remove(oPPORTUNITY);
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
