using Collini.GestioneInterventi.Domain.Registry;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collini.GestioneInterventi.Dal.Mappings.Registry;

public class ContactMap : BaseEntityMapping<Contact>
{
    public override void Configure(EntityTypeBuilder<Contact> builder)
    {
        base.Configure(builder);

        builder.ToTable("Contacts", "Registry");

        builder.Property(e => e.CompanyName)
            .HasMaxLength(256);

        builder.Property(e => e.Name)
            .HasMaxLength(64);

        builder.Property(e => e.Surname)
            .HasMaxLength(64);

        builder.Property(e => e.ErpCode)
            .HasMaxLength(16);

        builder.Property(e => e.Telephone)
            .HasMaxLength(32);

        builder.Property(e => e.Email)
            .HasMaxLength(128);

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