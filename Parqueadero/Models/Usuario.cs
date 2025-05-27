namespace Parqueadero.Models;

public enum Rol 
{
    SuperUsuario,
    Administrador,
    Trabajador,
    Cliente
}

public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public Rol Rol { get; set; } = Rol.Cliente;
    public string Clave { get; set; } = string.Empty;
    public int? EmpresaId { get; set; }
    public int? PlanEspecialId { get; set; }

    public Empresa? Empresa { get; set; }
    public PlanEspecial? PlanEspecial { get; set; }
}