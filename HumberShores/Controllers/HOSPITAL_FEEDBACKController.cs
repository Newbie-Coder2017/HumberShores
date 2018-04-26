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

    public class HOSPITAL_FEEDBACKController : Controller
    {
        private MattDatabaseEntities db = new MattDatabaseEntities();

        // GET: HOSPITAL_FEEDBACK
        public ActionResult Index()
        {
            return View(db.HOSPITAL_FEEDBACK.ToList());
            //homepage will display list of vacancies
            //variable holds list of vacancies ON THE PUBLIC INDEX PAGE
            //List<HOSPITAL_FEEDBACK> hospital_feedback = db.HOSPITAL_FEEDBACK.ToList();
            //return View(hospital_feedback);
        }

        // GET: HOSPITAL_FEEDBACK/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOSPITAL_FEEDBACK hOSPITAL_FEEDBACK = db.HOSPITAL_FEEDBACK.Find(id);
            if (hOSPITAL_FEEDBACK == null)
            {
                return HttpNotFound();
            }
            return View(hOSPITAL_FEEDBACK);
        }

        // GET: HOSPITAL_FEEDBACK/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HOSPITAL_FEEDBACK/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_feedback,f_name,l_name,contact_email,type_feedback,depart_feedback,comment_feedback,date_feedback,is_publish")] HOSPITAL_FEEDBACK hOSPITAL_FEEDBACK)
        {
            if (ModelState.IsValid)
            {
                db.HOSPITAL_FEEDBACK.Add(hOSPITAL_FEEDBACK);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hOSPITAL_FEEDBACK);
        }

        // GET: HOSPITAL_FEEDBACK/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOSPITAL_FEEDBACK hOSPITAL_FEEDBACK = db.HOSPITAL_FEEDBACK.Find(id);
            if (hOSPITAL_FEEDBACK == null)
            {
                return HttpNotFound();
            }
            return View(hOSPITAL_FEEDBACK);
        }

        // POST: HOSPITAL_FEEDBACK/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_feedback,f_name,l_name,contact_email,type_feedback,depart_feedback,comment_feedback,date_feedback,is_publish")] HOSPITAL_FEEDBACK hOSPITAL_FEEDBACK)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hOSPITAL_FEEDBACK).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hOSPITAL_FEEDBACK);
        }

        // GET: HOSPITAL_FEEDBACK/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOSPITAL_FEEDBACK hOSPITAL_FEEDBACK = db.HOSPITAL_FEEDBACK.Find(id);
            if (hOSPITAL_FEEDBACK == null)
            {
                return HttpNotFound();
            }
            return View(hOSPITAL_FEEDBACK);
        }

        // POST: HOSPITAL_FEEDBACK/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HOSPITAL_FEEDBACK hOSPITAL_FEEDBACK = db.HOSPITAL_FEEDBACK.Find(id);
            db.HOSPITAL_FEEDBACK.Remove(hOSPITAL_FEEDBACK);
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
