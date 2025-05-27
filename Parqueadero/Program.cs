using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Parqueadero.Config;
using Parqueadero.Models;
using Parqueadero.Observers.FacturaObserver;
using Parqueadero.Repositories;
using Parqueadero.Repositories.Interfaces;
using Parqueadero.Services;
using Parqueadero.Services.Interfaces;
using Parqueadero.Strategies.Bill;
using Parqueadero.Strategies.Descuento;
using Parqueadero.Strategies.Tarifa;
using Parqueadero.Builder;
using QuestPDF.Infrastructure;

// Configurar QuestPDF
QuestPDF.Settings.License = LicenseType.Community;

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


// Configurar EmailSettings
builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("EmailSettings"));

// Registrar estrategias
builder.Services.AddTransient<IDescuentoStrategy, NoDescuentoStrategy>();
builder.Services.AddTransient<IDescuentoStrategy, CodigoDescuentoStrategy>();
builder.Services.AddTransient<ITarifaStrategy, TarifaPorHoraStrategy>();
builder.Services.AddTransient<ITarifaStrategy, TarifaPorFinDeSemanaStrategy>();
builder.Services.AddTransient<HTMLBillStrategy>();
builder.Services.AddTransient<PDFBillStrategy>();
builder.Services.AddSingleton<IBillStrategySelector, BillStrategySelector>();

// Registrar builder
builder.Services.AddTransient<IContentBuilder, ReporteContentBuilder>();

// Registrar observers
builder.Services.AddTransient<IFacturaObserver, EmailFacturaObserver>();
builder.Services.AddTransient<IFacturaNotifier, FacturaNotifier>();

// Registrar repositorios
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IEmpresaRepositorio, EmpresaRepositorio>();
builder.Services.AddScoped<IParqueaderoRepositorio, ParqueaderoRepositorio>();
builder.Services.AddScoped<IPisosRepositorio, PisosRepositorio>();
builder.Services.AddScoped<IZonaRepositorio, ZonaRepositorio>();
builder.Services.AddScoped<ITarifaRepositorio, TarifaRepositorio>();
builder.Services.AddScoped<IVehiculoRepositorio, VehiculoRepositorio>();
builder.Services.AddScoped<IReservaRepositorio, ReservaRepositorio>();
builder.Services.AddScoped<IDescuentoRepositorio, DescuentoRepositorio>();
builder.Services.AddScoped<ICobroRepositorio, CobroRepositorio>();
builder.Services.AddScoped<IReciboRepositorio, ReciboRepositorio>();
builder.Services.AddScoped<IPlanEspecialRepositorio, PlanEspecialRepositorio>();

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
builder.Services.AddScoped<IReservaServicio, ReservaServicio>();
builder.Services.AddScoped<IDescuentoServicio, DescuentoServicio>();
builder.Services.AddScoped<IEmailServicio, EmailServicio>();
builder.Services.AddScoped<IReciboServicio, ReciboServicio>();
builder.Services.AddScoped<ICobroServicio, CobroServicio>();
builder.Services.AddScoped<IPlanEspecialServicio, PlanEspecialServicio>();

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
