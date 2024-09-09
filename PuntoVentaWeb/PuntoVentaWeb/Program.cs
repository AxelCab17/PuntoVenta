using PuntoVentaWeb.Models;
using PuntoVentaWeb.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IUsuarioModel, UsuarioModel>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.AddSingleton<IProveedorModel, ProveedorModel>();
builder.Services.AddSingleton<IFacturaModel, FacturaModel>();
builder.Services.AddSingleton<IEmpleadoModel, EmpleadoModel>();
builder.Services.AddSingleton<IComunModel, ComunModel>();
builder.Services.AddSingleton<ICarritoModel, CarritoModel>();
builder.Services.AddSingleton<INominaModel, NominaModel>();
builder.Services.AddSingleton<IProductoModel, ProductoModel>();
builder.Services.AddSingleton<IInventarioModel, InventarioModel>();
builder.Services.AddSingleton<ICategoriaModel, CategoriaModel>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",

    pattern: "{controller=Usuario}/{action=LoginUsuario}/{id?}");

app.Run();
