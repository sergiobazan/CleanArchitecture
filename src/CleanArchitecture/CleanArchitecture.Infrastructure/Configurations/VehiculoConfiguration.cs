using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehiculos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;

internal sealed class VehiculoConfiguration : IEntityTypeConfiguration<Vehiculo>
{
    public void Configure(EntityTypeBuilder<Vehiculo> builder)
    {
        builder.ToTable("vehiculos");
        builder.HasKey(Vehiculo => Vehiculo.Id);

        builder.OwnsOne(Vehiculo => Vehiculo.Direccion);

        builder.Property(Vehiculo => Vehiculo.Modelo)
            .HasMaxLength(200)
            .HasConversion(modelo => modelo!.Value, value => new Modelo(value));

        builder.Property(vehiculo => vehiculo.Vin)
            .HasMaxLength(500)
            .HasConversion(vin => vin!.Value, value => new Vin(value));
        
        builder.OwnsOne(Vehiculo => Vehiculo.Precio, priceBuilder => {
            priceBuilder.Property(moneda => moneda.TipoMoneda)
                .HasConversion(tipoMoneda => tipoMoneda.Codigo, codigo => TipoMoneda.FromCodigo(codigo!));
        });

        builder.OwnsOne(vehiculo => vehiculo.Matenimiento, priceBuilder => {
            priceBuilder.Property(moneda => moneda.TipoMoneda)
            .HasConversion(tipoMoneda => tipoMoneda.Codigo, codigo => TipoMoneda.FromCodigo(codigo!));
        });
    }
}