using Collini.GestioneInterventi.Domain.Docs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collini.GestioneInterventi.Dal.Mappings.Docs;

public class QuotationAttachmentMap : BaseEntityMapping<QuotationAttachment>
{
    public override void Configure(EntityTypeBuilder<QuotationAttachment> builder)
    {
        base.Configure(builder);

        builder.ToTable("QuotationAttachments", "Docs");

        builder.Property(e => e.DisplayName)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(e => e.FileName)
            .HasMaxLength(64)
            .IsRequired();
    }
}