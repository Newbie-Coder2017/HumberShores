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
    [Authorize(Roles = "User, Admin, Super Admin")]
    public class appointmentsController : Controller
    {
        private MattDatabaseEntities db = new MattDatabaseEntities();

        // GET: appointments
        public ActionResult Index()
        {
            var appointments = db.appointments.Include(a => a.employee).Include(a => a.site_users);
            return View(appointments.ToList());
        }

        private List<SelectListItem> GetEmployeeNamesEmpIds()
        {
            List<SelectListItem> EmployeeNamesEmpIds = new List<SelectListItem>();
            var result = from e in db.employees
                         join u in db.site_users
                         on e.user_id equals u.user_id
                         select new
                         {
                             fname = u.user_first_name,
                             lname = u.user_last_name,
                             empid = e.emp_id
                         };
            foreach (var r in result)
            {
                EmployeeNamesEmpIds.Add(new SelectListItem() { Value = r.empid.ToString(), Text = r.fname + " " + r.lname });
            }
            return EmployeeNamesEmpIds;
        }

        // GET: appointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            appointment appointment = db.appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: appointments/Create
        public ActionResult Create()
        {
            ViewBag.emp_name = GetEmployeeNamesEmpIds();
            ViewBag.emp_id = new SelectList(db.employees, "emp_id", "emp_id");
            return View();
        }

        // POST: appointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "app_id,emp_id,app_date,app_time,app_reason,app_comment,app_child,app_child_first,app_child_last,app_child_dob,app_child_gender")] appointment appointment)
        {
            if (ModelState.IsValid)
            {
                appointment.patient_id = Convert.ToInt32(Session["userId"]);
                db.appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.emp_name = GetEmployeeNamesEmpIds();
            ViewBag.emp_id = new SelectList(db.site_users, "emp_id", "emp_id", appointment.emp_id);
            return View(appointment);
        }

        // GET: appointments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            appointment appointment = db.appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.emp_name = GetEmployeeNamesEmpIds();
            ViewBag.emp_id = new SelectList(db.employees, "emp_id", "emp_id", appointment.emp_id);
            return View(appointment);
        }

        // POST: appointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "app_id,emp_id,app_date,app_time,app_reason,app_comment,app_child,app_child_first,app_child_last,app_child_dob,app_child_gender")] appointment appointment)
        {
            if (ModelState.IsValid)
            {
                appointment.patient_id = Convert.ToInt32(Session["userId"]);
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.emp_name = GetEmployeeNamesEmpIds();
            ViewBag.emp_id = new SelectList(db.employees, "emp_id", "emp_id", appointment.emp_id);
            return View(appointment);
        }

        // GET: appointments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            appointment appointment = db.appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            appointment appointment = db.appointments.Find(id);
            db.appointments.Remove(appointment);
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
