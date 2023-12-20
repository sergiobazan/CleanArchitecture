using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Vehiculos;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories;

internal sealed class AlquilerRepository: Repository<Alquiler>, IAlquilerRepository
{

    private static readonly AlquilerStatus[] ActiveAqluilerStatuses = {
        AlquilerStatus.Reservado,
        AlquilerStatus.Confirmado,
        AlquilerStatus.Completado
    };

    public AlquilerRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> IsOverlapingAsync(Vehiculo vehiculo, DateRange duracion, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Alquiler>()
            .AnyAsync(alquiler => alquiler.VehiculoId == vehiculo.Id &&
            alquiler.Duracion!.Inicio <= duracion.Fin &&
            alquiler.Duracion.Fin >= duracion.Inicio &&
            ActiveAqluilerStatuses.Contains(alquiler.Status),
            cancellationToken);
    }
}