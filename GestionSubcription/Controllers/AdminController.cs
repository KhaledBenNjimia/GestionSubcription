using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionSubcription.Controllers
{
    [Authorize(Roles = "Admin")] // Only admins can access this
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
