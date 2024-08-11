using AutoMapper;
using GraphQl.Abstractions;
using GraphQl.Mongo.Database.DALs;
using GraphQlDemo.API.Models;

namespace GraphQlDemo;

[ExtendObjectType<Query>]
public class CustomerQuery
{
    [UseOffsetPaging(DefaultPageSize = 10, IncludeTotalCount = true, MaxPageSize = 100)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<CustomerSchema> GetCustomers(
        [Service] CustomerDAL customerDal,
        [Service] IMapper mapper
    )
    {
        var customers = customerDal.GetCustomersAsync();

        return mapper.ProjectTo<CustomerSchema>(customers);
    }
}
