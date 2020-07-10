using ShopApp.Core.Domain.Relationships;
using System.Collections.Generic;

namespace ShopApp.Core.Domain.Categories
{
    /// <summary>
    /// Represents an category
    /// </summary>
    public class Category : BaseEntity<int>
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        public List<ProductCategory> ProductCategories { get; set; }
    }
}
