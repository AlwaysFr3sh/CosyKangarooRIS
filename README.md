# CosyKangarooRIS
Assignment 3 for Software Architectures and Design Unit.

Based on discussion in discord, I have taken the liberty of setting up a Dotnet 6 cli project in c#

It's still early days so if I have jumped the gun and the group would rather use something else we can always make a new repo or just switch what we're using in this one.

I installed dotnet 6 on my computer from here:
https://dotnet.microsoft.com/en-us/download/dotnet/6.0

I set up the project using microsoft's tutorial found here:
https://docs.microsoft.com/en-us/dotnet/core/tutorials/with-visual-studio-code?pivots=dotnet-6-0
# Architecture

To start with I was aiming for this:

https://www.codeauthority.com/Blog/Entry/three-tier-architecture

As our Project's architecture, although 'MVC' is also a strong option:

https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93controller

still early days so we can quite easily refactor.

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
