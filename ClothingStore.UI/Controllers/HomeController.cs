using Microsoft.AspNetCore.Mvc;
using ClothingStore.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;

namespace ClothingStore.UI.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

	}
}
