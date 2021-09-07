using System.Web.Mvc;
using FPT_Training_System.Web.Filters;

namespace FPT_Training_System.Web.Controllers
{
    [Authorize]
    [MyErrorHandler]
    public class BaseController : Controller
    {
    }
}
