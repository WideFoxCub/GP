using GP.Data;
using GP.Services;
using GP.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// DbContext - konfiguracja połączenia z PostgreSQL
// Connection string będzie w appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<GpDbContext>(options =>
    options.UseNpgsql(connectionString)
);

// CORS Configuration - pozwala Frontendowi (Angular) na dostęp do API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()        // Każda domena (Frontend)
              .AllowAnyMethod()        // GET, POST, PUT, DELETE
              .AllowAnyHeader();       // Authorization, Content-Type
    });
});

// Dependency Injection - rejestrujemy serwisy
// Interface -> Implementacja (nauczę Cię co to jest)
builder.Services.AddScoped<IServiceService, ServiceService>();

// FluentValidation - rejestrujemy validators
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

// Swagger/OpenAPI - dokumentacja API
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    // Swagger UI - dokumentacja interaktywna
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Beauty Salon API v1");
        c.RoutePrefix = string.Empty; // Swagger na głównej stronie (/)
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");  // ← CORS MUSI BYĆ przed Authorizacją!

app.UseAuthorization();

app.MapControllers();

// Inicjalizacja bazy danych - dodaj seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<GpDbContext>();
    DbInitializer.Initialize(context);
}

app.Run();