using AutoMapper;
using ShopApp.Core.Domain.Products;
using ShopApp.WebUI.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopApp.WebUI.Factories
{
    public partial class ProductModelFactory : IProductModelFactory
    {
        private readonly IMapper _mapper;
        public ProductModelFactory(IMapper mapper)
        {
            _mapper = mapper;
        }
        public virtual ProductDetailsModel PrepareProductDetailsModel(Product product = null)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            var productDetailsModel = new ProductDetailsModel();
            productDetailsModel.Product = product;
            productDetailsModel.Categories = product.ProductCategories.Select(x => x.Category).ToList();

            return productDetailsModel;
        }

        public virtual Product PrepareProductEntity(ProductModel productModel = null)
        {
            if (productModel == null)
                throw new ArgumentNullException(nameof(productModel));

            var product = _mapper.Map<ProductModel, Product>(productModel);

            return product;
        }

        public virtual ProductListModel PrepareProductListModel(List<Product> products = null)
        {
            if (products == null)
                throw new ArgumentNullException(nameof(products));

            var productListModel = new ProductListModel
            {
                Products = products,
                PageInfo = new PageInfo()
            };

            return productListModel;
        }

        public virtual ProductModel PrepareProductModel(Product product = null)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            var productModel = _mapper.Map<Product, ProductModel>(product);
            productModel.SelectedCategories = product.ProductCategories.Select(x => x.Category).ToList();

            return productModel;
        }
    }
}
