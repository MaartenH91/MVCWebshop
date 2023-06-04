using System.ComponentModel.DataAnnotations;

namespace MVCWebshop.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Date)]
        public DateTime StartOrder { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [MinLength(4, ErrorMessage = "Minimum length is 4 characters.")]
        [Display(Name = "Address of order.")]
        public string OrderAddress { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [MinLength(4, ErrorMessage = "Minimum length is 4 characters.")]
        public string Remark { get; set; }
        public bool Payed { get; set; } = false;
        public DateTime OrderPayed { get; set; }
    }
}
