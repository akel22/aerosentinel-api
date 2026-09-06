using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AeroSentinel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateTestDb3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "aircraft_profile",
                columns: table => new
                {
                    ICAO24 = table.Column<string>(type: "text", nullable: false),
                    Registration = table.Column<string>(type: "text", nullable: false),
                    AircraftType = table.Column<string>(type: "text", nullable: false),
                    Manufacturer = table.Column<string>(type: "text", nullable: false),
                    AircraftPerformance_CruiseSpeedKnots = table.Column<double>(type: "double precision", nullable: true),
                    AircraftPerformance_MaxVelocityKnots = table.Column<double>(type: "double precision", nullable: true),
                    AircraftPerformance_MaxAltitudeFeet = table.Column<double>(type: "double precision", nullable: true),
                    AircraftPerformance_MaxClimbRateFeetPerMinute = table.Column<double>(type: "double precision", nullable: true),
                    AircraftPerformance_MaxDescentRateFeetPerMinute = table.Column<double>(type: "double precision", nullable: true),
                    AircraftPerformance_MaxTurnRateDegreesPerSecond = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aircraft_profile", x => x.ICAO24);
                });

            migrationBuilder.CreateTable(
                name: "flight_plan",
                columns: table => new
                {
                    FlightPlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    ICAO24 = table.Column<string>(type: "text", nullable: false),
                    Callsign = table.Column<string>(type: "text", nullable: false),
                    DepartureAirport = table.Column<string>(type: "text", nullable: false),
                    DestinationAirport = table.Column<string>(type: "text", nullable: false),
                    DepartureTimeUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EstimatedArrivalTimeUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ActualArrivalTimeUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FlightStatus = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flight_plan", x => x.FlightPlanId);
                });

            migrationBuilder.CreateTable(
                name: "waypoint",
                columns: table => new
                {
                    WaypointId = table.Column<string>(type: "text", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_waypoint", x => x.WaypointId);
                });

            migrationBuilder.CreateTable(
                name: "aircraft_credential",
                columns: table => new
                {
                    CredentialId = table.Column<Guid>(type: "uuid", nullable: false),
                    ICAO24 = table.Column<string>(type: "text", nullable: false),
                    VerificationKey = table.Column<byte[]>(type: "bytea", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aircraft_credential", x => x.CredentialId);
                    table.ForeignKey(
                        name: "FK_aircraft_credential_aircraft_profile_ICAO24",
                        column: x => x.ICAO24,
                        principalTable: "aircraft_profile",
                        principalColumn: "ICAO24",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "flight_telemetry",
                columns: table => new
                {
                    MessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    ICAO24 = table.Column<string>(type: "text", nullable: false),
                    FlightPlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    Callsign = table.Column<string>(type: "text", nullable: false),
                    Squawk = table.Column<string>(type: "text", nullable: false),
                    TimestampUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SpatialState_Coordinates_Latitude = table.Column<double>(type: "double precision", nullable: true),
                    SpatialState_Coordinates_Longitude = table.Column<double>(type: "double precision", nullable: true),
                    SpatialState_BaroAltitudeFeet = table.Column<double>(type: "double precision", nullable: false),
                    SpatialState_GeoAltitudeFeet = table.Column<double>(type: "double precision", nullable: false),
                    SpatialState_GroundSpeedKnots = table.Column<double>(type: "double precision", nullable: false),
                    SpatialState_TrackAngleDegrees = table.Column<double>(type: "double precision", nullable: false),
                    FlightIntent_VerticalRateFpm = table.Column<double>(type: "double precision", nullable: false),
                    FlightIntent_SelectedAltitudeFeet = table.Column<double>(type: "double precision", nullable: false),
                    FlightIntent_IndicatedAirspeedKnots = table.Column<double>(type: "double precision", nullable: false),
                    FlightIntent_MagneticHeadingDegrees = table.Column<double>(type: "double precision", nullable: false),
                    FlightIntent_RollAngleDegrees = table.Column<double>(type: "double precision", nullable: false),
                    SequenceNumber = table.Column<long>(type: "bigint", nullable: false),
                    Signature = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    FailureReason = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flight_telemetry", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_flight_telemetry_flight_plan_FlightPlanId",
                        column: x => x.FlightPlanId,
                        principalTable: "flight_plan",
                        principalColumn: "FlightPlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "flight_plan_route",
                columns: table => new
                {
                    FlightPlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    Sequence = table.Column<int>(type: "integer", nullable: false),
                    WaypointId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flight_plan_route", x => new { x.FlightPlanId, x.Sequence });
                    table.ForeignKey(
                        name: "FK_flight_plan_route_flight_plan_FlightPlanId",
                        column: x => x.FlightPlanId,
                        principalTable: "flight_plan",
                        principalColumn: "FlightPlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_flight_plan_route_waypoint_WaypointId",
                        column: x => x.WaypointId,
                        principalTable: "waypoint",
                        principalColumn: "WaypointId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_aircraft_credential_ICAO24",
                table: "aircraft_credential",
                column: "ICAO24",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_aircraft_profile_Registration",
                table: "aircraft_profile",
                column: "Registration",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_flight_plan_route_WaypointId",
                table: "flight_plan_route",
                column: "WaypointId");

            migrationBuilder.CreateIndex(
                name: "IX_flight_telemetry_Callsign_TimestampUtc",
                table: "flight_telemetry",
                columns: new[] { "Callsign", "TimestampUtc" });

            migrationBuilder.CreateIndex(
                name: "IX_flight_telemetry_FlightPlanId_SequenceNumber",
                table: "flight_telemetry",
                columns: new[] { "FlightPlanId", "SequenceNumber" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aircraft_credential");

            migrationBuilder.DropTable(
                name: "flight_plan_route");

            migrationBuilder.DropTable(
                name: "flight_telemetry");

            migrationBuilder.DropTable(
                name: "aircraft_profile");

            migrationBuilder.DropTable(
                name: "waypoint");

            migrationBuilder.DropTable(
                name: "flight_plan");
        }
    }
}
