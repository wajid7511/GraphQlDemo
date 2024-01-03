using Microsoft.EntityFrameworkCore;
using Database;
using GraphQlDemo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<GraphQlDatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddProjections()
    .AddFiltering()
    .AddSorting();
    // .AddType<ProductType>()
    // .AddType<GroceryType>()
    // .AddAuthorization() ;

builder.Services.AddControllers();
// builder.Services.AddAuthorization(c =>
// {
//     c.AddPolicy("NamePolicy", builder =>
//     {
//         builder.RequireAssertion(async context =>
//         {
//             if (context.Resource is IMiddlewareContext ctx)
//             {

//                 return true;
//             }
//             return false;
//         });
//     });
//     c.AddPolicy("CreationDatePolicy", builder =>
//     {
//         builder.RequireAssertion(async context =>
//         {
//             if (context.Resource is IMiddlewareContext ctx)
//             {
//                 return true;
//             }
//             return false;
//         });
//     });
// });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting()
.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});

app.Run();

