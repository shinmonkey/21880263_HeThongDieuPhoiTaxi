using Microsoft.AspNetCore.Mvc;

namespace CallCenter.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult SignIn()
        {
            return View();
        }
    }
}
