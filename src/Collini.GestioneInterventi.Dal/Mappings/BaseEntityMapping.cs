using Collini.GestioneInterventi.Dal.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collini.GestioneInterventi.Dal.Mappings;

public abstract class BaseEntityMapping<T> : IEntityTypeConfiguration<T> where T : class
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.ConfigureDefaultDateTimeOffsetPrecision();
        builder.ConfigureDefaultDecimalPrecision();
    }
}