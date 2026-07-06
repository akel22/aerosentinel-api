using System;
using AeroSentinel.Domain.Aggregates;
using AeroSentinel.Domain.Exceptions;
using AeroSentinel.Domain.ValueObjects;

namespace AeroSentinel.Domain.Entities;

public sealed class FlightTelemetry //Aggregate root
{
    // Identification Markers
    public Guid MessageId { get; init;}
    public string ICAO24 { get; init;} = null!; //FK to Aircraft Profile root
    public Guid FlightPlanId { get; init;}
    public FlightPlan FlightPlan { get; init;} = null!;
    public string Callsign { get; init;} = null!;
    public string Squawk {get;init; } = null!;
    public DateTime TimestampUtc { get; init;} 

    //WHERE THE AIRCRAFT IS CURRENTLY
    public SpatialState SpatialState {get; init;} = null!; //Value object
   
   //WHAT THE AIRCRAFT IS CURRENTLY DOING
    public FlightIntent FlightIntent {get; init;} = null!; //Value object

    public long SequenceNumber {get; init;}
    public string Signature{get; init;} = null!;

    public TelemetryStatus Status { get; private set; }
    public string? FailureReason { get; private set; } 

    private FlightTelemetry()
    {
        // Parameterless constructor for ORM and serialization
    }
    public FlightTelemetry(
        Guid flightplanId,
        string icao24,
        string callsign,
        string squawk,
        DateTime timestampUtc,
        SpatialState spatialState,
        FlightIntent flightIntent,
        long sequenceNumber,
        string signature)
    {
        MessageId =  FlightTelemetryValidation.RequireValidMessageId(Guid.NewGuid(), null);

        FlightPlanId = FlightTelemetryValidation.RequireValidFlightPlanId(flightplanId, nameof(flightplanId));

        ICAO24 = AircraftProfileValidation.RequireValidICAO24(icao24, nameof(icao24));

        Callsign = PhilippineFlightPlanDetailsValidation.RequireValidCallsign(callsign, nameof(callsign));

        Squawk = FlightTelemetryValidation.RequireValidSquawk(squawk, nameof(squawk));

        TimestampUtc = timestampUtc;

        SpatialState = FlightTelemetryValidation.RequireValidSpatialState(spatialState, nameof(spatialState));
       
        FlightIntent = FlightTelemetryValidation.RequireValidFlightIntent(flightIntent, nameof(flightIntent));
       
        SequenceNumber = FlightTelemetryValidation.RequireValidSequenceNumber(sequenceNumber, nameof(sequenceNumber));
       
        Signature = FlightTelemetryValidation.RequireValidSignature(signature, nameof(signature));
        
        Status = TelemetryStatus.PendingVerification;
        
        FailureReason = null;


    }

    public void MarkAsSpoofed(string explanation)
    {
         if(string.IsNullOrEmpty(explanation))
        {
            throw new NullException(explanation, nameof(explanation));
        }

        Status = TelemetryStatus.Spoofed;
        FailureReason = explanation.Trim();
    }

    public void MarkAsVerified()
    {
        Status = TelemetryStatus.Verified;
        FailureReason = string.Empty;
    }
}