using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
//add services to the container



builder.Services
 .AddApplicationServices()
 .AddApplicationIfractureServices(builder.Configuration)
 .AddApiServices(builder.Configuration);


var app = builder.Build();

//Configure the http request pipline
app.UseApiServices();
if(app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.Run();
