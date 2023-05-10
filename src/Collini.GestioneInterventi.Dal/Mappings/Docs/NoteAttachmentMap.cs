using Collini.GestioneInterventi.Domain.Docs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collini.GestioneInterventi.Dal.Mappings.Docs;

public class NoteAttachmentMap : BaseEntityMapping<NoteAttachment>
{
    public override void Configure(EntityTypeBuilder<NoteAttachment> builder)
    {
        base.Configure(builder);

        builder.ToTable("NoteAttachments", "Docs");

        builder.Property(e => e.DisplayName)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(e => e.FileName)
            .HasMaxLength(64)
            .IsRequired();
    }
}