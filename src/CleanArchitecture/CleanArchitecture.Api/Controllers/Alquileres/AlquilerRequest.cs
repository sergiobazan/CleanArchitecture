namespace CleanArchitecture.Api.Controllers.Alquileres;

public sealed record AlquilerRequest(Guid VehiculoId, Guid UserId, DateOnly StartDate, DateOnly EndDate);