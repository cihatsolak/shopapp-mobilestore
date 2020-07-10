using ShopApp.Core.Domain.Categories;
using ShopApp.Core.Domain.Products;

namespace ShopApp.Core.Domain.Relationships
{
    /// <summary>
    /// Represents an productcategory relationship (N:N)
    /// </summary>
    public class ProductCategory
    {
        /// <summary>
        /// Gets or sets the category identifier
        /// </summary>
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        /// <summary>
        /// Gets or sets the product identifier
        /// </summary>
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
