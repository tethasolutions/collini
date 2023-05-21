using Collini.GestioneInterventi.Domain.Registry;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collini.GestioneInterventi.Dal.Mappings.Registry;

public class ProductTypeMap : BaseEntityMapping<ProductType>
{
    public override void Configure(EntityTypeBuilder<ProductType> builder)
    {
        base.Configure(builder);

        base.Configure(builder);

        builder.ToTable("ProductTypes", "Registry");
        
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