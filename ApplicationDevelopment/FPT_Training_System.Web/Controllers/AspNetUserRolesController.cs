using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using System;
using EventsData;
using FPT_Training_System.Data.Models;

namespace FPT_Training_System.Web.Controllers
{
    public class AspNetUserRolesController : BaseController
    {
        private readonly TrainingProgramManagerDbContext _context;
        public AspNetUserRolesController(
            TrainingProgramManagerDbContext context
        )
        {
            _context = context;
        }
        // GET: AspNetUsersRoles
        public ActionResult Index()
        {
            return View();
        }
    }
}