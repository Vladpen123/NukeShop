using Microsoft.EntityFrameworkCore;
using NLog;
using NukeShop.API.Configuration;
using NukeShop.API.Models.Logger;
using NukeShop.DAL;
using NukeShop.DAL.Infrastructure.Interfaces;
using NukeShop.DAL.Infrastructure.Repositories;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);



builder.Services.ConfigureRepositoryWrapper();

builder.Services.AddDbContext<ShopContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("ShopConnection"));
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



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
