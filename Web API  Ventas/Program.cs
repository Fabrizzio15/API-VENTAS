using OfficeOpenXml;
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
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});


builder.Services.AddScoped<IApplicationBuilder, ApplicationBuilder>();
builder.Services.AddScoped<IProducto , ProductoService>();
builder.Services.AddScoped<ICConexion , Conexion>();
builder.Services.AddScoped<ICategoria , CategoriaService>();
builder.Services.AddScoped<ISucursal , SucursalService>();
builder.Services.AddScoped<IOperacion, OperacionService>();
builder.Services.AddScoped<IOperacionProducto, OperacionProductoService>();
builder.Services.AddScoped<IInventario, InventarioService>();
builder.Services.AddScoped<IPersona, PersonaService>();
builder.Services.AddScoped<IVendedor, VendedorService>();

builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles(new StaticFileOptions
{
    ServeUnknownFileTypes = true,
    DefaultContentType = "application/octet-stream"
});
app.UseSwagger();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}
app.Use(async (context, next) =>
{
    ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Establecer el contexto de licencia
    await next.Invoke();
});

app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
