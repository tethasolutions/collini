using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Collini.GestioneInterventi.Domain.Security;
using Collini.GestioneInterventi.Framework.Security;

namespace Collini.GestioneInterventi.Dal.Mappings.Security
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "Security");

            builder.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(e => e.PasswordHash)
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(e => e.Salt)
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(e => e.AccessToken)
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(e => e.EmailAddress)
                .HasMaxLength(128);

            builder.HasMany(e => e.Activities)
                .WithOne(e => e.Operator)
                .HasForeignKey(e => e.OperatorId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasData(GetData());
        }

        private static User[] GetData()
        {
            var passwordHasher = new PasswordHasher();

            return new[]
            {
                new User
                {
                    Id = 1,
                    UserName = "administrator",
                    AccessToken = "a0f0a2ffd0f37c955fda023ed287c12fab375bfc0c3e58f96114c9eeb20066b0",
                    EmailAddress = "info@collini.it",
                    Enabled = true,
                    PasswordHash = passwordHasher.HashPassword("c0ll1n1@dm1n", "f3064d73de0ca6b806ad24df65a59e1eb692393fc3f0b0297e37df522610b58b"),
                    Role = Role.Administrator,
                    Salt = "f3064d73de0ca6b806ad24df65a59e1eb692393fc3f0b0297e37df522610b58b"
                }
            };
        }
    }
}
