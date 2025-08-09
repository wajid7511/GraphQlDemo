using GraphQlDemo;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterGraphQlDemoIServicesRegisterModules(builder.Configuration);
builder.Services.AddGraphQlDemoServices(builder.Configuration);

builder.Services.AddControllers();
var app = builder.Build();

app.UseMetricServer();
app.UseHttpMetrics();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) { }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGraphQL("/graphql");

app.Run();
