# GraphQL API with ASP.NET Core and HotChocolate

## Overview

This project demonstrates the implementation of a GraphQL API using ASP.NET Core and HotChocolate, with a SQL & Mongo database backend. The project employs a Code First approach to define the GraphQL schema and uses AutoMapper to map database entities to the GraphQL schema.

## Features

- **GraphQL HotChocolate**: Provides a powerful and flexible GraphQL server implementation.
- **ASP.NET Core**: Serves as the web framework for building the API.
- **SQL Database**: Stores data for groceries and products.
- **AutoMapper**: Maps database entities to GraphQL schema models.
- **Code First Approach**: Defines GraphQL schema based on C# classes.

## GraphQL Schema

### Mutations

1. **AddGrocery**
   - Adds a new grocery to the database.
   - Input: `GroceryInput` type.
   - Output: `GrocerySchema` type.

2. **AddProducts**
   - Adds a product to a specified grocery.
   - Input: `ProductInput` type.
   - Output: `ProductSchema` type.

3. **AddCustomer**
  - Adds a Customer .
  - Input: `CustomerInput` type.
  - Output: `CustomerSchema` type.

### Queries

1. **GetProducts**
   - Retrieves a list of products with pagination support.
   - Output: List of `ProductSchema` types.
   - Pagination: Uses `UseOffsetPaging` to handle page navigation with `pageInfo` that includes `hasNextPage` and `hasPreviousPage`.

2. **GetGroceries**
   - Retrieves a list of groceries with pagination support.
   - Output: List of `GrocerySchema` types.
   - Pagination: Uses `UseOffsetPaging` to handle page navigation with `pageInfo` that includes `hasNextPage` and `hasPreviousPage`.


3. **GetCustomers**
   - Retrieves a list of customers with pagination support.
   - Output: List of `CustomerSchema` types.
   - Pagination: Uses `UseOffsetPaging` to handle page navigation with `pageInfo` that includes `hasNextPage` and `hasPreviousPage`.

## Installation

1. **Clone the Repository**
   ```bash
   git clone <repository-url>
   cd <project-directory>
2. **Restore Dependencies**

      ```bash 
      dotnet restore
      ```
3. **Configuration**
Database Connection: Ensure your SQL database connection string is correctly configured in appsettings.json or appsettings.development.json.
 
   **Note** Open Terminal should be open GraphQlDemo folder not Project 

   1.  **Add Migration** "nameof Migration Replace with migration name" 
           ```bash
           dotnet ef migrations add PriceInProduct --context GraphQlDatabaseContext --output-dir Migrations --project Data/Database/GraphQl.Database.csproj```
   2. **Update Database**
        ```bash
        dotnet ef database update --startup-project GraphQlDemo```
4. **Run Application**
      ```bash  
      dotnet run
      ```
5. **Usage**
Open your browser and navigate to https://localhost:7104/graphql/ to interact with the GraphQL API. 
Use the GraphQL Playground or any GraphQL client to execute queries and mutations.

## Queries and Result Screenshots
You can find the example query and Screenshots in Queries_Result Folder

## Contribution
Feel free to fork the repository and submit pull requests with improvements or new features.

## License
This project is licensed under the MIT License. See the LICENSE file for details.

## Contact
For any questions or feedback, please contact email2wajidkhan@gmail.com, Phone Number: +971566290465.