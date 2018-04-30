using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HumberShores.Models;

namespace Parking02.Controllers
{
    public class PassTypeController : Controller
    {
        private MattDatabaseEntities db = new MattDatabaseEntities();

        // GET: PassType
        [Authorize(Roles = "Admin, Super Admin")]
        public ActionResult Index()
        {
            return View(db.PASS_TYPE.ToList());
        }

        // GET: PassType/Details/5
        [Authorize(Roles = "Admin, Super Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PASS_TYPE pASS_TYPE = db.PASS_TYPE.Find(id);
            if (pASS_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(pASS_TYPE);
        }

        // GET: PassType/Create
        [Authorize(Roles = "Admin, Super Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PassType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Super Admin")]
        public ActionResult Create([Bind(Include = "TYPE_ID,PASS_TYPE1,PRICE")] PASS_TYPE pASS_TYPE)
        {
            if (ModelState.IsValid)
            {
                db.PASS_TYPE.Add(pASS_TYPE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pASS_TYPE);
        }

        // GET: PassType/Edit/5
        [Authorize(Roles = "Admin, Super Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PASS_TYPE pASS_TYPE = db.PASS_TYPE.Find(id);
            if (pASS_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(pASS_TYPE);
        }

        // POST: PassType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Super Admin")]
        public ActionResult Edit([Bind(Include = "TYPE_ID,PASS_TYPE1,PRICE")] PASS_TYPE pASS_TYPE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pASS_TYPE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pASS_TYPE);
        }

        // GET: PassType/Delete/5
        [Authorize(Roles = "Admin, Super Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PASS_TYPE pASS_TYPE = db.PASS_TYPE.Find(id);
            if (pASS_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(pASS_TYPE);
        }

        // POST: PassType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Super Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            PASS_TYPE pASS_TYPE = db.PASS_TYPE.Find(id);
            db.PASS_TYPE.Remove(pASS_TYPE);
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
