namespace basket.api.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public List<ShopptingCartItems> Items = new List<ShopptingCartItems>();
        public ShoppingCart() { }
        public ShoppingCart(string name) { UserName = name; }

        public decimal TotalPrice
        {
            get 
            {
                decimal totalPrice = 0;
                foreach(var item in Items)
                {
                    totalPrice += item.Price * item.Quantity;
                }
                return totalPrice;
            }
        }
    }
}
