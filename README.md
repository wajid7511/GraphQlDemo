# GraphQL API with ASP.NET Core and HotChocolate

## Overview

This project demonstrates the implementation of a GraphQL API using ASP.NET Core and HotChocolate, with a SQL database backend. The project employs a Code First approach to define the GraphQL schema and uses AutoMapper to map database entities to the GraphQL schema.

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
   - Output: `Grocery` type.

2. **AddProductToGrocery**
   - Adds a product to a specified grocery.
   - Input: `ProductToGroceryInput` type.
   - Output: `Product` type.

### Queries

1. **GetProducts**
   - Retrieves a list of products with pagination support.
   - Output: List of `Product` types.
   - Pagination: Uses `UseOffsetPaging` to handle page navigation with `pageInfo` that includes `hasNextPage` and `hasPreviousPage`.

## Installation

1. **Clone the Repository**
   ```bash
   git clone <repository-url>
   cd <project-directory>
2. **Restore Dependencies**

      ```bash 
      dotnet restore
      Run the Application
      dotnet run
      ```
3. **Configuration**
Database Connection: Ensure your SQL database connection string is correctly configured in appsettings.json.
4. **Usage**
Open your browser and navigate to https://localhost:7104/graphql/ to interact with the GraphQL API. 
Use the GraphQL Playground or any GraphQL client to execute queries and mutations.

3. **Example Queries**
For example, to add a new grocery, use the AddGrocery mutation with the required input fields.
a) **Add Grocery**
    ```
        mutation AddGrocery {
          addGrocery(rquest: { name: "Madinah 1" }) {
            id
            name
            createdOn
            lastUpdateTime
          }
        }
    ```
    b) **Add Product to Grocery Mutation**
    ```
        mutation AddProduct {
          addProduct(
            rquest: {
              productMame: "Fresh Milk 1"
              productImageUrl: "https://unsplash.com/photos/164_6wVEHfINULL"
              groceryId: 5
            }
          ) {
            id
            name
            productImageUrl
            groceryId
            createdOn
            lastUpdateTime
            grocery {
              id
              name
              createdOn
              lastUpdateTime
            }
          }
        }
    ``` 
    c) **Get Products Query** 
    ```
    query Products {
      products(skip: 0, take: 10) {
        totalCount
        pageInfo {
          hasNextPage
          hasPreviousPage
        }
        items {
          id
          name
          productImageUrl
          groceryId
          createdOn
          lastUpdateTime
          grocery {
            id
            name
            createdOn
            lastUpdateTime
          }
        }
      }
    }
    ```

## Contribution
Feel free to fork the repository and submit pull requests with improvements or new features.

## License
This project is licensed under the MIT License. See the LICENSE file for details.

## Contact
For any questions or feedback, please contact email2wajidkhan@gmail.com, Phone Number: +971566290465.