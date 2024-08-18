using NotificationManager;
using NotificationManager.Extensions;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.RegisterRabbitMqServices(builder.Configuration);
builder.Services.RegisterNotificationProcessors();
builder.Services.RegisterGraphQlDemoIServicesRegisterModules(builder.Configuration);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
