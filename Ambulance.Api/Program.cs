using Ambulance.Api.DbConnection;
using Ambulance.Api.Interfaces;
using Ambulance.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Swagger Services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Vehicle Management API",
        Version = "v1",
        Description = "API for managing vehicles and related data"
    });
});
builder.Services.AddDbContext<AmbulanceAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AmbulanceDBConn")));
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IVehicle, VehicleService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Vehicle API v1");
        options.RoutePrefix = string.Empty; // Swagger loads at root URL
    });
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
