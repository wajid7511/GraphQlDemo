using GraphQl.Database.Models;

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

