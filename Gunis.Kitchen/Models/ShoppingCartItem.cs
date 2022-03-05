namespace Gunis.Kitchen.Models
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemID { get; set; }

        public Item Item { get; set; }

        public int Amount { get; set; }

        public string ShoppingCartId { get; set; }
    }
}
