using Web_API__Ventas;
using Web_API__Ventas.Interfaces;
using Web_API__Ventas.Modelos;
using Web_API__Ventas.Servicios;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IApplicationBuilder, ApplicationBuilder>();
builder.Services.AddScoped<IProducto , ProductoService>();
builder.Services.AddScoped<ICConexion , CConexionBueno>();
builder.Services.AddScoped<ICategoria , CategoriaService>();
builder.Services.AddScoped<ISucursal , SucursalService>();
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
