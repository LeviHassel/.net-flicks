# .NetFlicks (.NET Core 2.1 Sample App)
---
## Note: This is a work in progress and is not ready to be used in its current state

## Overview
What started as a simple template project for .NET Core evolved into a decent-sized sample application showcasing what I believe to be the best .NET practices. .NetFlicks allows users to rent/purchase digital movies and view details such as cast and crew. Additionally, it contains admin pages to create/edit/delete movies, people and more. It utilizes multitier system architecture and has an intuitive, mobile-friendly user interface. This project has been a great learning experience for me and now I hope it can help other developers looking for a solid template to build on.

## Demo
[![.NetFlicks Demo](https://img.youtube.com/vi/ScMzIvxBSi4/0.jpg)](https://www.youtube.com/watch?v=ScMzIvxBSi4)

## System Architecture
This solution is divided into four layers based on [IDesign](http://www.idesign.net/ "IDesign") methodology. IDesign is a closed architecture system where each layer can only call down.

[Explain breifly How to use iDesign correctly so they don't just create a Controller/Manager/Accessor for every class]
A very important concept is to design around volatility. Try to give every service only one reason to change.

| Layer | Description | Dependencies |
| - | - | - |
| Clients | The user interface, where all of the controllers, views, styling and scripts live | Managers |
| Managers | Manages a sequence of actions, handles business logic (often called the Services layer in the world of C#) | Engines, Accessors |
| Engines | Applies an algorithm/business rule, useful for encapsulating things | Accessors |
| Accessors | Stores and retrieves data from resources like databases and APIs (often called the Repository layer in world of C#) | None |

![.NetFlicks Architecture](https://user-images.githubusercontent.com/9669653/40292370-8e94ff6a-5c90-11e8-8751-08ce14575cea.png)

## Database Design
[Talk briefly about the database]
![.NetFlicks Database](https://user-images.githubusercontent.com/9669653/40290536-25721b6e-5c84-11e8-927e-0656b7452ff2.png)

## Stack
 * Server
   * ASP .NET Core 2.1
   * Entity Framework Core
   * AutoMapper
 * Client
   * Bootstrap 4.1
   * Bootstrap Select
   * DataTables
   * Font Awesome
 * Testing
   * [xUnit](https://xunit.github.io/ "xUnit")
   * [Moq](https://github.com/moq/moq4 "Moq")
   * AutoFixture
   * FluentAssertions
* Other
   * TMDB (provided me with seed data)


## Setup
### Getting Started
1. Install the following:
   * [Visual Studio Community](https://www.visualstudio.com/downloads/ "Visual Studio Community") (15.3+ is required for Core 2.0)
   * [.NET Core 2.0 SDK](https://www.microsoft.com/net/download/core ".NET Core 2.0 SDK")
   * [SQL Server 2016 Express LocalDB](https://www.microsoft.com/en-us/sql-server/sql-server-editions-express "SQL Server 2016 Express LocalDB") (Find the LocalDB download option link on the page- it's not the main one; during the installation process, select "New SQL Server stand-alone installation or add features to an existing installation" and use the default setup)
2. Download this repository.
3. Open the solution in Visual Studio and run the Web project. This will create and seed the application's database, called DotNetFlicksDb.

### Tips
* **Connect to database using SQL Server Management Studio**
  * Install [SQL Server Management Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms "SQL Server Management Studio"), a significantly more powerful and user-friendly alternative to Visual Studio's built-in SQL Server Object Explorer
  * When you open it, it should show the "Connect to Server" window; if not, click Connect->Database Engine in the sidebar
  * Select `Database Engine` for Server type, enter `(localdb)\MSSQLLocalDB` for Server name, select `Windows authentication` for Authentication and click Connect
  * You should now see `DotNetFlicksDb` in the Databases folder of your localdb
* **Catch emails in develpoment**
  * Install [Papercut](https://github.com/ChangemakerStudios/Papercut "Papercut"), a fake SMTP server that you can use to catch outgoing emails in development
* **View logs and exceptions**
  * In order to view a list of all logs and exceptions from your current IIS session, add `/elm` to the base URL
  * This is made possible by the [ELM (Error Logging Middleware)](https://www.nuget.org/packages/Microsoft.AspNetCore.Diagnostics.Elm/ "ELM (Error Logging Middleware)") package, which you can read more about [here](http://www.talkingdotnet.com/aspnet-core-diagnostics-middleware-error-handling/#UseElmPage "app.UseElmPage() and app.UseElmCapture()")
  * For more information on logging, check out Microsoft's [Introduction to Logging in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging?tabs=aspnetcore2x "Introduction to Logging in ASP.NET Core")
