using System;
using System.Security.Claims;
using Database.Models;
using HotChocolate.Authorization;

namespace GraphQlDemo.ObjectTypes
{
    public class ProductType : ObjectType<Product>
    {
        protected override void Configure(IObjectTypeDescriptor<Product> descriptor)
        {
            descriptor.Field(x => x.Name).Authorize();
        }
    }
}

