using GraphQlDemo;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQlDemoOptions(builder.Configuration);
builder.Services.RegisterGraphQlDemoIServicesRegisterModules(builder.Configuration);
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddGraphQlDemoGraphQl();
builder.Services.AddGraphQlDemoMapper();

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
