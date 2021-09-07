using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FPT_Training_System.Data.Models;

namespace FPT_Training_System.Web.Controllers
{
    public class ManageTrainerController : BaseController
    {
        //private TrainingProgramManager_contextContext _context = new TrainingProgramManager_contextContext();
        private readonly TrainingProgramManagerDbContext _context;

        public ManageTrainerController(
            TrainingProgramManagerDbContext context
        )
        {
            _context = context;
        }

        // GET: ManageTrainer

        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {

            var trainer = await (from u in _context.AspNetUsers
                                 join ur in _context.AspNetUserRoles on u.Id equals ur.UserId
                                 join r in _context.AspNetRoles on ur.RoleId equals r.Id
                                 where r.Name == "Trainer"
                                 select u).ToListAsync(cancellationToken);

            ViewBag.user = trainer;

            return View(await _context.AspNetUsers.ToListAsync(cancellationToken));
        }

        // GET: ManageTrainer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = _context.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // GET: ManageTrainer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManageTrainer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fullname,Email,Password,Age,Dob,Education,MainPpLanguage,ToeicScore,ExperienceDetails,Department,Location,Contact,isActive")] AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                _context.AspNetUsers.Add(aspNetUser);

                var insertedUserRoleId = new AspNetUserRole { RoleId = 3, UserId = aspNetUser.Id };

                _context.AspNetUserRoles.Add(insertedUserRoleId);

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aspNetUser);
        }

        // GET: ManageTrainer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = _context.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: ManageTrainer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fullname,Email,Password,Age,Dob,Education,MainPpLanguage,ToeicScore,ExperienceDetails,Department,Location,Contact,isActive")] AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(aspNetUser).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aspNetUser);
        }

        // GET: ManageTrainer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = _context.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: ManageTrainer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AspNetUser aspNetUser = _context.AspNetUsers.Find(id);
            //AspNetUserRole aspNetUserRole = _context.AspNetUserRoles.Find(id);
            //var aspNetUserRole = new AspNetUserRole { UserId = id };
            _context.AspNetUsers.Remove(aspNetUser);
            _context.AspNetUserRoles.Remove(_context.AspNetUserRoles.Single(a => a.UserId == id));
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
