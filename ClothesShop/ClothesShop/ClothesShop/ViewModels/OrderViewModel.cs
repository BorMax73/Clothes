using System.ComponentModel.DataAnnotations;

namespace ClothesShop.ViewModels
{
	public class OrderViewModel
	{

		[Required(ErrorMessage = "Не вказано ім'я клієнта")]
		[StringLength(50, ErrorMessage = "Довжина імені не повинна перевищувати 50 символів")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Не вказано адресу")]
		[StringLength(100, ErrorMessage = "Довжина адреси не повинна перевищувати 100 символів")]
		public string Address { get; set; }

		[Required(ErrorMessage = "Не вказано електронну пошту")]
		[EmailAddress(ErrorMessage = "Некоректна електронна пошта")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Не вказано номер телефону")]
		[Phone(ErrorMessage = "Некоректний номер телефону")]
		public string Phone { get; set; }

	}
}
