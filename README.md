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
* [SQL Server 2016 Express LocalDB](https://www.microsoft.com/en-us/sql-server/sql-server-editions-express "SQL Server 2016 Express LocalDB") (select "New SQL Server stand-alone installation or add features to an existing installation" in the installation process and use the default setup)
* [SQL Server Management Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms "SQL Server Management Studio") (Optional - download if you'd prefer not to use Visual Studio's built-in SLQ Server Object Explorer)

## Getting Started
1. Download and install all of the requirements listed above.
2. Download this repository.
3. In Visual Studio, open Package Manager Console, switch the Default Project to CoreTemplate.Accessors and type the command `update-database` into the window. This should create the database, called CoreTemplateDb.
4. If you'd like to use SQL Server Management Studio to manage your database instead of Visual Studio's built-in SLQ Server Object Explorer, open SSMS, connect to a database engine, enter `(localdb)\MSSQLLocalDB` for the Server name, make sure Windows authentication is checked and click Connect. You can now freely use either SSMS or SLQ Server Object Explorer to manage your database.
