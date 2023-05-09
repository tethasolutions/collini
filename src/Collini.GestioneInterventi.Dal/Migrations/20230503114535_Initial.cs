using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Collini.GestioneInterventi.Dal.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Security");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    AccessToken = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "Users",
                columns: new[] { "Id", "AccessToken", "EmailAddress", "Enabled", "PasswordHash", "Role", "Salt", "UserName" },
                values: new object[] { 1L, "a0f0a2ffd0f37c955fda023ed287c12fab375bfc0c3e58f96114c9eeb20066b0", "info@collini.it", true, "d96eb91d532452667aff24191055ebba05d8a30853367821502a55ca4b1532db", 0, "f3064d73de0ca6b806ad24df65a59e1eb692393fc3f0b0297e37df522610b58b", "administrator" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users",
                schema: "Security");
        }
    }
}
