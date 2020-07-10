using System.Collections.Generic;
using System.Linq;

namespace ShopApp.WebUI.Models.Carts
{
    public class CartModel
    {
        public int CartId { get; set; }
        public List<CartItemModel> CartItemModels { get; set; }

        public decimal TotalPrice()
        {
            return CartItemModels.Sum(x => x.ProductPrice * x.Quantity);
        }
    }

    public class CartItemModel
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
    }
}
