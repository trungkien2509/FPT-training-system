using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EventsData;
using FPT_Training_System.Data.Models;

namespace FPT_Training_System.Web.Controllers
{
    public class CoursesController : BaseController
    {
        private TrainingProgramManagerDbContext db = new TrainingProgramManagerDbContext();

        // GET: Courses
        public ActionResult Index(string searchString)
        {
            var searchCourse = (from c in db.Courses
                                select c).ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                searchCourse = searchCourse.Where(s => s.CourseName.Contains(searchString)).ToList();
            }
            var courses = db.Courses.Include(c => c.AssignedCourses).Include(c => c.CourseCategory);
            return View(searchCourse);
        }
        public ActionResult ViewCourse()
        {
            var courses = db.Courses.Include(c => c.AssignedCourses).Include(c => c.CourseCategory);
            return View(courses.ToList());
        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.AssignedCourses, "AssignedId", "AssignedId");
            ViewBag.CourseCateId = new SelectList(db.CourseCategories, "CourseCateId", "CourseCateName");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseId,CourseName,CourseCateId,CourseDescription")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.AssignedCourses, "AssignedId", "AssignedId", course.CourseId);
            ViewBag.CourseCateId = new SelectList(db.CourseCategories, "CourseCateId", "CourseCateName", course.CourseCateId);
            return View(course);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }

            ViewBag.CourseId = new SelectList(db.AssignedCourses, "AssignedId", "AssignedId", course.CourseId);
            ViewBag.CourseCateId = new SelectList(db.CourseCategories, "CourseCateId", "CourseCateName", course.CourseCateId);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseId,CourseName,CourseCateId,CourseDescription")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.AssignedCourses, "AssignedId", "AssignedId", course.CourseId);
            ViewBag.CourseCateId = new SelectList(db.CourseCategories, "CourseCateId", "CourseCateName", course.CourseCateId);
            return View(course);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
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
