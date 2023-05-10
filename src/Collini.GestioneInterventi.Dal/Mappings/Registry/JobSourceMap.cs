using Collini.GestioneInterventi.Domain.Registry;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collini.GestioneInterventi.Dal.Mappings.Registry;

public class JobSourceMap : IEntityTypeConfiguration<JobSource>
{
    public void Configure(EntityTypeBuilder<JobSource> builder)
    {
        builder.ToTable("JobSources", "Registry");

        builder.Property(e => e.Code)
            .HasMaxLength(16)
            .IsRequired();

        builder.Property(e => e.Name)
            .HasMaxLength(128)
            .IsRequired();

        builder.HasMany(e => e.Jobs)
            .WithOne(e => e.Source)
            .HasForeignKey(e => e.SourceId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired();
    }
}