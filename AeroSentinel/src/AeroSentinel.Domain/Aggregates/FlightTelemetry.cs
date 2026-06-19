using System;
using AeroSentinel.Domain.Exceptions;
using AeroSentinel.Domain.ValueObjects;

namespace AeroSentinel.Domain.Entities;

public sealed class FlightTelemetry //Aggregate root
{
    // Identification Markers
    public Guid MessageId { get; init;}
    public string ICAO24 { get; init;} = null!; //FK to Aircraft Profile root
    public string Callsign { get; init;} = null!;
    public string Squawk {get;init; } = null!;
    public DateTime TimestampUtc { get; init;} 

    //WHERE THE AIRCRAFT IS CURRENTLY
    SpatialState SpatialState {get; init;} //Value object
   
   //WHAT THE AIRCRAFT IS CURRENTLY DOING
    FlightIntent FlightIntent {get; init;} //Value object

    public long SequenceNumber {get; init;}
    public string Signature{get; init;} = null!;

    public TelemetryStatus Status { get; private set; }
    public string? FailureReason { get; private set; } 

    private FlightTelemetry()
    {
        // Parameterless constructor for ORM and serialization
    }
    public FlightTelemetry(
        string icao24,
        string callsign,
        string squawk,
        DateTime timestampUtc,
        SpatialState spatialState,
        FlightIntent flightIntent,
        long sequenceNumber,
        string signature)
    {
        MessageId = Guid.NewGuid();

        ICAO24 = AircraftProfileValidation.RequireValidICAO24(icao24, nameof(icao24));

        Callsign = PhilippineFlightPlanDetailsValidation.RequireValidCallsign(callsign, nameof(callsign));

        TimestampUtc = FlightTelemetryValidation.RequireValidTimestamp(timestampUtc, nameof(timestampUtc));

        SpatialState = FlightTelemetryValidation.RequireValidSpatialState(spatialState, nameof(spatialState));
       
        FlightIntent = FlightTelemetryValidation.RequireValidFlightIntent(flightIntent, nameof(flightIntent));
       
        SequenceNumber = FlightTelemetryValidation.RequireValidSequenceNumber(sequenceNumber, nameof(sequenceNumber));
       
        Signature = FlightTelemetryValidation.RequireValidSignature(signature, nameof(signature));
        
        Status = TelemetryStatus.Stale;
        
        FailureReason = "Pending multi-stage verification analysis pipeline.";
    }

    public void MarkAsCompromised(string explanation)
    {
        if(string.IsNullOrEmpty(explanation))
        {
            throw new NullException(explanation, nameof(explanation));
        }

        Status = TelemetryStatus.Compromised;
        FailureReason = explanation.Trim();
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