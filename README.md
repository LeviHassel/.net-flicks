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

???

## Requirements
* [Visual Studio Community](https://www.visualstudio.com/downloads/ "Visual Studio Community") (15.3+ is required for Core 2.0)
* [.NET Core 2.0 SDK](https://www.microsoft.com/net/download/core ".NET Core 2.0 SDK")
* [SQL Server Developer Edition](https://www.microsoft.com/en-us/sql-server/sql-server-downloads "SQL Server Developer Edition")
* [SQL Server Management Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms "SQL Server Management Studio")

## Getting Started
1. Download and install all of the requirements listed above.
2. Download this repository.
3. Open SQL Server Management Studio, enter "localhost" as the Server Name and click Connect.
4. 