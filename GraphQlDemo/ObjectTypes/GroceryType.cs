using GraphQl.Database.Models;
using HotChocolate.Authorization;

namespace GraphQlDemo.ObjectTypes
{
    public class GroceryType : ObjectType<Grocery>
    {
        protected override void Configure(IObjectTypeDescriptor<Grocery> descriptor)
        {
            descriptor.Field(x => x.Name).Authorize("NamePolicy", ApplyPolicy.AfterResolver);
            descriptor.Field(x => x.CreatedOn).Authorize("CreationDatePolicy", ApplyPolicy.BeforeResolver);
        }
    }
}

