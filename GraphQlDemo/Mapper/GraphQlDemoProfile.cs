using AutoMapper;
using Database.Models;
using GraphQlDemo.API.Models;

namespace GraphQlDemo;

public class GraphQlDemoProfile : Profile
{
    public GraphQlDemoProfile()
    {
        CreateMap<Product, ProductSchema>()
            .ForMember(m => m.ProductImageUrl, opt => opt.MapFrom(s => s.ImageUrl));
        CreateMap<Grocery, GrocerySchema>();
    }
}
