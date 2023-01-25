This project has been created to connect GraphQl with Database Context directly. 

Queries Example

{
  products(
    take: 100
    skip: 1
  ) {
    items {
      id
      name
      imageUrl
      createdOn
      groceryId 
    }
  }
}

{
  products(
    take: 100
    skip: 1
    order: { name: DESC } 
  ) {
    items {
      id
      name
      imageUrl
      createdOn
      groceryId 
    }
  }
}
{
  products(
    take: 100
    skip: 1
    order: { name: DESC }
    where: { name: { contains: "s" } }
  ) {
    items {
      id
      name
      imageUrl
      createdOn
      groceryId 
    }
  }
}

{
  products(
    take: 100
    skip: 1
    order: { name: DESC }
    where: { name: { contains: "s" } }
  ) {
    items {
      id
      name
      imageUrl
      createdOn
      groceryId
      grocery {
        id
        name
        createdOn
      }
    }
  }
}
