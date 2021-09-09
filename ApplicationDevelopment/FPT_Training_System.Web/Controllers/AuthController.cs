using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EventsData;
using FPT_Training_System.Data.Models;
using FPT_Training_System.Web.Models;
using FPT_Training_System.Web.Providers;

namespace FPT_Training_System.Web.Controllers
{
    public class AuthController : BaseController
    {
        private readonly TrainingProgramManagerDbContext _context;
        private readonly IHttpUserProvider _userProvider;

        public AuthController(
            TrainingProgramManagerDbContext context,
            IHttpUserProvider userProvider
        )
        {
            _context = context;
            _userProvider = userProvider;
        }

        [AllowAnonymous]
        // GET: AspNetUsers
        public async Task<ActionResult> Index()
        {
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        // GET: AspNetUsers
        public async Task<ActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> LoginCallback(LoginModel model, string ReturnUrl, CancellationToken cancellationToken)
        {
            try
            {
                //GetUser information
                var userEntity = await _context.AspNetUsers.FirstOrDefaultAsync(p => p.Email == model.Username && p.Password == model.Password);

                if (userEntity == null)
                {
                    //return View("Login");
                    return RedirectToAction("Login");
                }

                //var ctx = Request.GetOwinContext().Authentication;
                //var claims = new List<Claim>
                //{
                //    new Claim(ClaimTypes.Name, userEntity.Fullname),
                //    new Claim(ClaimTypes.Email, userEntity.Email)
                //    //new Claim(ClaimTypes.Role, "Foo")
                //};
                ////var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

                //var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie, ClaimTypes.Name, ClaimTypes.Role);

                //ctx.SignIn(new AuthenticationProperties
                //    {IsPersistent = true}, identity);

                var roles = await (from u in _context.AspNetUsers
                                   join ur in _context.AspNetUserRoles on u.Id equals ur.UserId
                                   join r in _context.AspNetRoles on ur.RoleId equals r.Id
                                   where u.Id == userEntity.Id
                                   select r.Name).ToListAsync(cancellationToken);

                _userProvider.SignIn(new LoginUserModel
                {
                    Email = userEntity.Email,
                    Fullname = userEntity.Fullname,
                    UserId = userEntity.Id,
                    Roles = roles
                });

                //var loggedUser = _userProvider.Provide();
                if (roles.Contains("Admin"))
                {
                    return RedirectToAction("Index", "ManageTrainer");
                }
                if (roles.Contains("Training Staff"))
                {
                    return RedirectToAction("Index", "ManageTrainer");
                }
                if (roles.Contains("Trainer"))
                {
                    return RedirectToAction("ViewAssignedCourse", "AssignedCourses");
                }
                if (roles.Contains("Trainee"))
                {
                    return RedirectToAction("ViewCourse", "Courses");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }


            //ReturnUrl = "AspNetUsers/Index";

            //return Redirect(ReturnUrl);


            return RedirectToAction("Login");

            //return RedirectToAction("Index", "AspNetUsers");
        }
    
        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            ctx.Authentication.SignOut();
            Session.Abandon();

            return RedirectToAction("Login");
        }
    }
}
