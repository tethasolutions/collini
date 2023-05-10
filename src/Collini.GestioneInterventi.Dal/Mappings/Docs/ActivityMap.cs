using Collini.GestioneInterventi.Domain.Docs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collini.GestioneInterventi.Dal.Mappings.Docs;

public class ActivityMap : BaseEntityMapping<Activity>
{
    public override void Configure(EntityTypeBuilder<Activity> builder)
    {
        base.Configure(builder);

        builder.ToTable("Activities", "Docs");

        builder.Property(e => e.Description)
            .IsRequired();

        builder.HasMany(e => e.Notes)
            .WithOne(e => e.Activity)
            .HasForeignKey(e => e.ActivityId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired(false);
    }
}