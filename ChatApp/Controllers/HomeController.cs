using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChatApp.Models;

namespace ChatApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private const string UserKey = "USER_KEY";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var username = HttpContext.Session.GetString(UserKey);
           
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("SignIn");
            }
            var vm = new IndexVm
            {
                Username = username,
            };
            return View(vm);
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(SignInVm vm)
        {
            if (!ModelState.IsValid) {
                return View(vm);
            }

            SignInUser(vm.Username);
            return RedirectToAction("Index");
            
        }
        private void SignInUser(string vmUsername)
        {
            HttpContext.Session.SetString(key: UserKey, value: vmUsername);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
