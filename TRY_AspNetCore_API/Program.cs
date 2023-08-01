using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TRY_AspNetCore_API;
using TRY_AspNetCore_API.Data;
using TRY_AspNetCore_API.Logging;
using TRY_AspNetCore_API.Mappings;
using TRY_AspNetCore_API.Middlewares;
using TRY_AspNetCore_API.Repositories;

const string AllowedOrigins = "AllowedOrigins";

var builder = WebApplication.CreateBuilder(args);



/* Add logging. */
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/API_Log.txt", rollingInterval: RollingInterval.Day)
    .MinimumLevel.Information()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

/* Add services to the container. */

builder.Services.AddControllers();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowedOrigins,
        policy =>
        {
            policy.AllowCredentials().WithOrigins("http://localhost:4200");
        });
});

// Add API versioning
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;

    // Swagger related options
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnectionString"))
);
// Add repositories mapping
builder.Services.AddScoped<IPokemonRepository, SQLPokemonRepository>();

// Add Automapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

// Add custom logging formatter
builder.Services.AddSingleton<ILogging, Logging>();

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();


/* Build App. */

var app = builder.Build();

var versionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();



/* Configure the HTTP request pipeline. */

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
        {
            foreach (var description in versionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
            }
        }
    );
}

// Global exception handler for exception uncaught on controller-level
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseCors(AllowedOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
