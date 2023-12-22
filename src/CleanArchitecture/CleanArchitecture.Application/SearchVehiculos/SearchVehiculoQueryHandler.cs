using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquileres;
using Dapper;

namespace CleanArchitecture.Application.SearchVehiculos;

internal sealed class SearchVehiculoQueryHandler
    : IQueryHandler<SearchVehiculosQuery, IReadOnlyList<VehiculoResponse>>
{

    private static readonly int[] ActiveAlquilerStatuses = 
    {
        (int)AlquilerStatus.Reservado,
        (int)AlquilerStatus.Confirmado,
        (int)AlquilerStatus.Completado
    };

    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public SearchVehiculoQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<VehiculoResponse>>> Handle(SearchVehiculosQuery request, CancellationToken cancellationToken)
    {
        if (request.FechaInicio > request.FechaFin)
        {
            return new List<VehiculoResponse>();
        }

        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT 
                a.id AS Id,
                a.modelo AS Modelo,
                a.vin AS Vin,
                a.precio_monto AS Precio,
                a.precio_tipo_moneda AS TipoMoneda,
                a.direccion_pais AS Pais,
                a.direccion_departamento AS Departamento,
                a.direccion_provincia AS Provincia,
                a.direccion_ciudad AS Ciudad,
                a.direccion_calle AS Calle
            FROM vehiculos AS a
            WHERE NOT EXISTS
            (
                SELECT 1
                FROM alquileres AS b
                WHERE
                 b.vehiculo_id = a.id AND
                 b.duracion_inicio <= @EndDate AND
                 b.duracion_fin >= @StartDate AND
                 b.status = ANY(@ActiveAlquilerStatuses)
            )
        """;
    
        var vehiculos = await connection
            .QueryAsync<VehiculoResponse, DireccionResponse, VehiculoResponse>
            (
                sql,
                (vehiculo, direccion) => {
                    vehiculo.Direccion = direccion;
                    return vehiculo;
                },
                new {
                    StartDate = request.FechaInicio,
                    EndDate = request.FechaFin,
                    ActiveAlquilerStatuses
                },
                splitOn: "Pais"
            );

        return vehiculos.ToList();
    }
}