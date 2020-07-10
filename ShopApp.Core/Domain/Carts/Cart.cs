using System.Collections.Generic;

namespace ShopApp.Core.Domain.Carts
{
    public class Cart : BaseEntity<int>
    {
        public string UserId { get; set; }

        //Navigation Property
        public List<CartItem> CartItems { get; set; }
    }
}
