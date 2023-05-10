using Collini.GestioneInterventi.Domain.Registry;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collini.GestioneInterventi.Dal.Mappings.Registry;

public class ContactMap : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("Contacts", "Registry");

        builder.Property(e => e.CompanyName)
            .HasMaxLength(256);

        builder.Property(e => e.Name)
            .HasMaxLength(64);

        builder.Property(e => e.Surname)
            .HasMaxLength(64);

        builder.Property(e => e.ErpCode)
            .HasMaxLength(16);

        builder.HasMany(e => e.Addresses)
            .WithOne(e => e.Contact)
            .HasForeignKey(e => e.ContactId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired();

        builder.HasMany(e => e.Jobs)
            .WithOne(e => e.Customer)
            .HasForeignKey(e => e.CustomerId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired();

        builder.HasMany(e => e.Orders)
            .WithOne(e => e.Supplier)
            .HasForeignKey(e => e.SupplierId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired();
    }
}