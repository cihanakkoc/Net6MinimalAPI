global using Microsoft.EntityFrameworkCore;
global using Net6MinimalAPI.Entities;
using Net6MinimalAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CarDBContext>(options => options.UseInMemoryDatabase("cardb"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.MapPost("/car", async (CarDBContext context, Car car) =>
{
    context.Cars.Add(car);
    await context.SaveChangesAsync();
    return Results.Ok(await context.Cars.ToListAsync());
});

app.MapDelete("/car/{id}", async (CarDBContext context, int id) =>
{
    var _car = await context.Cars.FindAsync(id);
    if (_car == null) return Results.NotFound("No car to delete.");

    context.Cars.Remove(_car);
    await context.SaveChangesAsync();

    return Results.Ok(await context.Cars.ToListAsync());
});

app.MapGet("/car", async (CarDBContext context) => await context.Cars.ToListAsync());

app.MapGet("/car/{id}", async (CarDBContext context, int id) => await context.Cars.FindAsync(id) is Car car ? Results.Ok(car) : Results.NotFound("No car."));

app.MapPut("/car/{id}", async (CarDBContext context, Car car, int id) =>
{
    var _car = await context.Cars.FindAsync(id);
    if (_car == null) return Results.NotFound("Nope.");

    _car.Year = car.Year;
    _car.Brand = car.Brand;
    _car.Model = car.Model;
    await context.SaveChangesAsync();

    return Results.Ok(await context.Cars.ToListAsync());
});

app.Run();