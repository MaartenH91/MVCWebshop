using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCWebshop.Models
{
	public class ShopItem
	{
		public int Id { get; set; }
		public Plant Kind { get; set; }

		[Required(ErrorMessage = "This field is required.")]
		[MinLength(4, ErrorMessage = "The required value is minimum {1} characters.")]
		[Display(Prompt ="Name of product.")]
		public string Name { get; set; }

		[Required]
		[Display(Prompt = "Price of product.")]
		[DataType(DataType.Currency)]
		[Column(TypeName ="decimal(6,2)")]
		[DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

		[Required(ErrorMessage = "This field is required.")]
		[MinLength (4, ErrorMessage = "The required value is minimum {1} characters.")]
		[Display (Prompt = "Description of product.")]
		public string Description { get; set; }

		[Required]
		[Display(Prompt = "Amount in store")]
		public int Amount { get; set; }
	}
}
