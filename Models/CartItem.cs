namespace MVCWebshop.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public ShopItem ShopItem { get; set; }
        public int Amount { get; set; }
    }
}
