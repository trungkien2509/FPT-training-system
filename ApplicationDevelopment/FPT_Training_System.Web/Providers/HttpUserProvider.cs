using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using FPT_Training_System.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Newtonsoft.Json;

namespace FPT_Training_System.Web.Providers
{
    public interface IProvider<T>
    {
        T Provide();
    }

    public interface IHttpUserProvider : IProvider<LoginUserModel>
    {
        void SignIn(LoginUserModel model);
    }
    //Lấy User Login ra
    public class HttpUserProvider : IHttpUserProvider
    {
        private LoginUserModel _cachedModel;

        public LoginUserModel Provide()
        {
            if (_cachedModel == null)
            {
                _cachedModel = new LoginUserModel();

                var user = HttpContext.Current.GetOwinContext().Authentication.User;
                var identity = (ClaimsIdentity)user.Identity;
                IEnumerable<Claim> claims = identity.Claims;

                _cachedModel.Fullname = claims.FirstOrDefault(p => p.Type == ClaimTypes.Name)?.Value;
                _cachedModel.Email = claims.FirstOrDefault(p => p.Type == ClaimTypes.Email)?.Value;
                _cachedModel.UserId = int.Parse(claims.FirstOrDefault(p => p.Type == ClaimTypes.Sid)?.Value);

                var role = claims.FirstOrDefault(p => p.Type == ClaimTypes.Actor)?.Value;
                _cachedModel.Roles = string.IsNullOrWhiteSpace(role)
                    ? new List<string>()
                    : JsonConvert.DeserializeObject<List<string>>(role);
            }

            return _cachedModel;
        }

        public void SignIn(LoginUserModel model)
        {
            var ctx = HttpContext.Current.GetOwinContext().Authentication;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Fullname),
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.Actor,JsonConvert.SerializeObject( model.Roles)),
                new Claim(ClaimTypes.Sid, model.UserId.ToString())
            };

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie, ClaimTypes.Name, ClaimTypes.Role);

            ctx.SignIn(new AuthenticationProperties
            { IsPersistent = true }, identity);

            //var user = HttpContext.Current.GetOwinContext().Authentication;
            //var identity = (ClaimsIdentity)user.Identity;
            //IEnumerable<Claim> claims = identity.Claims;
        }
    }
}
