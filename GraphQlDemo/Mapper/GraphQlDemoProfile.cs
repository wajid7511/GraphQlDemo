using AutoMapper;
using Database.Models;
using GraphQlDemo.API.Models;

namespace GraphQlDemo;

public class GraphQlDemoProfile : Profile
{
    public GraphQlDemoProfile()
    {
        CreateMap<Product, ProductSchema>();
        CreateMap<Grocery, GrocerySchema>();
    }
}
