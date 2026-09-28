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
            migrationBuilder.Sql(
                """
                ALTER TABLE aircraft_credential
                ALTER COLUMN "VerificationKey" TYPE varchar(512)
                USING convert_from("VerificationKey", 'UTF8');
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                ALTER TABLE aircraft_credential
                ALTER COLUMN "VerificationKey" TYPE bytea
                USING convert_to("VerificationKey", 'UTF8');
                """);
        }
    }
}
