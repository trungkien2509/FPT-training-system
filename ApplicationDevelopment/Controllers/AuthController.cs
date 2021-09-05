using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ApplicationDevelopment.Entities;
using ApplicationDevelopment.Models;
using ApplicationDevelopment.Providers;

namespace ApplicationDevelopment.Controllers
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

            _userProvider.SignIn(new LoginUserModel
            {
                Email = userEntity.Email,
                Fullname = userEntity.Fullname,
                UserId = userEntity.Id
            });

            ReturnUrl = "AspNetUsers/Index";

            return Redirect(ReturnUrl);

            //return RedirectToAction("Index","AspNetUsers");
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
