using Collini.GestioneInterventi.Domain.Docs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collini.GestioneInterventi.Dal.Mappings.Docs;

public class NoteMap : BaseEntityMapping<Note>
{
    public override void Configure(EntityTypeBuilder<Note> builder)
    {
        base.Configure(builder);

        builder.ToTable("Notes", "Docs");

        builder.Property(e => e.Value)
            .IsRequired();

        builder.HasMany(e => e.Attachments)
            .WithOne(e => e.Note)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired();
    }
}