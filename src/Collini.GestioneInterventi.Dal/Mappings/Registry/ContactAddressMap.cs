using Collini.GestioneInterventi.Domain.Registry;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collini.GestioneInterventi.Dal.Mappings.Registry;

public class ContactAddressMap : IEntityTypeConfiguration<ContactAddress>
{
    public void Configure(EntityTypeBuilder<ContactAddress> builder)
    {
        builder.ToTable("ContactAddresses", "Registry");

        builder.Property(e => e.Description)
            .HasMaxLength(256);

        builder.Property(e => e.City)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(e => e.StreetAddress)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(e => e.Province)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(e => e.ZipCode)
            .HasMaxLength(16)
            .IsRequired();

        builder.Property(e => e.Telephone)
            .HasMaxLength(32);

        builder.Property(e => e.Email)
            .HasMaxLength(128);

        builder.HasMany(e => e.Jobs)
            .WithOne(e => e.CustomerAddress)
            .HasForeignKey(e => e.CustomerAddressId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired();
    }
}