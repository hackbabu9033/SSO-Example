using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataProtector _dataProtector;

        public HomeController(ILogger<HomeController> logger,
            IDataProtectionProvider dataProtectorProvider)
        {
            _logger = logger;
            _dataProtector = dataProtectorProvider.CreateProtector("temp");
        }

        public async Task<IActionResult> Index()
        {
            //await MockLoginAction();
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public async Task MockLoginAction()
        {
            var grandmaClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "hank"),
            };
            var grandmaIdentity = new ClaimsIdentity(grandmaClaims, "Identity.Cookie");

            var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });
            //-----------------------------------------------------------
            await HttpContext.SignInAsync(userPrincipal);
        }

    }
}
