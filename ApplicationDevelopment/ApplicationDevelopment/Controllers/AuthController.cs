using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ApplicationDevelopment.Entities;
using ApplicationDevelopment.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace ApplicationDevelopment.Controllers
{
    public class AuthController : BaseController
    {
        private readonly TrainingProgramManagerDbContext _context;

        public AuthController(
            TrainingProgramManagerDbContext context
        )
        {
            _context = context;
        }

        [AllowAnonymous]
        // GET: AspNetUsers
        public async Task<ActionResult> Login()
        {
            return View();
        }


        [AllowAnonymous]
        public async Task<ActionResult> LoginCallback(LoginModel model, string ReturnUrl)
        {

            //GetUser information

            var ctx = Request.GetOwinContext().Authentication;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "name"), new Claim(ClaimTypes.Email, "email@email.com"), new Claim(ClaimTypes.Role, "Foo")
            };
            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            ctx.SignIn(new AuthenticationProperties
                {IsPersistent = true}, identity);



            return Redirect(ReturnUrl);
        }

        public ActionResult Logout()
        {
            // Call log out Sp.
            //FormsAuthentication.SignOut();
            //Session.Clear();
            //HttpContext.Session.Clear();

            var ctx = Request.GetOwinContext();
            ctx.Authentication.SignOut();
            Session.Abandon();

            return RedirectToAction("Login");
        }
    }
}
