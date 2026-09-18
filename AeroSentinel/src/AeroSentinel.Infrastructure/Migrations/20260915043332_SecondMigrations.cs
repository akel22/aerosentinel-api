using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AeroSentinel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VerificationKey",
                table: "aircraft_credential",
                type: "varchar(512)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "bytea");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "VerificationKey",
                table: "aircraft_credential",
                type: "bytea",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(512)");
        }
    }
}
