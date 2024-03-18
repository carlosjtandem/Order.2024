using Microsoft.EntityFrameworkCore;
using Orders.Backend.Controllers.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Inyeccion de dbContext para conexion.
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=ConexionSql"));
var app = builder.Build();

app.UseCors(x=> x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin=> true).AllowCredentials());

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