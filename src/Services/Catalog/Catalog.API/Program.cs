var builder = WebApplication.CreateBuilder(args);

//Add services to the container 

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    //Regestring all the services in assembly 
    config.RegisterServicesFromAssembly(assembly);
    //Add the custom behaviour in the MediatR pipline 
    config.AddOpenBehavior(typeof(ValidationBehaviors<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
//Add the custom validations in the assembly 
builder.Services.AddValidatorsFromAssembly(assembly);
//Add the carter for
builder.Services.AddCarter();
//Add the martin for database configuration  
builder.Services.AddMarten(config =>
{
    config.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();
//checking if the enviroment is developmnet 
if(builder.Environment.IsDevelopment())
{
    //for seeding the data base when the application start 
    builder.Services.InitializeMartenWith<CatalgInitalData>();
}
//Add the custom validations
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

var app = builder.Build();

//Configure the HTTP Request pipline 

app.MapCarter();

app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health",
    new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });



app.Run();
