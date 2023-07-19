using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Collini.GestioneInterventi.Dal.Migrations
{
    /// <inheritdoc />
    public partial class release1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update docs.jobs set ExpirationDate = getUtcDate() where ExpirationDate is null;");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ExpirationDate",
                schema: "Docs",
                table: "Jobs",
                type: "datetimeoffset(3)",
                precision: 3,
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(3)",
                oldPrecision: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CustomerAddressId",
                schema: "Docs",
                table: "Jobs",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "JobDate",
                schema: "Docs",
                table: "Jobs",
                type: "datetimeoffset(3)",
                precision: 3,
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.Sql("update docs.jobs set jobDate = createdOn;");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "Registry",
                table: "Contacts",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telephone",
                schema: "Registry",
                table: "Contacts",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobDate",
                schema: "Docs",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "Registry",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Telephone",
                schema: "Registry",
                table: "Contacts");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ExpirationDate",
                schema: "Docs",
                table: "Jobs",
                type: "datetimeoffset(3)",
                precision: 3,
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(3)",
                oldPrecision: 3);

            migrationBuilder.AlterColumn<long>(
                name: "CustomerAddressId",
                schema: "Docs",
                table: "Jobs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
