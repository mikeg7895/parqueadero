namespace Parqueadero.Models;

public class Cobro
{
    public int Id { get; set; }
    public Reserva Reserva { get; set; } = new Reserva();
    public Tarifa Tarifa { get; set; } = new Tarifa();
    public Descuento Descuento { get; set; } = new Descuento();
    public decimal Total { get; set; }
    public DateTime FechaCobro { get; set; } = DateTime.Now;
}