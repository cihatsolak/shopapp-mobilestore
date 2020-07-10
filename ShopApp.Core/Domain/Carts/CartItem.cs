using ShopApp.Core.Domain.Products;

namespace ShopApp.Core.Domain.Carts
{
    public class CartItem : BaseEntity<int>
    {
        //Navigation Property
        public Product Product { get; set; }
        public int ProductId { get; set; }
        
        //Navigation Property
        public Cart Cart { get; set; }
        public int CartId { get; set; }
        
        public int Quantity { get; set; }
    }
}