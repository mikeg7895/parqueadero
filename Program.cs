using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Parqueadero.Models;
using Parqueadero.Repositories;
using Parqueadero.Repositories.Interfaces;
using Parqueadero.Services;
using Parqueadero.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

// Configurar la autenticación de cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Autenticacion/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        // options.LogoutPath = "/Autenticacion/Logout";
        options.AccessDeniedPath = "/Autenticacion/AccessDenied";
    });

// Configurar DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Registrar repositorios
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IEmpresaRepositorio, EmpresaRepositorio>();
builder.Services.AddScoped<IParqueaderoRepositorio, ParqueaderoRepositorio>();
builder.Services.AddScoped<IPisosRepositorio, PisosRepositorio>();
builder.Services.AddScoped<IZonaRepositorio, ZonaRepositorio>();
builder.Services.AddScoped<ITarifaRepositorio, TarifaRepositorio>();
builder.Services.AddScoped<IVehiculoRepositorio, VehiculoRepositorio>();

// Registrar servicios
builder.Services.AddScoped<IEncriptacionService, EncriptacionService>();
builder.Services.AddScoped<IAutenticacionServicio, AutenticacionServicio>();
builder.Services.AddScoped<IEmpresaServicio, EmpresaServicio>();
builder.Services.AddScoped<IParqueaderoServicio, ParqueaderoServicio>();
builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>();
builder.Services.AddScoped<IPisoServicio, PisoServicio>();
builder.Services.AddScoped<IZonaServicio, ZonaServicio>();
builder.Services.AddScoped<ITarifaServicio, TarifaServicio>();
builder.Services.AddScoped<IVehiculoServicio, VehiculoServicio>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Autenticacion}/{action=Login}/{id?}")
    .WithStaticAssets();


app.Run();
