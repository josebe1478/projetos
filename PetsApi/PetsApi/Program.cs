using Microsoft.EntityFrameworkCore;
using PetsApi.Data;
using PetsApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("petsdb"));
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/pets", async (AppDbContext db) => await db.Petss.ToListAsync());

app.MapGet("/pets/{id}", async (AppDbContext db, int id) => await db.Petss.FindAsync(id));

app.MapPut("/pets/{id}", async (AppDbContext db, Pets updatepets, int id) =>
{
    var pets = await db.Petss.FindAsync(id);
    if (pets is null) return Results.NotFound();
    pets.Nome = updatepets.Nome;
    pets.Idade = updatepets.Idade;
    pets.Cor = updatepets.Cor;
    pets.Tipo = updatepets.Tipo;
    pets.Peso_kg = updatepets.Peso_kg;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/pets/{id}", async (AppDbContext db, int id) =>
{
    var pets = await db.Petss.FindAsync(id);
    if (pets is null)
    {
        return Results.NotFound();
    }
    db.Petss.Remove(pets);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.MapPost("/pets", async (AppDbContext db, Pets pets) =>
{
    await db.Petss.AddAsync(pets);
    await db.SaveChangesAsync();
    return Results.Created($"/pets/{pets.Id}", pets);
});

app.Run();
