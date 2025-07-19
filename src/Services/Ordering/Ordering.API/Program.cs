using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);
//add services to the container



builder.Services
 .AddApplicationServices()
 .AddApplicationIfractureServices(builder.Configuration)
 .AddApiServices();


var app = builder.Build();

//Configure the http request pipline
app.UseApiServices();
if(app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.Run();
