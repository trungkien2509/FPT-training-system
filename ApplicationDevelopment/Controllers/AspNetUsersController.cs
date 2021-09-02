using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using ApplicationDevelopment.Entities;

namespace ApplicationDevelopment.Controllers
{
    public class AspNetUsersController : BaseController
    {
        //private readonly TrainingProgramManagerDbContext db = new TrainingProgramManagerDbContext();
        private readonly TrainingProgramManagerDbContext _context;

        public AspNetUsersController(
            TrainingProgramManagerDbContext context
        )
        {
            _context = context;
        }

        // GET: AspNetUsers
        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            return View(await _context.AspNetUsers.ToListAsync(cancellationToken));
        }

        // GET: AspNetUsers/Details/5
        public async Task<ActionResult> Details(int? id, CancellationToken cancellationToken)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var aspNetUser = await _context.AspNetUsers.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
          
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }

            return View(aspNetUser);
        }

        // GET: AspNetUsers/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}
        public async Task<ActionResult> Create(CancellationToken cancellationToken)
        {
            return View();
        }
        // POST: AspNetUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Fullname,Email,Password,Age,Dob,Education,MainPpLanguage,ToeicScore,ExperienceDetails,Department,Location,isActive")]AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                _context.AspNetUsers.Add(aspNetUser);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aspNetUser);
        }

        // GET: AspNetUsers/Edit/5
        public async Task<ActionResult> Edit(int? id)
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

        // POST: AspNetUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Fullname,Email,Password,Age,Dob,Education,MainPpLanguage,ToeicScore,ExperienceDetails,Department,Location,isActive")]AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(aspNetUser).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aspNetUser);
        }

        // GET: AspNetUsers/Delete/5
        public async Task<ActionResult> Delete(int? id)
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

        // POST: AspNetUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AspNetUser aspNetUser = _context.AspNetUsers.Find(id);
            _context.AspNetUsers.Remove(aspNetUser);
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
