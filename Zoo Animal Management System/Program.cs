using Microsoft.EntityFrameworkCore;
using System;
using Zoo_Animal_Management_System.Database;
using Zoo_Animal_Management_System.Services;
using Zoo_Animal_Management_System.Services.Adapters;
using Zoo_Animal_Management_System.Services.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IAnimalAdapter, AnimalAdapter>();
builder.Services.AddScoped<IEnclosureAdapter, EnclosureAdapter>();
builder.Services.AddScoped<IAnimalDistributionService, AnimalDistributionService>();
builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
builder.Services.AddScoped<IEnclosureRepository, EnclosureRepository>();



// Database realated
var connString = builder.Configuration.GetConnectionString("Database");
builder.Services.AddDbContext<ZooDbContext>(o => o.UseSqlServer(connString));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
