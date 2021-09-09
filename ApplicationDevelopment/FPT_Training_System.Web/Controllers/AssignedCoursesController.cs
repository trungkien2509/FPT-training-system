using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using FPT_Training_System.Data.Models;

namespace FPT_Training_System.Web.Controllers
{
    public class AssignedCoursesController : BaseController
    {
        //private TrainingProgramManager_contextContext _context = new TrainingProgramManager_contextContext();
        private readonly TrainingProgramManagerDbContext _context;
        public AssignedCoursesController(
           TrainingProgramManagerDbContext context
       )
        {
            _context = context;
        }

        // GET: AssignedCourses
        public async Task<ActionResult> Index()
        {
            var assignedCourses = _context.AssignedCourses.Include(a => a.AspNetUser).Include(a => a.Course);
            return View(assignedCourses.ToList());
        }

        public async Task<ActionResult> ViewAssignedCourse()
        {
            var assignedCourses = _context.AssignedCourses.Include(a => a.AspNetUser).Include(a => a.Course);
            return View(assignedCourses.ToList());
        }

        // GET: AssignedCourses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AssignedCourse assignedCourse = _context.AssignedCourses.Find(id);
            if (assignedCourse == null)
            {
                return HttpNotFound();
            }

            return View(assignedCourse);
        }

        // GET: AssignedCourses/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.UserId = new SelectList(_context.AspNetUsers, "Id", "Fullname");
            ViewBag.CourseId = new SelectList(_context.Courses, "CourseId", "CourseName");
            return View();
        }

        // POST: AssignedCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AssignedId,UserId,CourseId")] AssignedCourse assignedCourse)
        {
            if (ModelState.IsValid)
            {
                _context.AssignedCourses.Add(assignedCourse);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(_context.AspNetUsers, "Id", "Fullname", assignedCourse.UserId);
            ViewBag.CourseId = new SelectList(_context.Courses, "CourseId", "CourseName", assignedCourse.CourseId);
            return View(assignedCourse);
        }

        // GET: AssignedCourses/Create
        public async Task<ActionResult> AssignTrainer(CancellationToken cancellationToken)
        {
            var trainer = await (from u in _context.AspNetUsers
                                 join ur in _context.AspNetUserRoles on u.Id equals ur.UserId
                                 join r in _context.AspNetRoles on ur.RoleId equals r.Id
                                 where r.Name == "Trainer"
                                 select u).ToListAsync(cancellationToken);

            ViewBag.UserId = new SelectList(trainer, "Id", "Fullname");
            ViewBag.CourseId = new SelectList(_context.Courses, "CourseId", "CourseName");
            return View();
        }

        // POST: AssignedCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssignTrainer([Bind(Include = "AssignedId,UserId,CourseId")] AssignedCourse assignedCourse)
        {
            if (ModelState.IsValid)
            {
                _context.AssignedCourses.Add(assignedCourse);
                _context.SaveChanges();
                return RedirectToAction("Index", "Courses");
            }

            ViewBag.UserId = new SelectList(_context.AspNetUsers, "Id", "Fullname", assignedCourse.UserId);
            ViewBag.CourseId = new SelectList(_context.Courses, "CourseId", "CourseName", assignedCourse.CourseId);
            return View(assignedCourse);
        }

        // GET: AssignedCourses/Create
        public async Task<ActionResult> AssignTrainee(CancellationToken cancellationToken)
        {
            var trainee = await (from u in _context.AspNetUsers
                                 join ur in _context.AspNetUserRoles on u.Id equals ur.UserId
                                 join r in _context.AspNetRoles on ur.RoleId equals r.Id
                                 where r.Name == "Trainee"
                                 select u).ToListAsync(cancellationToken);

            ViewBag.UserId = new SelectList(trainee, "Id", "Fullname");
            ViewBag.CourseId = new SelectList(_context.Courses, "CourseId", "CourseName");
            return View();
        }

        // POST: AssignedCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssignTrainee([Bind(Include = "AssignedId,UserId,CourseId")] AssignedCourse assignedCourse)
        {
            if (ModelState.IsValid)
            {
                _context.AssignedCourses.Add(assignedCourse);
                _context.SaveChanges();
                return RedirectToAction("Index", "Courses");
            }

            ViewBag.UserId = new SelectList(_context.AspNetUsers, "Id", "Fullname", assignedCourse.UserId);
            ViewBag.CourseId = new SelectList(_context.Courses, "CourseId", "CourseName", assignedCourse.CourseId);
            return View(assignedCourse);
        }

        // GET: AssignedCourses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AssignedCourse assignedCourse = _context.AssignedCourses.Find(id);
            if (assignedCourse == null)
            {
                return HttpNotFound();
            }

            ViewBag.UserId = new SelectList(_context.AspNetUsers, "Id", "Fullname", assignedCourse.UserId);
            ViewBag.CourseId = new SelectList(_context.Courses, "CourseId", "CourseName", assignedCourse.CourseId);
            return View(assignedCourse);
        }

        // POST: AssignedCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AssignedId,UserId,CourseId")] AssignedCourse assignedCourse)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(assignedCourse).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(_context.AspNetUsers, "Id", "Fullname", assignedCourse.UserId);
            ViewBag.CourseId = new SelectList(_context.Courses, "CourseId", "CourseName", assignedCourse.CourseId);
            return View(assignedCourse);
        }

        // GET: AssignedCourses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AssignedCourse assignedCourse = _context.AssignedCourses.Find(id);
            if (assignedCourse == null)
            {
                return HttpNotFound();
            }

            return View(assignedCourse);
        }

        // POST: AssignedCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AssignedCourse assignedCourse = _context.AssignedCourses.Find(id);
            _context.AssignedCourses.Remove(assignedCourse);
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
