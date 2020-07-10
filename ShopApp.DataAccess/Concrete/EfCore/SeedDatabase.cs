using Microsoft.EntityFrameworkCore;
using ShopApp.Core.Domain.Categories;
using ShopApp.Core.Domain.Products;
using ShopApp.Core.Domain.Relationships;
using System.Linq;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    public static class SeedDatabase
    {
        // Database Test Data 
        public static void Seed()
        {
            var context = new ShopContext();

            //Eğer bekleyen migration sayısı 0 ise. migration'ı yansıt.
            if (context.Database.GetPendingMigrations().Count() == 0)
            {
                if (context.Categories.Count() == 0)
                {
                    context.Categories.AddRange(Categories);
                }

                if (context.Products.Count() == 0)
                {
                    context.Products.AddRange(Products);
                    context.AddRange(ProductCategory);
                }

                context.SaveChanges();
            }
        }

        private static Category[] Categories =
        {
            new Category(){Name="Telefon"},
            new Category(){Name="Bilgisayar"},
            new Category(){Name="Beyaz Eşya"},
            new Category(){Name="Ev Aletleri"},
            new Category(){Name="Elektronik"}
        };

        private static Product[] Products =
        {
            new Product(){Name="Samsung S5", Price=2000, ImageUrl="1.jpg", Description="<p>Güzel Telefon</p>"},
            new Product(){Name="Samsung S6", Price=3000, ImageUrl="2.jpg", Description="<p>Güzel Telefon</p>"},
            new Product(){Name="Samsung S7", Price=4000, ImageUrl="3.jpg", Description="<p>Güzel Telefon</p>"},
            new Product(){Name="Samsung S8", Price=5000, ImageUrl="4.jpg", Description="<p>Güzel Telefon</p>"},
            new Product(){Name="Samsung S9", Price=5000, ImageUrl="5.jpg", Description="<p>Güzel Telefon</p>"},
            new Product(){Name="Iphone 6", Price=5000, ImageUrl="6.jpg", Description="<p>Güzel Telefon</p>"},
            new Product(){Name="Iphone 7", Price=5000, ImageUrl="7.jpg", Description="<p>Güzel Telefon</p>"}
        };

        private static ProductCategory[] ProductCategory =
        {
            new ProductCategory(){ Product = Products[0], Category = Categories[0]},
            new ProductCategory(){ Product = Products[0], Category = Categories[2]},
            new ProductCategory(){ Product = Products[1], Category = Categories[0]},
            new ProductCategory(){ Product = Products[2], Category = Categories[1]},
            new ProductCategory(){ Product = Products[3], Category = Categories[2]},
            new ProductCategory(){ Product = Products[4], Category = Categories[3]},
            new ProductCategory(){ Product = Products[5], Category = Categories[4]},
            new ProductCategory(){ Product = Products[6], Category = Categories[4]}
        };
    }
}
