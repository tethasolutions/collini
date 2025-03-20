using Collini.GestioneInterventi.Domain.Registry;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collini.GestioneInterventi.Dal.Mappings.Registry;

public class SmtpSettingsMap : BaseEntityMapping<SmtpSettings>
{
    public override void Configure(EntityTypeBuilder<SmtpSettings> builder)
    {
        base.Configure(builder);

        builder.ToTable("SmtpSettings", "Registry");
        
    }
}