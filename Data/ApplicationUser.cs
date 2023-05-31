using Microsoft.AspNetCore.Identity;
using MVCWebshop.Models;

namespace MVCWebshop.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string BankAccount { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public List<Order> Orders { get; set; }
    }
}