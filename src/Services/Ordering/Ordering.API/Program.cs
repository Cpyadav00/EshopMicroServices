using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
//add services to the container

//----------------
//Infracture -EF core
//Application -meditar
//Api-carter healthchecks

builder.Services
 .AddApplicationServices()
 .AddApplicationServices(builder.Configuration)
 .AddApiServices();


var app = builder.Build();

//Configure the http request pipline

app.Run();
