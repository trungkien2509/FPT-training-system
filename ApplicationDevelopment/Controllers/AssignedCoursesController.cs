using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ApplicationDevelopment.Entities;

namespace ApplicationDevelopment.Controllers
{
    public class AssignedCoursesController : BaseController
    {
        private TrainingProgramManagerDbContext db = new TrainingProgramManagerDbContext();

        // GET: AssignedCourses
        public ActionResult Index()
        {
            var assignedCourses = db.AssignedCourses.Include(a => a.AspNetUser).Include(a => a.Course);
            return View(assignedCourses.ToList());
        }

        // GET: AssignedCourses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AssignedCourse assignedCourse = db.AssignedCourses.Find(id);
            if (assignedCourse == null)
            {
                return HttpNotFound();
            }

            return View(assignedCourse);
        }

        // GET: AssignedCourses/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Fullname");
            ViewBag.AssignedId = new SelectList(db.Courses, "CourseId", "CourseName");
            return View();
        }

        // POST: AssignedCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssignedId,UserId,CourseId")]AssignedCourse assignedCourse)
        {
            if (ModelState.IsValid)
            {
                db.AssignedCourses.Add(assignedCourse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Fullname", assignedCourse.UserId);
            ViewBag.AssignedId = new SelectList(db.Courses, "CourseId", "CourseName", assignedCourse.AssignedId);
            return View(assignedCourse);
        }

        // GET: AssignedCourses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AssignedCourse assignedCourse = db.AssignedCourses.Find(id);
            if (assignedCourse == null)
            {
                return HttpNotFound();
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Fullname", assignedCourse.UserId);
            ViewBag.AssignedId = new SelectList(db.Courses, "CourseId", "CourseName", assignedCourse.AssignedId);
            return View(assignedCourse);
        }

        // POST: AssignedCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssignedId,UserId,CourseId")]AssignedCourse assignedCourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignedCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Fullname", assignedCourse.UserId);
            ViewBag.AssignedId = new SelectList(db.Courses, "CourseId", "CourseName", assignedCourse.AssignedId);
            return View(assignedCourse);
        }

        // GET: AssignedCourses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AssignedCourse assignedCourse = db.AssignedCourses.Find(id);
            if (assignedCourse == null)
            {
                return HttpNotFound();
            }

            return View(assignedCourse);
        }

        // POST: AssignedCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssignedCourse assignedCourse = db.AssignedCourses.Find(id);
            db.AssignedCourses.Remove(assignedCourse);
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
