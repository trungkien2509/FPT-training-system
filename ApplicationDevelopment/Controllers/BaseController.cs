using System.Web.Mvc;
using ApplicationDevelopment.Filters;

namespace ApplicationDevelopment.Controllers
{
    [Authorize]
    [MyErrorHandler]
    public class BaseController : Controller
    {
    }
}
