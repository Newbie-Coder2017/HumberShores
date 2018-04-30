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
    public class PassesController : Controller
    {
        private MattDatabaseEntities db = new MattDatabaseEntities();

        // GET: Passes
        public ActionResult Index()
        {
            var pASSes = db.PASSes.Include(p => p.PASS_TYPE1).Include(p => p.site_users);
            return View(pASSes.ToList());
        }

        // GET: Passes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PASS pASS = db.PASSes.Find(id);
            if (pASS == null)
            {
                return HttpNotFound();
            }
            return View(pASS);
        }

        // GET: Passes/Create
        public ActionResult Create()
        {
            ViewBag.PASS_TYPE = new SelectList(db.PASS_TYPE, "TYPE_ID", "PASS_TYPE1");
            return View();
        }

        // POST: Passes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PASS_ID, USER_ID,PASS_TYPE, PURCHASE_DATE,EXPIRY_DATE,PURCHASE_SUCCESS")] PASS pASS)
        {
            pASS.USER_ID = Convert.ToInt32(Session["userId"]);

            pASS.PURCHASE_DATE = DateTime.Now;

            if(pASS.PASS_TYPE == 1)
            {
                pASS.EXPIRY_DATE = pASS.PURCHASE_DATE.AddMonths(1);  
            }
            else if(pASS.PASS_TYPE == 2)
            {
                pASS.EXPIRY_DATE = pASS.PURCHASE_DATE.AddDays(7); 
            }


            if (ModelState.IsValid)
            {
                
                db.PASSes.Add(pASS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

             
            
            ViewBag.PASS_TYPE = new SelectList(db.PASS_TYPE, "TYPE_ID", "PASS_TYPE1", pASS.PASS_TYPE);
            
            return View(pASS);
        }

        // GET: Passes/Edit/5
        [Authorize(Roles = "Admin, Super Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PASS pASS = db.PASSes.Find(id);
            if (pASS == null)
            {
                return HttpNotFound();
            }
            ViewBag.PASS_TYPE = new SelectList(db.PASS_TYPE, "TYPE_ID", "PASS_TYPE1", pASS.PASS_TYPE);
            ViewBag.USER_ID = new SelectList(db.site_users, "user_id", "user_id", pASS.USER_ID);
            return View(pASS);
        }

        // POST: Passes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PASS_ID,USER_ID,PASS_TYPE,PURCHASE_DATE,EXPIRY_DATE,PURCHASE_SUCCESS")] PASS pASS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pASS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PASS_TYPE = new SelectList(db.PASS_TYPE, "TYPE_ID", "PASS_TYPE1", pASS.PASS_TYPE);
            ViewBag.USER_ID = new SelectList(db.site_users, "user_id", "user_id", pASS.USER_ID);
            return View(pASS);
        }

        // GET: Passes/Delete/5
        [Authorize(Roles = "Admin, Super Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PASS pASS = db.PASSes.Find(id);
            if (pASS == null)
            {
                return HttpNotFound();
            }
            return View(pASS);
        }

        // POST: Passes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Super Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            PASS pASS = db.PASSes.Find(id);
            db.PASSes.Remove(pASS);
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
