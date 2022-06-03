# CosyKangarooRIS
Assignment 3 for Software Architectures and Design Unit.


For this project we used .Net 6:
https://dotnet.microsoft.com/en-us/download/dotnet/6.0

# Architecture

We were aiming for 3 tier/layer architecture, a good article is here:
https://www.ibm.com/au-en/cloud/learn/three-tier-architecture

# Project Structure

```
.
├── README.md
└── src
    ├── CosyKangarooRIS.csproj
    ├── Program.cs
    ├── Presentation
    │   ├── Authentication.cs
    │   ├── InvoiceView.cs
    │   ├── MainMenu.cs
    │   ├── MenuItemViews.cs
    │   ├── OrderViews.cs
    │   ├── ReceiptView.cs
    │   ├── ReservationViews.cs
    │   └── View.cs
    ├── Application
    │   ├── Invoice.cs
    │   ├── MenuItem.cs
    │   ├── Order.cs
    │   ├── Payment.cs
    │   ├── PaymentMethod.cs
    │   ├── Person.cs
    │   ├── Receipt.cs
    │   ├── Reservation.cs
    │   └── Table.cs
    ├── Data
    │   ├── DatabaseInterface.cs
    │   ├── Schemas
    │   │   └── schema.sql
    │   └── mydatabase.db
    └── Utils
        └── Utils.cs

```

# Run the program with dotnet cli

```
cd src/
dotnet run
```
Or click the play button on vscode / visual studio etc. 

# Database
I am going to try using 'sqllite' because the other ones look too hard.

To run without an error you might need to add the sqlite package to your directory (not sure if it's being tracked on git, will check later)

### .net cli
```
dotnet add package System.Data.SQLite
```
### Visual Studio Package Manager
```
Install-Package System.Data.SQLite
```

If it still doesn't work maybe you need to install sqlite on your computer.
To check if you have sqlite installed type `sqlite3` into your terminal on linux/mac (not sure how to check on windows lol)
