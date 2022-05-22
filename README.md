# CosyKangarooRIS
Assignment 3 for Software Architectures and Design Unit.

Based on discussion in discord, I have taken the liberty of setting up a Dotnet 6 cli project in c#

It's still early days so if I have jumped the gun and the group would rather use something else we can always make a new repo or just switch what we're using in this one.

I installed dotnet 6 on my computer from here:
https://dotnet.microsoft.com/en-us/download/dotnet/6.0

I set up the project using microsoft's tutorial found here:
https://docs.microsoft.com/en-us/dotnet/core/tutorials/with-visual-studio-code?pivots=dotnet-6-0

# Run the program with dotnet cli

```
cd src/
dotnet run
```
Or click the play button on vscode / visual studio etc. 

# Database
I am going to try using 'litedb' because the other ones look too hard

# Architecture

I started implementing classes based on what we specified in our Assignment 2,

however I am starting to think it would make more sense to split our classes up to separate business logic from 
our data objects

what we specified (to me) looks like this:

`ui -> classes -> database`

However I think we should implement it like this:

`ui logic -> Business Logic -> Data classes -> Database`

this specific architecture is called "Three tiered architecture design" according to this website:

https://www.codeauthority.com/Blog/Entry/three-tier-architecture#:~:text=A%20three%2Dtiered%20architectural%20design,and%20maintained%20as%20independent%20modules.

To start with I think we should start with the database and work up to the ui
depending on how much time we have, we can make a simple cli interface, or we could port to .net asp website

let me know what you guys think.
