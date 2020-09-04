# Mobile Phone Shop Application

The project was developed with four .NET core class libraries and one .net core web application. Core kütüphanesinde the kernel layer is the section where the classes belonging to the database are located. We keep the properties that can be used in common in all these classes in the Base Entity class. The asp.net identity package is used for identity management in the data access layer.

![Home](https://user-images.githubusercontent.com/54249736/92264867-93a2d680-eee7-11ea-9d56-4feddd46e2a0.png)

#### Kurulu Paketler (ShopApp.Core)
 Microsoft.EntityFrameworkCore.Design
 
#### Kurulu Paketler (ShopApp.DataAccess)
 Microsoft.AsNetCore.Identity.EntityFramework
 Microsoft.EntityFrameworkCore
 Microsoft.EntityFrameworkCore.Design
 Microsoft.EntityFrameworkCore.SqlServer
 Microsoft.EntityFrameworkCore.Tools

#### Kurulu Paketler (ShopApp.Service)
 Microsoft.AspNetCore.Mvc.ViewFeatures,
 SendGrid
 
#### Kurulu Paketler (ShopApp.Web.Framework)
 Microsoft.AspNetCore.Mvc.ViewFeatures,
 SendGrid
