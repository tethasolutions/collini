using Collini.GestioneInterventi.Domain.Docs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collini.GestioneInterventi.Dal.Mappings.Docs;

public class QuotationMap : BaseEntityMapping<Quotation>
{
    public override void Configure(EntityTypeBuilder<Quotation> builder)
    {
        base.Configure(builder);

        builder.ToTable("Quotations", "Docs");

        builder.Property(e => e.Amount)
            .HasPrecision(14, 2);

        builder.HasMany(e => e.Notes)
            .WithOne(e => e.Quotation)
            .HasForeignKey(e => e.QuotationId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired(false);

        builder.HasOne(e => e.Attachment)
            .WithOne(e => e.Quotation)
            .HasForeignKey<QuotationAttachment>(x=>x.QuotationId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired();
    }
}