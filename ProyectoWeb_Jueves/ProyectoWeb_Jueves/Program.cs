using ProyectoWeb_Jueves.Models;
using ProyectoWeb_Jueves.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpClient();

//ADD INTERFACES
builder.Services.AddSingleton<IUsuarioModel, UsuarioModel>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=IniciarSesion}/{id?}");

app.Run();
