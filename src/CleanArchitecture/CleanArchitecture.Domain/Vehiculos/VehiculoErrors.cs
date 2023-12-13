using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Vehiculos;

public static class VehiculoErrors
{
   public static Error NotFound = new(
    "Vehiculo.NotFound",
    "No existe un vehiculo con este id"
   ); 
}