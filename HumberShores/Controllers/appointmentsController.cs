using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HumberShores.Models;
using System.Data.Entity.Infrastructure;
using System.ComponentModel.DataAnnotations;

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

        public PartialViewResult _ShowApp()
        {
            var apps = db.appointments.Where(a => a.app_date > DateTime.Now).OrderBy(a => a.app_date).ToList();
            return PartialView(apps);
        }
        //[HttpPost]
        //public PartialViewResult ShowApp(int emp_id, DateTime app_date)
        //{
        //    var appointments = from a in db.appointments
        //                       where a.emp_id == emp_id
        //                       where a.app_date == app_date
        //                       select a;

        //    return PartialView();
        //}

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

        private List<SelectListItem> GetPatientNamesUserIds()
        {
            List<SelectListItem> PatientNamesUserIds = new List<SelectListItem>();
            var result = from u in db.site_users
                         select new
                         {
                             fname = u.user_first_name,
                             lname = u.user_last_name,
                             userid = u.user_id
                         };
            foreach (var r in result)
            {
                PatientNamesUserIds.Add(new SelectListItem() { Value = r.userid.ToString(), Text = r.fname + " " + r.lname });
            }
            return PatientNamesUserIds;
        }

        // GET: appointments/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                appointment appointment = db.appointments.Find(id);
                if (appointment == null)
                {
                    return RedirectToAction("Error");
                }
                return View(appointment);
            }
            catch (Exception Error)
            {
                ViewBag.Error = Error.GetBaseException().Message;
            }
            return View("Error");

        }

        // GET: appointments/Create
        public ActionResult Create()
        {
            ViewBag.user_name = GetPatientNamesUserIds();
            ViewBag.emp_name = GetEmployeeNamesEmpIds();
            ViewBag.emp_id = new SelectList(db.employees, "emp_id", "emp_id");
            return View();
        }

        // POST: appointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "app_id,emp_id,app_date,app_time,app_reason,app_comment,app_child,app_child_first,app_child_last,app_child_dob,app_child_gender")] appointment appointment, FormCollection form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(User.IsInRole("Admin") || User.IsInRole("Super Admin"))
                    {
                        appointment.patient_id = Convert.ToInt32(form["patient_id"]);
                    }
                    else
                    {
                        appointment.patient_id = Convert.ToInt32(Session["userId"]);
                    }
                    db.appointments.Add(appointment);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.user_name = GetPatientNamesUserIds();
                ViewBag.emp_name = GetEmployeeNamesEmpIds();
                ViewBag.emp_id = new SelectList(db.site_users, "emp_id", "emp_id", appointment.emp_id);
                return View(appointment);
            }
            catch (Exception Error)
            {
                ViewBag.Error = Error.GetBaseException().Message;
                if (ViewBag.Error.Contains("uniqueApp"))
                {
                    //throw new Exception("You must enter a valid User Id to create an announcement.");
                    ViewBag.Error = "There is already an appointment created for that time with your Doctor please pick a new time.";
                }
                else
                {
                    ViewBag.Error = Error.GetBaseException().Message;
                }
            }
            return View("Error");

        }

        // GET: appointments/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                appointment appointment = db.appointments.Find(id);
                if (appointment == null)
                {
                    return RedirectToAction("Error");
                }
                ViewBag.user_name = GetPatientNamesUserIds();
                ViewBag.emp_name = GetEmployeeNamesEmpIds();
                ViewBag.emp_id = new SelectList(db.employees, "emp_id", "emp_id", appointment.emp_id);
                return View(appointment);
            }
            catch (Exception Error)
            {
                ViewBag.Error = Error.GetBaseException().Message;
            }
            return View("Error");
           
        }

        // POST: appointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "app_id,emp_id,app_date,app_time,app_reason,app_comment,app_child,app_child_first,app_child_last,app_child_dob,app_child_gender")] appointment appointment, FormCollection form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (User.IsInRole("Admin") || User.IsInRole("Super Admin"))
                    {
                        appointment.patient_id = Convert.ToInt32(form["patient_id"]);
                    }
                    else
                    {
                        appointment.patient_id = Convert.ToInt32(Session["userId"]);
                    }
                    db.Entry(appointment).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.user_name = GetPatientNamesUserIds();
                ViewBag.emp_name = GetEmployeeNamesEmpIds();
                ViewBag.emp_id = new SelectList(db.employees, "emp_id", "emp_id", appointment.emp_id);
                return View(appointment);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ViewBag.Error = ex.GetBaseException().Message;
                //appointment.patient_id = Convert.ToInt32(Session["userId"]);
                //ViewBag.emp_name = GetEmployeeNamesEmpIds();
                ex.Entries.Single().Reload();
                db.SaveChanges();
                //return View(appointment);
            }
            return View("Error");
          
        }

        // GET: appointments/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                appointment appointment = db.appointments.Find(id);
                if (appointment == null)
                {
                    return RedirectToAction("Error");
                }
                return View(appointment);
            }
            catch (Exception Error)
            {
                ViewBag.Error = Error.GetBaseException().Message;
            }
            return View("Error");

        }

        // POST: appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                appointment appointment = db.appointments.Find(id);
                db.appointments.Remove(appointment);
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
