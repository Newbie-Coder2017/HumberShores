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
    public class physiciansController : Controller
    {
        private MattDatabaseEntities db = new MattDatabaseEntities();

        // GET: physicians
        public ActionResult Index()
        {
			//var physicians = db.physicians.Include(p => p.department).Include(p => p.employee).Include(p => p.province1).Include(p => p.specialty).Include(p => p.specialty1).Include(p => p.site_users);
			//return View(physicians.ToList());

			var physicians = db.physicians.Include(p => p.department).Include(p => p.specialty).Include(p => p.specialty1);

			List<SelectListItem> departments = new List<SelectListItem>();

			var result = db.physicians.Join(db.departments, p => p.department_id, d => d.dept_id, (p, d) => new
			{
				dept_id = p.department_id,
				dept_name = d.dept_name
			});

			foreach ( var r in result.Distinct())
			{
				//Build list of Departments actively assigned to the existing Physicians
				departments.Add(new SelectListItem() { Value = r.dept_id.ToString(), Text = r.dept_name });
			}

			ViewBag.depts = departments;

			return View(physicians.ToList());
		}

        // GET: physicians/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            physician physician = db.physicians.Find(id);
            if (physician == null)
            {
                return HttpNotFound();
            }
            return View(physician);
        }

		// GET: physicians/Create
		[Authorize(Roles = "Admin, Super Admin")]
		public ActionResult Create()
        {
			ViewBag.department_id = db.departments;
            ViewBag.emp_id = GetEmployeeNamesEmpIds();
            ViewBag.province = new SelectList(db.provinces, "id", "name");
            ViewBag.special1 = new SelectList(db.specialties, "special_id", "specialty_name");
            ViewBag.special2 = new SelectList(db.specialties, "special_id", "specialty_name");
            ViewBag.user_id = new SelectList(db.site_users, "user_id", "role_code");
            return View();
        }

		// POST: physicians/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin, Super Admin")]
		public ActionResult Create([Bind(Include = "doctor_id,emp_id,department_id,special1,special2,phone,email,website,street_address1,street_address2,building_name,city,province,postal_code,user_id,fax")] physician physician)
        {
            if (ModelState.IsValid)
            {
                db.physicians.Add(physician);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.department_id = new SelectList(db.departments, "dept_id", "dept_name", physician.department_id);
            ViewBag.emp_id = new SelectList(db.employees, "emp_id", "emp_position", physician.emp_id);
            ViewBag.province = new SelectList(db.provinces, "id", "name", physician.province);
            ViewBag.special1 = new SelectList(db.specialties, "special_id", "specialty_name", physician.special1);
            ViewBag.special2 = new SelectList(db.specialties, "special_id", "specialty_name", physician.special2);
            ViewBag.user_id = new SelectList(db.site_users, "user_id", "role_code", physician.user_id);
            return View(physician);
        }

		// GET: physicians/Edit/5
		[Authorize(Roles = "Admin, Super Admin")]
		public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            physician physician = db.physicians.Find(id);
            if (physician == null)
            {
                return HttpNotFound();
            }
            ViewBag.department_id = new SelectList(db.departments, "dept_id", "dept_desc", physician.department_id);
            ViewBag.emp_id = new SelectList(db.employees, "emp_id", "emp_position", physician.emp_id);
            ViewBag.province = new SelectList(db.provinces, "id", "name", physician.province);
            ViewBag.special1 = new SelectList(db.specialties, "special_id", "specialty_name", physician.special1);
            ViewBag.special2 = new SelectList(db.specialties, "special_id", "specialty_name", physician.special2);
            ViewBag.user_id = new SelectList(db.site_users, "user_id", "role_code", physician.user_id);
            return View(physician);
        }

		// POST: physicians/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin, Super Admin")]
		public ActionResult Edit([Bind(Include = "doctor_id,emp_id,department_id,special1,special2,phone,email,website,street_address1,street_address2,building_name,city,province,postal_code,user_id,fax")] physician physician)
        {
            if (ModelState.IsValid)
            {
                db.Entry(physician).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.department_id = new SelectList(db.departments, "dept_id", "dept_desc", physician.department_id);
            ViewBag.emp_id = new SelectList(db.employees, "emp_id", "emp_position", physician.emp_id);
            ViewBag.province = new SelectList(db.provinces, "id", "name", physician.province);
            ViewBag.special1 = new SelectList(db.specialties, "special_id", "specialty_name", physician.special1);
            ViewBag.special2 = new SelectList(db.specialties, "special_id", "specialty_name", physician.special2);
            ViewBag.user_id = new SelectList(db.site_users, "user_id", "role_code", physician.user_id);
            return View(physician);
        }

		// GET: physicians/Delete/5
		[Authorize(Roles = "Admin, Super Admin")]
		public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            physician physician = db.physicians.Find(id);
            if (physician == null)
            {
                return HttpNotFound();
            }
            return View(physician);
        }

		// POST: physicians/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin, Super Admin")]
		public ActionResult DeleteConfirmed(int id)
        {
            physician physician = db.physicians.Find(id);
            db.physicians.Remove(physician);
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

		public PartialViewResult ListPhysicians(List<physician> physicians)
		{
			//var physicians = db.physicians.Include(p => p.department).Include(p => p.employee).Include(p => p.specialty).Include(p => p.specialty1);

			return PartialView("~/Views/physicians/_ListPhysicians.cshtml", physicians);
		}

		[HttpPost]
		public PartialViewResult ListPhysicians(FormCollection form)
		{
			int department_id;
			List<physician> physicians;
			if (form["depts"] == null || form["depts"].ToString() == "")
			{
				department_id = 0;
			}

			bool result = Int32.TryParse(form["depts"], out department_id);
			if (department_id != 0)
			{
				physicians = (db.physicians.Where(p => p.department_id == department_id)).ToList();
			}
			else
			{
				physicians = (db.physicians).ToList();
			}
			return PartialView("~/Views/physicians/_ListPhysicians.cshtml", physicians);
		}

		private List<SelectListItem> GetEmployeeNamesEmpIds()
		{
			List<SelectListItem> EmployeeNamesEmpIds = new List<SelectListItem>();
			var result = from e in db.employees
						 join u in db.site_users
						 on e.user_id equals u.user_id
						 where e.emp_position == "Doctor" || e.emp_position == "Surgeon"//Limit to employees of position Doctor and Surgeon
						 select new
						 {
							 fname = u.user_first_name,
							 lname = u.user_last_name,
							 empid = e.emp_id
						 };


			foreach (var r in result)
			{
				//Build List to send to the view
				EmployeeNamesEmpIds.Add(new SelectListItem() { Value = r.empid.ToString(), Text = r.fname + " " + r.lname });
			}

			return EmployeeNamesEmpIds;//Holds the list of the employee IDs and Names of the employees with position "head"
		}
	}
}
