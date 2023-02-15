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
var MyAllowSpecificOrigins = "https://localhost:7152";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7152",
                                              "https://localhost:7152")
                                                .AllowAnyHeader()
                                             .AllowAnyMethod();
                      });
});

builder.Services.AddScoped<IApplicationBuilder, ApplicationBuilder>();
builder.Services.AddScoped<IProducto , ProductoService>();
builder.Services.AddScoped<ICConexion , CConexionBueno>();
builder.Services.AddScoped<ICategoria , CategoriaService>();
builder.Services.AddScoped<ISucursal , SucursalService>();
builder.Services.AddScoped<IOperacion, OperacionService>();
builder.Services.AddScoped<IOperacionProducto, OperacionProductoService>();
builder.Services.AddScoped<IInventario, InventarioService>();

builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

var app = builder.Build();
app.UseStaticFiles(new StaticFileOptions
{
    ServeUnknownFileTypes = true,
    DefaultContentType = "application/octet-stream"
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllers();

app.Run();
