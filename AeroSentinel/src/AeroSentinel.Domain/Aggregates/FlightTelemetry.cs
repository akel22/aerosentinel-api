using System;
using AeroSentinel.Domain.Exceptions;
using AeroSentinel.Domain.ValueObjects;

namespace AeroSentinel.Domain.Entities;

public sealed class FlightTelemetry
{
    // Identification Markers
    public Guid MessageId { get; }
    public string ICAO24 { get; } = null!;
    public string Callsign { get; } = null!;
    public string Squawk {get; } = null!;
    public DateTime TimestampUtc { get; } 

    //WHERE THE AIRCRAFT IS CURRENTLY
    SpatialState SpatialState {get;} = null!;
   
   //WHAT THE AIRCRAFT IS CURRENTLY DOING
    FlightIntent FlightIntent {get;} = null!;

    public long SequenceNumber {get;}
    public string Signature{get;} = null!;

    // public TelemetryStatus Status { get; private set; }
    // public string FailureReason { get; private set; } = null!;

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
        
        //Status = TelemetryStatus.Stale;
        
        //FailureReason = "Pending multi-stage verification analysis pipeline.";
    }

    // public void MarkAsCompromised(string explanation)
    // {
    //     Status = TelemetryStatus.Compromised;
    //     FailureReason = explanation;
    // }

    // public void MarkAsSpoofed(string explanation)
    // {
    //     Status = TelemetryStatus.Spoofed;
    //     FailureReason = explanation;
    // }

    // public void MarkAsVerified()
    // {
    //     Status = TelemetryStatus.Verified;
    //     FailureReason = string.Empty;
    // }
}