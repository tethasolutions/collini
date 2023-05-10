using Collini.GestioneInterventi.Domain.Docs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collini.GestioneInterventi.Dal.Mappings.Docs;

public class QuotationMap : IEntityTypeConfiguration<Quotation>
{
    public void Configure(EntityTypeBuilder<Quotation> builder)
    {
        builder.ToTable("Quotations", "Docs");

        builder.HasMany(e => e.Notes)
            .WithOne(e => e.Quotation)
            .HasForeignKey(e => e.QuotationId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired(false);
    }
}