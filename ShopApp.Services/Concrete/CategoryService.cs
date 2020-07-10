using ShopApp.Core.Domain.Categories;
using ShopApp.DataAccess.Abstract.Implements;
using ShopApp.Services.Abstract;
using System;
using System.Collections.Generic;

namespace ShopApp.Services.Concrete
{
    public partial class CategoryService : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryService(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public List<Category> GetAllCategories()
        {
            return _categoryDal.GetAll();
        }

        public Category GetById(int id)
        {
            if (id == 0)
                throw new ArgumentNullException(nameof(id));

            return _categoryDal.Get(id);
        }

        public void Create(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            _categoryDal.Insert(category);
        }
        public void Update(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            _categoryDal.Update(category);
        }

        public void Delete(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            _categoryDal.Delete(category);
        }

        public Category GetByIdWithProducts(int id)
        {
            if (id == 0)
                throw new ArgumentNullException(nameof(id));

            return _categoryDal.GetByIdWithProducts(id);
        }

        public void DeleteFromCategory(int categoryId, int productId)
        {
            if (categoryId == 0)
                throw new ArgumentNullException(nameof(categoryId));

            if (productId == 0)
                throw new ArgumentNullException(nameof(productId));

            _categoryDal.DeleteFromCategory(categoryId, productId);
        }
    }
}
