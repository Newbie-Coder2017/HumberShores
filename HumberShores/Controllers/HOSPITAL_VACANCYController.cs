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
    public class HOSPITAL_VACANCYController : Controller
    {
        private MattDatabaseEntities db = new MattDatabaseEntities();

        // GET: HOSPITAL_VACANCY
        public ActionResult Index()
        {
            return View(db.HOSPITAL_VACANCY.ToList());
        }

        // GET: HOSPITAL_VACANCY/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOSPITAL_VACANCY hOSPITAL_VACANCY = db.HOSPITAL_VACANCY.Find(id);
            if (hOSPITAL_VACANCY == null)
            {
                return HttpNotFound();
            }
            return View(hOSPITAL_VACANCY);
        }

        // GET: HOSPITAL_VACANCY/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HOSPITAL_VACANCY/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "vacancy_id,job_title,apply_due_date,description,description_id,job_type,no_of_vacancy,contact_email")] HOSPITAL_VACANCY hOSPITAL_VACANCY)
        {
            if (ModelState.IsValid)
            {
                db.HOSPITAL_VACANCY.Add(hOSPITAL_VACANCY);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hOSPITAL_VACANCY);
        }

        // GET: HOSPITAL_VACANCY/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOSPITAL_VACANCY hOSPITAL_VACANCY = db.HOSPITAL_VACANCY.Find(id);
            if (hOSPITAL_VACANCY == null)
            {
                return HttpNotFound();
            }
            return View(hOSPITAL_VACANCY);
        }

        // POST: HOSPITAL_VACANCY/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "vacancy_id,job_title,apply_due_date,description,description_id,job_type,no_of_vacancy,contact_email")] HOSPITAL_VACANCY hOSPITAL_VACANCY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hOSPITAL_VACANCY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hOSPITAL_VACANCY);
        }

        // GET: HOSPITAL_VACANCY/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOSPITAL_VACANCY hOSPITAL_VACANCY = db.HOSPITAL_VACANCY.Find(id);
            if (hOSPITAL_VACANCY == null)
            {
                return HttpNotFound();
            }
            return View(hOSPITAL_VACANCY);
        }

        // POST: HOSPITAL_VACANCY/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HOSPITAL_VACANCY hOSPITAL_VACANCY = db.HOSPITAL_VACANCY.Find(id);
            db.HOSPITAL_VACANCY.Remove(hOSPITAL_VACANCY);
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
