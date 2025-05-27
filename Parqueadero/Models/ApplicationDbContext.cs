using Microsoft.EntityFrameworkCore;

namespace Parqueadero.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<Parqueadero> Parqueaderos { get; set; }
    public DbSet<Piso> Pisos { get; set; }
    public DbSet<Zona> Zonas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Tarifa> Tarifas { get; set; }
    public DbSet<Vehiculo> Vehiculos { get; set; }
    public DbSet<Reserva> Reservas { get; set; }
    public DbSet<Descuento> Descuentos { get; set; }
    public DbSet<Cobro> Cobros { get; set; }
    public DbSet<Recibo> Recibos { get; set; }
    public DbSet<PlanEspecial> PlanesEspeciales { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>().HasData(
            new Usuario {
                Id = 1,
                Nombre = "Super Usuario",
                Correo = "superadmin@gmail.com",
                Rol = Rol.SuperUsuario,
                Clave = "$2a$11$Fu8vYpe9qhadk.b8B/Az7enrXNKEa93JHXNyIWLo2N205GRInyw4C"
            }
        );
    }
}