using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HumberShores.Models;
using System.Data.SqlClient;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;

namespace HumberShores.Controllers
{
    public class departmentsController : Controller
    {
        private MattDatabaseEntities db = new MattDatabaseEntities();

        // GET: departments
        public ActionResult Index()
        {
			//var departments = db.departments.Include(d => d.employee).Include(d => d.property_sections);
			//return View(departments.ToList());

			var depts = db.departments.OrderBy(model => model.dept_name);
			return View(depts);
		}

        // GET: departments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            department department = db.departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

		// GET: departments/Create
		[Authorize(Roles = "Admin, Super Admin")]
		public ActionResult Create()
        {
            ViewBag.dept_head = GetEmployeeNamesEmpIds();
			ViewBag.sections = db.property_sections;
			return View();
        }

        // POST: departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin, Super Admin")]
		public ActionResult Create([Bind(Include = "dept_id,dept_head,dept_desc,dept_name,dept_phone,section")] department department)
        {
			try
			{
				if (ModelState.IsValid)
				{
					db.departments.Add(department);
					db.SaveChanges();
					return RedirectToAction("Index");
				}
			}
			catch (DbEntityValidationException dbValEx)
			{
				foreach (var error in dbValEx.EntityValidationErrors)
				{
					ViewBag.ExceptionMsg += error.Entry.Entity;
					foreach (var valErr in error.ValidationErrors)
					{
						ViewBag.ExceptionMsg += "<br/>" + valErr.PropertyName;
						ViewBag.ExceptionMsg += "<br/>" + valErr.ErrorMessage;
					}
				}
				return View("~/Views/Error/Error.cshtml");
			}
			catch (DbUpdateException dbException)
			{
				ViewBag.ExceptionMsg = dbException.Message;
				ViewBag.ExceptionMsg += "<br/>" + dbException.InnerException.StackTrace;
				ViewBag.ExceptionMsg += "<br/>" + dbException.InnerException.HelpLink;

				ViewBag.ExceptionMsg += dbException.InnerException.Data;
				return View("~/Views/Error/Error.cshtml");
			}
			catch (SqlException sqlEx)
			{
				ViewBag.ExceptionMsg = sqlEx.Message;
				return View("~/Views/Error/Error.cshtml");
			}

			catch (Exception genericErr)
			{
				ViewBag.ExceptionMsg = genericErr.Message;
				ViewBag.ExceptionMsg += "<br/>" + genericErr.InnerException;
				return View("~/Views/Error/Error.cshtml");
			}


			ViewBag.dept_head = GetEmployeeNamesEmpIds();
			ViewBag.sections = db.property_sections;
			return View(department);
        }

		// GET: departments/Edit/5
		[Authorize(Roles = "Admin, Super Admin")]
		public ActionResult Edit(int? id)
        {
            if (id == null)
            {
				return RedirectToAction("Index");
			}
            department department = db.departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
			ViewBag.dept_head = GetEmployeeNamesEmpIds();
			ViewBag.sections = db.property_sections;
			return View(department);
        }

		// POST: departments/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin, Super Admin")]
		public ActionResult Edit([Bind(Include = "dept_id,dept_head,dept_desc,dept_name,dept_phone,section")] department department)
        {
			try
			{			
				if (ModelState.IsValid)
				{
					db.Entry(department).State = EntityState.Modified;
					db.SaveChanges();
					return RedirectToAction("Index");
				}
			}
			catch (Exception genericErr)
			{
				ViewBag.ExceptionMsg = genericErr.Message;
				ViewBag.ExceptionMsg += "<br/>" + genericErr.InnerException;
				return View("~/Views/Error/Error.cshtml");
			}

			ViewBag.dept_head = new SelectList(db.employees, "emp_id", "emp_position", department.dept_head);
			ViewBag.sections = db.property_sections;
			return View(department);
        }

		// GET: departments/Delete/5
		[Authorize(Roles = "Super Admin")]
		public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            department department = db.departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Super Admin")]
		public ActionResult DeleteConfirmed(int id)
        {
			try
			{
				department department = db.departments.Find(id);
				db.departments.Remove(department);
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			catch (Exception genericErr)
			{
				ViewBag.ExceptionMsg = genericErr.Message;
			}
			return View("~/Views/Error/Error.cshtml");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

		[HttpPost]
		public PartialViewResult ShowDepartment(FormCollection form)
		{
			int dept_id;
			bool result = Int32.TryParse(form["departments"], out dept_id);

			if (!result)
			{
				return PartialView("~/Views/departments/_ShowDepartmentEmpty.cshtml");
			}

			department depts = db.departments.SingleOrDefault(d => d.dept_id == dept_id);

			return PartialView("~/Views/departments/_ShowDepartment.cshtml", depts);
		}

		[HttpPost]
		public PartialViewResult SectionDepartments(FormCollection form)
		{
			int section_id;
			bool result = Int32.TryParse(form["section"], out section_id);//Handle true and false of this result
			
			//SECTION_ID IS TRUE - LOOK FOR DEPARTMENTS WITH A MATCHING SECTION VALUE
			var deptsInSection = db.departments.Where(d => d.section == section_id);
			List<department> depts = new List<department>();

			//IF THE SECTION HAS NO CONTAINED DEPARTMENTS (LIKE ENTRANCE/PARKING LOT) - REDIRECT TO SHOW SECTION DESCRIPTION
			if (!deptsInSection.Any())
			{
				return PartialView("~/Views/departments/_SectionDescription.cshtml", db.property_sections.SingleOrDefault(s => s.id == section_id));
			}

			foreach (var item in deptsInSection)
			{
				depts.Add(item);
			}

			return PartialView("~/Views/departments/_SectionDepartments.cshtml", depts);
		}

		private List<SelectListItem> GetEmployeeNamesEmpIds()
		{
			List<SelectListItem> EmployeeNamesEmpIds = new List<SelectListItem>();
			var result = from e in db.employees
						 join u in db.site_users
						 on e.user_id equals u.user_id
						 //where e.position == "head" //Limit to employees of position Head
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

		[HttpPost]
		public PartialViewResult SectionDescription(FormCollection form)
		{
			int section_id;

			bool result = Int32.TryParse(form["section"], out section_id);
			property_sections prop_section = new property_sections();

			prop_section = db.property_sections.SingleOrDefault(model => model.id == section_id);
			return PartialView("~/Views/departments/_SectionDescription.cshtml", prop_section);
		}

	}
}
