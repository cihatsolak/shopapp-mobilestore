using ShopApp.Core.Domain.Relationships;
using System.Collections.Generic;

namespace ShopApp.Core.Domain.Products
{
    /// <summary>
    /// Represents an product
    /// </summary>
    public class Product : BaseEntity<int>
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the image url
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the price
        /// </summary>
        public decimal? Price { get; set; }

        public List<ProductCategory> ProductCategories { get; set; }
    }
}
