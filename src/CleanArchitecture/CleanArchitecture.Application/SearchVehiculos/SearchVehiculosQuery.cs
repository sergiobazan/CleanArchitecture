using CleanArchitecture.Application.Abstractions.Messaging;

namespace CleanArchitecture.Application.SearchVehiculos;

public sealed record SearchVehiculosQuery(
    DateOnly FechaInicio,
    DateOnly FechaFin
) : IQuery<IReadOnlyList<VehiculoResponse>>;