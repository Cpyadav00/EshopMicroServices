var builder = WebApplication.CreateBuilder(args);

// =====================================
// Registering Services into the DI Container
// =====================================

// Get the current assembly where the Program class resides
var assembly = typeof(Program).Assembly;

// Register MediatR services to use the CQRS pattern
builder.Services.AddMediatR(config =>
{
    // Automatically register all handlers (commands, queries, etc.) from the current assembly
    config.RegisterServicesFromAssembly(assembly);

    // Register custom MediatR behavior for validation (runs before handling a request)
    config.AddOpenBehavior(typeof(ValidationBehaviors<,>));

    // Register custom MediatR behavior for logging request and response info
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

// Register FluentValidation validators found in the current assembly
builder.Services.AddValidatorsFromAssembly(assembly);

// Register Carter framework for defining modular API endpoints in a clean and simple way
builder.Services.AddCarter();

// Register Marten (a PostgreSQL document database and event store for .NET)
// This configures Marten to use a connection string from the appsettings.json
builder.Services.AddMarten(config =>
{
    config.Connection(builder.Configuration.GetConnectionString("Database")!);
})
// Use lightweight sessions which are optimized for commands (write-heavy operations)
.UseLightweightSessions();

// If the environment is development, seed the Marten database with initial data
if (builder.Environment.IsDevelopment())
{
    // Register a custom seeding class that runs when the app starts
    builder.Services.InitializeMartenWith<CatalgInitalData>();
}

// Register a custom exception handler for centralized error handling
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

// Add health check services to the application for monitoring and diagnostics
// Here, it checks the connectivity of the PostgreSQL database
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

// =====================================
// Build the App and Configure the HTTP Pipeline
// =====================================

var app = builder.Build();

// Register all Carter-defined endpoints (modular route definitions)
app.MapCarter();

// Use the custom exception handler middleware globally
app.UseExceptionHandler(options => { });

// Setup a health check endpoint at "/health" that returns JSON UI-friendly results
app.UseHealthChecks("/health",
    new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
    {
        // Write a JSON response for health status, compatible with UI tools
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

// Run the application
app.Run();
