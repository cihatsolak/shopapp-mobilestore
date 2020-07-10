using Microsoft.AspNetCore.Identity;

namespace ShopApp.DataAccess.Identity
{
    public class ApplicationUser : IdentityUser
    {
        //Identity user haricindeli özelliklerimiz
        public string FullName { get; set; }
    }
}
