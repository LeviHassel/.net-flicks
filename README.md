# ASP .NET Core 2.0 Multi-Project Starter Template

## Overview
This is a good template. It uses [IDesign](http://www.idesign.net/ "IDesign") methodology.

## Design
This solution is divided into four layers. 

### Clients
Dependencies: Managers

The Clients layer is the user interface. This is where all of the controllers, views, front-end styling and JavaScript lives. If the need arises to build a Web API, you simply need to build a new project in this layer and hook it up to the other layers of your project.

### Managers
Dependencies: Accessors

The Managers layer (usually called the Services layer in the world of C#), handles all of the business logic of the application.

### Accessors
Dependencies: None

The Accessors layer (usually called the Repository layer in world of C#), is in charge of storing and retrieving your application's data.

### Tests
Dependencies: Web, Managers and Accessors

The Tests layer uses [xUnit](https://xunit.github.io/ "xUnit") along with [Moq](https://github.com/moq/moq4 "Moq") to handle integration and unit tests.

## Requirements
* [Visual Studio Community](https://www.visualstudio.com/downloads/ "Visual Studio Community") (15.3+ is required for Core 2.0)
* [.NET Core 2.0 SDK](https://www.microsoft.com/net/download/core ".NET Core 2.0 SDK")
* [SQL Server 2016 Express LocalDB](https://www.microsoft.com/en-us/sql-server/sql-server-editions-express "SQL Server 2016 Express LocalDB") (find the LocalDB download option link on the page- it's not the main one; during the installation process, select "New SQL Server stand-alone installation or add features to an existing installation" and use the default setup)

## Suggestions
* [SQL Server Management Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms "SQL Server Management Studio") (Significantly more powerful and user-friendly alternative to Visual Studio's built-in SQL Server Object Explorer)
* [Papercut](https://github.com/ChangemakerStudios/Papercut "Papercut") (Fake SMTP server that you can use to catch outgoing emails in development)

## Getting Started
1. Download and install all of the requirements listed above.
2. Download this repository.
3. In Visual Studio, open Package Manager Console, switch the Default Project to CoreTemplate.Accessors and type the command `update-database` into the window. This should create the application's database, called CoreTemplateDb.

## Tips

### SQL Server Management Studio
If you'd like to use SQL Server Management Studio to manage your database instead of Visual Studio's built-in SQL Server Object Explorer, open SSMS, connect to a database engine, enter `(localdb)\MSSQLLocalDB` for the Server name, make sure Windows authentication is checked and click Connect. You can now freely use both SSMS and SQL Server Object Explorer to manage your database.

### Logging
Logging is configured and ready to go. In order to view a list of all recent logs and exceptions, add `/elm` to the URL. For example, you could visit `localhost:63319/elm`. This is made possible by the [ELM (Error Logging Middleware)](https://www.nuget.org/packages/Microsoft.AspNetCore.Diagnostics.Elm/ "ELM (Error Logging Middleware)") package, which you can read more about [here](http://www.talkingdotnet.com/aspnet-core-diagnostics-middleware-error-handling/#UseElmPage "app.UseElmPage() and app.UseElmCapture()"). In this template, the logs only exist as long as your IIS session is open. For more information on logging, including links to third-party logging providers that you could use to store your logs in a database or text file, check out Microsoft's [Introduction to Logging in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging?tabs=aspnetcore2x "Introduction to Logging in ASP.NET Core").
