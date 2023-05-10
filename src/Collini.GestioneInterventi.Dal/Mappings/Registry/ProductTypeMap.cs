using Collini.GestioneInterventi.Domain.Registry;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collini.GestioneInterventi.Dal.Mappings.Registry;

public class ProductTypeMap : IEntityTypeConfiguration<ProductType>
{
    public void Configure(EntityTypeBuilder<ProductType> builder)
    {
        builder.ToTable("ProductTypes", "Registry");

        builder.Property(e => e.Code)
            .HasMaxLength(16)
            .IsRequired();

        builder.Property(e => e.Name)
            .HasMaxLength(128)
            .IsRequired();

        builder.HasMany(e => e.Jobs)
            .WithOne(e => e.ProductType)
            .HasForeignKey(e => e.ProductTypeId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired();
    }
}