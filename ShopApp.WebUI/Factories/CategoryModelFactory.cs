using AutoMapper;
using ShopApp.Core.Domain.Categories;
using ShopApp.Services.Abstract;
using ShopApp.WebUI.Models.Categories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopApp.WebUI.Factories
{
    public class CategoryModelFactory : ICategoryModelFactory
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryModelFactory(ICategoryService categoryService, IMapper mapper)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }

        public virtual Category PrepareCategoryEntity(CategoryModel categoryModel = null)
        {
            if (categoryModel == null)
                throw new ArgumentNullException(nameof(categoryModel));

            var category = _mapper.Map<CategoryModel, Category>(categoryModel);

            return category;
        }

        public virtual CategoryListModel PrepareCategoryListModel(List<Category> categories = null)
        {
            if (categories == null)
                throw new ArgumentNullException(nameof(categories));

            var categoryListModel = new CategoryListModel
            {
                Categories = categories
            };

            return categoryListModel;
        }

        public virtual CategoryListViewModel PrepareCategoryListViewModel()
        {
            var categoryListViewModel = new CategoryListViewModel();
            categoryListViewModel.Categories = _categoryService.GetAllCategories();

            return categoryListViewModel;
        }

        public CategoryModel PrepareCategoryModel(Category category = null)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            var categoryModel = _mapper.Map<Category, CategoryModel>(category);
            categoryModel.Products = category.ProductCategories.Select(x => x.Product).ToList();

            return categoryModel;
        }
    }
}
