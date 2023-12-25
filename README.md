
## Setup

Check your connection string inside appsettings.json -->
*.\ShoppingListReact_ASP.NET_API\ShoppingList.Test\WebAPI\appsettings.json*

In my case it is: 
**"ShoppingListDbContext": 
"Server=LocalDb)\\MSSQLLocalDB;Database=shoppingListTest;Trusted_Connection=True;MultipleActiveResultSets=True;"**

Inside Visual Studio Package Manager Console run ***Update-Database*** command to create the database to the latest migration.

If that's not working please run the SQL scripts: 

 - **CreateDatabase.sql**
 - **ImportData.sql**

After you start the project it should be running on http://localhost:60528 .
<br/>

In the **react-app** directory, you can run:

### `npm install`
### `npm start`
