using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Vehiculos;

public sealed class Vehiculo : Entity
{
    public Vehiculo(
        Guid id,
        Modelo modelo,
        Vin vin,
        Moneda precio,
        Moneda matenimiento,
        DateTime? fechaUltimaAlquiler,
        List<Accesorio> accesorios,
        Direccion? direccion
        ) : base(id)
    {
        Modelo = modelo;
        Vin = vin;
        Precio = precio;
        Matenimiento = matenimiento;
        FechaUltimaAlquiler = fechaUltimaAlquiler;
        Direccion = direccion;
        Accesorios = accesorios;
    }

   public Modelo? Modelo { get; private set; }
   public Vin? Vin { get; private set; }
   public Direccion? Direccion { get; private set; }
   public Moneda? Precio { get; private set; }
   public Moneda? Matenimiento { get; private set; }
   public DateTime? FechaUltimaAlquiler { get; private set; }
   public List<Accesorio> Accesorios { get; private set; } = new();
}