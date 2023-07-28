using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Collini.GestioneInterventi.Dal.Migrations
{
    /// <inheritdoc />
    public partial class release2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResultNote",
                schema: "Docs",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                schema: "Registry",
                table: "ContactAddresses",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(16)",
                oldMaxLength: 16);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResultNote",
                schema: "Docs",
                table: "Jobs");

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                schema: "Registry",
                table: "ContactAddresses",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(16)",
                oldMaxLength: 16,
                oldNullable: true);
        }
    }
}
