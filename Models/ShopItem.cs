using System.ComponentModel.DataAnnotations;

namespace MVCWebshop.Models
{
	public class ShopItem
	{
		public int Id { get; set; }
		public Plant Kind { get; set; }
		[Required(ErrorMessage = "This field is required.")]
		[MinLength(4, ErrorMessage = "The required value is minimum 4 characters.")]
		[Display(Prompt ="Kind of product.")]
		public string Name { get; set; }
		[Range(0, double.MaxValue, ErrorMessage = "The required value is between {0} and {1}.")]
		[Display(Prompt = "Price of product.")]
		public double Price { get; set; }
		[Required(ErrorMessage = "This field is required.")]
		[MinLength (4, ErrorMessage = "The required value is minimum 4 characters.")]
		[Display (Prompt = "Description of product.")]
		public string Description { get; set; }
	}
}
