# ShopApp

The project was developed with four .NET core class libraries and one .net core web application. Core kütüphanesinde the kernel layer is the section where the classes belonging to the database are located. We keep the properties that can be used in common in all these classes in the Base Entity class. They used asp.net identity package for identity management at the data access layer. They used asp.net identity package for identity management at the data access layer.

## Kurulu Paketler (ShopApp.Core)
 Microsoft.EntityFrameworkCore.Design
 
### Kurulu Paketler (ShopApp.DataAccess)
 Microsoft.AsNetCore.Identity.EntityFramework
 Microsoft.EntityFrameworkCore
 Microsoft.EntityFrameworkCore.Design
 Microsoft.EntityFrameworkCore.SqlServer
 Microsoft.EntityFrameworkCore.Tools

## Kurulu Paketler (ShopApp.Service)
 Microsoft.AspNetCore.Mvc.ViewFeatures,
 SendGrid
 
 ## Kurulu Paketler (ShopApp.Web.Framework)
 Microsoft.AspNetCore.Mvc.ViewFeatures,
 SendGrid
