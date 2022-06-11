using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace domain1.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(): base()
        {

        }

        public IActionResult Index()
        {
            return View("Index");
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }
       
        //public async Task<IActionResult> LogOut()
        //{
        //    // todo : clear cookie then redirect url to sso login page
        //    var authenticateCookie = "Identity.Cookie";
        //    Response.Cookies.Delete(authenticateCookie);
        //    return RedirectToAction("Index");
        //}
    }
}
