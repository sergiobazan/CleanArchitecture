using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Alquileres;

public sealed record PrecioDetalle(
    Moneda PrecioPorPeriodo,
    Moneda Mantenimiento,
    Moneda Accesorios,
    Moneda PrecioTotal
);