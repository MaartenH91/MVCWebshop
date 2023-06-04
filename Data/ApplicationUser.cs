using Microsoft.AspNetCore.Identity;
using MVCWebshop.Models;

namespace MVCWebshop.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public char BoxNumber { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string BankAccount { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public List<Order> Orders { get; set; }
    }
}