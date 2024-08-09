using AutoMapper;
using GraphQl.Database.Models;
using GraphQlDemo.API.Models;

namespace GraphQlDemo;

public class GraphQlDemoProfile : Profile
{
    public GraphQlDemoProfile()
    {
        CreateMap<Product, ProductSchema>()
            .ForMember(m => m.ProductImageUrl, opt => opt.MapFrom(s => s.ImageUrl));
        CreateMap<ProductInput, Product>()
            .ForMember(m => m.Name, opt => opt.MapFrom(s => s.ProductName));
        CreateMap<Grocery, GrocerySchema>();
    }
}
