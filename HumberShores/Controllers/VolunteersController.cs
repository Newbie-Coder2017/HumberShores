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
    public class VolunteersController : Controller
    {
        private MattDatabaseEntities db = new MattDatabaseEntities();

        // GET: Volunteers
        [Authorize(Roles = "Admin, Super Admin")]
        public ActionResult Index()
        {
            var vOLUNTEERs = db.VOLUNTEERs.Include(v => v.OPPORTUNITY);
            return View(vOLUNTEERs.ToList());
        }

        // GET: Volunteers/Details/5
        [Authorize(Roles = "Admin, Super Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VOLUNTEER vOLUNTEER = db.VOLUNTEERs.Find(id);
            if (vOLUNTEER == null)
            {
                return HttpNotFound();
            }
            return View(vOLUNTEER);
        }

        // GET: Volunteers/Create

        public ActionResult Create()
        {
            ViewBag.OPPORTUNITY_ID = new SelectList(db.OPPORTUNITies, "OPPORTUNITY_ID", "OPPORTUNITY_TITLE");
            return View();
        }

        // POST: Volunteers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VOLUNTEER_ID,FIRST_NAME,MIDDLE_NAME,LAST_NAME,OCCUPATION,OPPORTUNITY_ID,PHONE_NUMBER,STREET_ADDRESS,CITY,PROVINCE,POSTAL_CODE,EMAIL,DATE_OF_BIRTH,GENDER,LICENCE,START_DATE")] VOLUNTEER vOLUNTEER)
        {
            if (ModelState.IsValid)
            {
                db.VOLUNTEERs.Add(vOLUNTEER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OPPORTUNITY_ID = new SelectList(db.OPPORTUNITies, "OPPORTUNITY_ID", "OPPORTUNITY_TITLE", vOLUNTEER.OPPORTUNITY_ID);
            return View(vOLUNTEER);
        }

        // GET: Volunteers/Edit/5
        [Authorize(Roles = "Admin, Super Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VOLUNTEER vOLUNTEER = db.VOLUNTEERs.Find(id);
            if (vOLUNTEER == null)
            {
                return HttpNotFound();
            }
            ViewBag.OPPORTUNITY_ID = new SelectList(db.OPPORTUNITies, "OPPORTUNITY_ID", "OPPORTUNITY_TITLE", vOLUNTEER.OPPORTUNITY_ID);
            return View(vOLUNTEER);
        }

        // POST: Volunteers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Super Admin")]
        public ActionResult Edit([Bind(Include = "VOLUNTEER_ID,FIRST_NAME,MIDDLE_NAME,LAST_NAME,OCCUPATION,OPPORTUNITY_ID,PHONE_NUMBER,STREET_ADDRESS,CITY,PROVINCE,POSTAL_CODE,EMAIL,DATE_OF_BIRTH,GENDER,LICENCE,START_DATE")] VOLUNTEER vOLUNTEER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vOLUNTEER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OPPORTUNITY_ID = new SelectList(db.OPPORTUNITies, "OPPORTUNITY_ID", "OPPORTUNITY_TITLE", vOLUNTEER.OPPORTUNITY_ID);
            return View(vOLUNTEER);
        }

        // GET: Volunteers/Delete/5
        [Authorize(Roles = "Admin, Super Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VOLUNTEER vOLUNTEER = db.VOLUNTEERs.Find(id);
            if (vOLUNTEER == null)
            {
                return HttpNotFound();
            }
            return View(vOLUNTEER);
        }

        // POST: Volunteers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Super Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            VOLUNTEER vOLUNTEER = db.VOLUNTEERs.Find(id);
            db.VOLUNTEERs.Remove(vOLUNTEER);
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
