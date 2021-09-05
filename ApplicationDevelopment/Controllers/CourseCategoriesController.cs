﻿using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ApplicationDevelopment.Entities;

namespace ApplicationDevelopment.Controllers
{
    public class CourseCategoriesController : BaseController
    {
        private TrainingProgramManagerDbContext db = new TrainingProgramManagerDbContext();

        // GET: CourseCategories
        public ActionResult Index()
        {
            return View(db.CourseCategories.ToList());
        }

        // GET: CourseCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CourseCategory courseCategory = db.CourseCategories.Find(id);
            if (courseCategory == null)
            {
                return HttpNotFound();
            }

            return View(courseCategory);
        }

        // GET: CourseCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourseCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseCateId,CourseCateName,CourseCateDescription")]CourseCategory courseCategory)
        {
            if (ModelState.IsValid)
            {
                db.CourseCategories.Add(courseCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(courseCategory);
        }

        // GET: CourseCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CourseCategory courseCategory = db.CourseCategories.Find(id);
            if (courseCategory == null)
            {
                return HttpNotFound();
            }

            return View(courseCategory);
        }

        // POST: CourseCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseCateId,CourseCateName,CourseCateDescription")]CourseCategory courseCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(courseCategory);
        }

        // GET: CourseCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CourseCategory courseCategory = db.CourseCategories.Find(id);
            if (courseCategory == null)
            {
                return HttpNotFound();
            }

            return View(courseCategory);
        }

        // POST: CourseCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseCategory courseCategory = db.CourseCategories.Find(id);
            db.CourseCategories.Remove(courseCategory);
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
