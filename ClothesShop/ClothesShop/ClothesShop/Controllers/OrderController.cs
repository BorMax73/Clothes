using ClothesShop.Data;
using ClothesShop.Models;
using ClothesShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ClothesShop.Controllers
{
	public class OrderController : Controller
	{
		private readonly ApplicationContext _context;

		public OrderController(ApplicationContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{

			var cart = new Cart();
			if (HttpContext.Session.GetString("Cart") != null)
			{
				var jsonCart = HttpContext.Session.GetString("Cart");
				cart = JsonConvert.DeserializeObject<Cart>(jsonCart);
			}
			ViewData["Cart"] = cart;

			var items = _context.Products.Include(x=>x.Images).Where(p => cart.Items.Select(i => i.Product.Id).Contains(p.Id)).ToList();
			ViewData["Qnt"] = cart.Items.Select(x=>x.Quantity).ToList();
			ViewBag.Cart = cart;
			return View(items);
		}

		//public IActionResult Checkout()
		//{
		//	return View();
		//}
		//[HttpPost]
		public IActionResult Checkout(OrderViewModel order)
		{
			var cartString = HttpContext.Session.GetString("Cart");
			var cart = new Cart();
			if (cartString != null)
			{
				cart = JsonConvert.DeserializeObject<Cart>(cartString);

			}
			else
			{
				ModelState.AddModelError("", "Sorry, your cart is empty!");
			}

			if (ModelState.IsValid)
			{
				var newOrder = new Order()
				{
					Name = order.Name,
					Address = order.Address,
					Email = order.Email,
					Phone = order.Phone,
					
					Date = DateTime.Now,
					Total = cart.GetTotal(),

				};
				_context.Orders.Add(newOrder);
				_context.SaveChanges();

				foreach (var item in cart.Items)
				{
					var orderItem = new OrderItem()
					{
						OrderId = newOrder.Id,
						ProductId = item.Product.Id,
						Quantity = item.Quantity,
						Price = item.Product.Price,
						Size = item.Size,
						Color = item.Color


					};
					_context.OrderItems.Add(orderItem);
				}
				_context.SaveChanges();
				HttpContext.Session.Remove("Cart");
				return RedirectToAction("Index", "Home");
			}
			return View(order);



		}
	}
}
