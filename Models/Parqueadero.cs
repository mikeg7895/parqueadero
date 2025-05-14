namespace Parqueadero.Models;

public class Parqueadero
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public decimal Latitud { get; set; }
    public decimal Longitud { get; set; }
    public int EmpresaId { get; set; }

    public Empresa? Empresa { get; set; }
    public ICollection<Piso> Pisos { get; set; } = [];
    public ICollection<Tarifa> Tarifas { get; set; } = [];
}