using AutoMapper;
using GraphQl.Abstractions;
using GraphQl.Abstractions.Customers.Dtos;
using GraphQl.Database.Models;
using GraphQl.Mongo.Database.Models;
using GraphQlDemo.API.Models;

namespace GraphQlDemo;

public class GraphQlDemoProfile : Profile
{
    public GraphQlDemoProfile()
    {
        CreateMap<Product, ProductSchema>()
            .ForMember(m => m.ProductImageUrl, opt => opt.MapFrom(s => s.ImageUrl));
        CreateMap<ProductInput, Product>()
            .ForMember(m => m.Name, opt => opt.MapFrom(s => s.ProductName))
            .ForMember(m => m.Id, opt => opt.Ignore())
            .ForMember(m => m.CreatedOn, opt => opt.Ignore())
            .ForMember(m => m.LastUpdateTime, opt => opt.Ignore())
            .ForMember(m => m.Grocery, opt => opt.Ignore());
        CreateMap<Grocery, GrocerySchema>();
        CreateMap<Customer, CustomerDto>();
        CreateMap<Customer, CustomerSchema>();
        CreateMap<CustomerDto, CustomerSchema>();
        CreateMap<CustomerOrder, CustomerOrderDto>();
        CreateMap<CustomerOrderItem, CustomerOrderItemDto>();
        CreateMap<CustomerOrderDto, CustomerOrderSchema>();
        CreateMap<CustomerOrderItemDto, CustomerOrderItemSchema>();
        CreateMap<CustomerInput, Customer>()
            .ForMember(m => m.Id, opt => opt.Ignore())
            .ForMember(m => m.CreatedOn, opt => opt.Ignore())
            .ForMember(m => m.LastUpdateTime, opt => opt.Ignore());
    }
}
