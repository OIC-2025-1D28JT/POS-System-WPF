namespace POSS.Models
{
    public class CartItem
    {
        public int No { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int Subtotal => Price * Quantity;
    }
}