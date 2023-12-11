namespace CleanArchitecture.Domain.Vehiculos;

public sealed record Direccion
(
    string Pais,
    string Departamento,
    string Provincia,
    string Ciudad,
    string Calle
);