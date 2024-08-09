using GraphQlDemo;

var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddAbstractions();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddGraphQl();
builder.Services.AddMapper();

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
if (app.Environment.IsDevelopment()) { }

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapGraphQL();
    });

app.Run();
