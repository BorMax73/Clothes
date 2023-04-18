using Microsoft.AspNetCore.Mvc;

namespace ClothesShop.Controllers
{
	public class ContactController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
