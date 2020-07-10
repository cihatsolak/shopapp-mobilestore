using ShopApp.Core.Domain.Products;
using ShopApp.DataAccess.Abstract.Implements;
using ShopApp.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopApp.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductService(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public List<Product> GetAllProducts()
        {
            return _productDal.GetAll();
        }

        public Product GetById(int id)
        {
            if (id == 0)
                throw new ArgumentNullException(nameof(id));

            return _productDal.Get(id);
        }

        public void Create(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            _productDal.Insert(product);
        }

        public void Update(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            _productDal.Update(product);
        }

        public void Delete(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            _productDal.Delete(product);
        }

        public List<Product> GetProductsByCategoryName(string categoryName, int page, int pageSize)
        {
            if (categoryName == null || string.IsNullOrWhiteSpace(categoryName))
                categoryName = string.Empty;

            return _productDal.GetProductsByCategory(categoryName, page, pageSize);
        }

        public Product GetProductDetails(int id = 0)
        {
            if (id == 0)
                throw new ArgumentNullException(nameof(id));

            return _productDal.GetProductDetails(id);
        }

        public int GetCountByCategoryName(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName) || string.IsNullOrWhiteSpace(categoryName))
                categoryName = string.Empty;

            return _productDal.GetCountByCategoryName(categoryName);
        }

        public Product GetByIdWithCategories(int id)
        {
            if (id == 0)
                throw new ArgumentNullException(nameof(id));

            return _productDal.GetByIdWithCategories(id);
        }

        public void UpdateWithCategory(Product product, int[] categoryIds)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            if (categoryIds == null)
                throw new ArgumentNullException(nameof(categoryIds));

            _productDal.UpdateWithCategory(product, categoryIds);
        }
    }
}
