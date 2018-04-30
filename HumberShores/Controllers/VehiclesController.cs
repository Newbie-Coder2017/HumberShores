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
    public class VehiclesController : Controller
    {
        private MattDatabaseEntities db = new MattDatabaseEntities();

        // GET: Vehicles
        public ActionResult Index()
        {
            var vEHICLES = db.VEHICLES.Include(v => v.site_users);
            return View(vEHICLES.ToList());
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VEHICLE vEHICLE = db.VEHICLES.Find(id);
            if (vEHICLE == null)
            {
                return HttpNotFound();
            }
            return View(vEHICLE);
        }

        // GET: Vehicles/Create
        public ActionResult Create()
        {
            ViewBag.USER_ID = new SelectList(db.site_users, "user_id", "role_code");
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LICENSE_NUMBER,USER_ID,MAKE,MODEL,YEAR")] VEHICLE vEHICLE)
        {
            if (ModelState.IsValid)
            {
                db.VEHICLES.Add(vEHICLE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.USER_ID = new SelectList(db.site_users, "user_id", "role_code", vEHICLE.USER_ID);
            return View(vEHICLE);
        }

        // GET: Vehicles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VEHICLE vEHICLE = db.VEHICLES.Find(id);
            if (vEHICLE == null)
            {
                return HttpNotFound();
            }
            ViewBag.USER_ID = new SelectList(db.site_users, "user_id", "role_code", vEHICLE.USER_ID);
            return View(vEHICLE);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LICENSE_NUMBER,USER_ID,MAKE,MODEL,YEAR")] VEHICLE vEHICLE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vEHICLE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.USER_ID = new SelectList(db.site_users, "user_id", "role_code", vEHICLE.USER_ID);
            return View(vEHICLE);
        }

        // GET: Vehicles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VEHICLE vEHICLE = db.VEHICLES.Find(id);
            if (vEHICLE == null)
            {
                return HttpNotFound();
            }
            return View(vEHICLE);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            VEHICLE vEHICLE = db.VEHICLES.Find(id);
            db.VEHICLES.Remove(vEHICLE);
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
