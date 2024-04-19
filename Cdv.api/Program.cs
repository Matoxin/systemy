using Cdv.Api.Database;
using Microsoft.EntityFrameworkCore;
using Cdv.Api.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString(name: "PeopleDb");
builder.Services.AddDbContext<PeopleDb>(optionsAction: options =>
{
    options.UseSqlServer(connectionString);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet(pattern: "/people", handler: async (PeopleDb db) =>
{ var people = await db.People.ToListAsync();
    return Results.Ok(people);
    });

app.Run();