using Collini.GestioneInterventi.Domain.Docs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collini.GestioneInterventi.Dal.Mappings.Docs;

public class JobMap : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.ToTable("Jobs", "Docs");

        builder.Property(e => e.Description)
            .HasMaxLength(256)
            .IsRequired();

        builder.HasMany(e => e.Notes)
            .WithOne(e => e.Job)
            .HasForeignKey(e => e.JobId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired(false);

        builder.HasMany(e => e.Quotations)
            .WithOne(e => e.Job)
            .HasForeignKey(e => e.JobId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired();

        builder.HasMany(e => e.Orders)
            .WithOne(e => e.Job)
            .HasForeignKey(e => e.JobId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired();

        builder.HasMany(e => e.Activities)
            .WithOne(e => e.Job)
            .HasForeignKey(e => e.JobId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired();
    }
}