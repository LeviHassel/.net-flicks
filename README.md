# .NetFlicks (.NET Core 2.1 Sample App)

## Note: This is a work in progress and is not ready to be used in its current state

## Overview
What started as a simple template project for .NET Core evolved into a decent-sized sample application showcasing what I believe to be the best .NET practices. .NetFlicks allows users to rent/purchase digital movies and view details such as cast and crew. Additionally, it contains admin pages to create/edit/delete movies, people and more. It utilizes multitier system architecture and has an intuitive, mobile-friendly user interface. This project has been a great learning experience for me and I hope it can help other developers looking for a solid template to build on.

## Demo
[![.NetFlicks Demo](https://img.youtube.com/vi/ScMzIvxBSi4/0.jpg)](https://www.youtube.com/watch?v=ScMzIvxBSi4)

## Stack
 * Server
   * [ASP.NET Core 2.1](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-2.0 "ASP.NET Core 2.1")
   * [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/ "Entity Framework Core")
   * [AutoMapper](https://automapper.org/ "AutoMapper")
 * Client
   * [Bootstrap 4.1](https://getbootstrap.com/ "Bootstrap 4.1")
   * [Bootstrap Select](https://silviomoreto.github.io/bootstrap-select/ "Bootstrap Select")
   * [Font Awesome](https://fontawesome.com/ "Font Awesome")
 * Testing
   * [xUnit](https://xunit.github.io/ "xUnit")
   * [Moq](https://github.com/moq/moq4 "Moq")
   * [AutoFixture](https://github.com/AutoFixture/AutoFixture "AutoFixture")
   * [Fluent Assertions](https://fluentassertions.com/ "Fluent Assertions")

## Database Design
The database for this solution uses Entity Framework and ASP.NET Identity. I chose not to use Identity Roles, instead allowing all users to see both the Client and Administrator UI at once for simplicity. The database, called DotNetFlicksDb, is automatically seeded with data for 40 movies, provided by [TMDB](https://www.themoviedb.org "TMDB").
![.NetFlicks Database](https://user-images.githubusercontent.com/9669653/41392853-b398faba-6f68-11e8-996e-21f882e9df8c.png)

## System Architecture
This solution is divided into four layers based on [IDesign](http://www.idesign.net/ "IDesign") methodology:

| Layer | Description | Able to Call | Model |
| --- | --- | --- | --- |
| Clients | An entry-point to the system, such as an MVC site or REST API | Managers | ViewModel |
| Managers | Manages the workflow of a call chain, handles business logic | Engines, Accessors | DTO |
| Engines | Encapsulates algorithms and business logic (optional layer) | Accessors | DTO |
| Accessors | Accesses data from resources like databases and APIs | None | Entity |

IDesign is a closed architecture that focuses on encapsulating volatility, minimizing coupling and separation of concerns. The official IDesign documentation is sparse, so I'd recommend reading [Software architecture and project design, a mechanized approach](http://codewithspoon.com/2017/07/software-architecture/ "Software architecture and project design, a mechanized approach") for a quick intro to the method.

Here's my implementation of IDesign for this solution:
![.NetFlicks Architecture](https://user-images.githubusercontent.com/9669653/41392851-b37d69a8-6f68-11e8-8e45-6f5b8ab7fcba.png)

## Setup
### Getting Started
1. Install the following:
   * [.NET Core 2.1 SDK](https://www.microsoft.com/net/download/windows ".NET Core 2.1 SDK")
   * [Visual Studio Community 2017](https://www.visualstudio.com/downloads/ "Visual Studio Community 2017") (15.7+ is required for Core 2.1, select `ASP.NET and web development` workload during installation)
2. Download this repository
3. Open the solution in Visual Studio and run the Web project (this may take some time, as it will create and seed the database)
4. Log into the default administrator account (email: `admin@dotnetflicks.com`, password: `p@ssWORD471`) or create your own account to start using the site

### Tips
* **Access/manage database**
  * Inside Visual Studio, open the SQL Server Object Explorer window by going to View->SQL Server Object Explorer
  * Expand `(localdb)\MSSQLLocalDB` and the `Databases` folder to find `DotNetFlicksDb` (exists only after you first run the project)
* **Catch emails in development**
  * Install [Papercut](https://github.com/ChangemakerStudios/Papercut "Papercut"), a fake SMTP server that you can use to catch outgoing emails in development
* **View logs and exceptions**
  * In order to view a list of all logs and exceptions from your current IIS session, add `/elm` to the base URL
  * This is made possible by the [ELM (Error Logging Middleware)](https://www.nuget.org/packages/Microsoft.AspNetCore.Diagnostics.Elm/ "ELM (Error Logging Middleware)") package, which you can read more about [here](http://www.talkingdotnet.com/aspnet-core-diagnostics-middleware-error-handling/#UseElmPage "app.UseElmPage() and app.UseElmCapture()")
  * For more information on logging, check out Microsoft's [Introduction to Logging in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging?tabs=aspnetcore2x "Introduction to Logging in ASP.NET Core")
  
### Troubleshooting
* **Can't find a LocalDB instance in SQL Server Object Explorer**
  * Open Visual Studio Installer
  * Modify your Visual Studio Installation
  * Go to the `Individual Components` tab
  * Scroll down to the `Cloud, database and server` section
  * Ensure that `SQL Server 2016 Express LocalDB` is checked and click `Modify`
* **Admin Settings pages are slow or unresponsive**
  * LocalDB requires a certain amount of free memory to efficiently run large database queries
  * Try closing processes until your memory usage drops and then reload the page
