# _Recipe Book_

#### By: _*Anna Pittman , Colt Thatcher*_

#### _A C# web application that utilizes a many to many relationship, as well as authentication and authorization functionality._

## Technologies Used

* _C#_
* _.NET 5_
* _ASP.NET Core MVC_
* _Razor_
* _Git_
* _MySql and MySQL Workbench_

## Description

A website where users can add recipes and tags to a database, as well as display which recipes belong to which tags. A many to many design is utilized to display the relationships between tags and recipes. Users can delete, edit, and see the details for every tag and recipe added. In addition, users must register an account before they can create any recipes or tags. 

## Setup/Installation Requirements

* Download [.NET5](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)
* Download MySQL and MySQL Workbench

## Application Setup

* Clone this repository to your desktop using the command: `git clone https://github.com/an12346/recipe-book.git`
* Navigate to the project directory: `cd Recipe.Solution`
* Then, navigate to the production file: `cd RecipeBook`
* Create a file named "appsettings.json" in the `RecipeBook` directory
* Add the following code to appsettings.json:
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;database=best_restaurants_refactor;uid=[YOUR DATABASE];pwd=[YOUR PASSWORD];"
  }
}
```
* Add the name of your database and password to the appsettings.json file
* To update the databse, run the command: `$ dotnet ef database update`
* To download obj and bin files, run the command `$ dotnet restore` 
* To open the application in a web browser, run the command: `$ dotnet run`

## Known Bugs

* _No known bugs_

## License

_MIT License: https://opensource.org/licenses/MIT_

Copyright (c) _2021_ _Anna Pittman_, _Colt Thatcher_