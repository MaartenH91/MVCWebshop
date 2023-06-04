using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCWebshop.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public List<CartItem> CartItems { get; set; }
        [DataType(DataType.Date)]
        public DateTime ValidUntil { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(6,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public decimal TotalCost { get; set; }
        public ShoppingCart()
        {
            CartItems = new List<CartItem>();
            TotalCost = 0;
        }
        public ShoppingCart(List<CartItem> cartItems, DateTime validUntil)
        {
            this.CartItems = cartItems;
            this.ValidUntil = validUntil;
        }
        public void AddItem(ShopItem shopItem, int amount)
        {
            var i = CartItems.FirstOrDefault(i => i.ShopItem.Id == shopItem.Id);
            if (i == null)
            {
                CartItems.Add(new CartItem()
                {
                    ShopItem = shopItem,
                    Amount = amount
                });
            }
            else
            {
                i.Amount += amount;
            }
            // shopping cart only lasts for two weeks.
            ValidUntil = DateTime.Now.AddDays(14);
        }
        public void RemoveItem(ShopItem shopItem)
        {
            var i = CartItems.FirstOrDefault(i => i.ShopItem.Id == shopItem.Id);
            if (i != null)
            {
                CartItems.Remove(i);
            }
            ValidUntil = DateTime.Now.AddDays(14);
        }
        public void Clear()
        {
            CartItems = new List<CartItem>();
        }
    }
}
