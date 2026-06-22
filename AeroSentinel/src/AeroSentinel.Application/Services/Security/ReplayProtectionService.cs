namespace AeroSentinel.Application.Security;

public sealed class ReplayProtectionService : IReplayProtectionService
{
    private static readonly TimeSpan AllowedClockDrift = TimeSpan.FromSeconds(30);

    public void ValidateSequence(
        DateTime timestampUtc,
        long incomingSequence,
        long? lastAcceptedSequence)
    {
        var currentUtc = DateTime.UtcNow;
        
        // 1. Dual-Bounded Window Validation
        var oldestAllowed = currentUtc - AllowedClockDrift;
        var newestAllowed = currentUtc + AllowedClockDrift;

        if (timestampUtc < oldestAllowed)
        {
            throw new ReplayAttackException(incomingSequence, "Telemetry timestamp has expired (Too stale).");
        }

        if (timestampUtc > newestAllowed)
        {
            throw new ReplayAttackException(incomingSequence, "Telemetry timestamp is out of bounds (Too far in the future).");
        }

        // 2. Cold-Start Initialization Handling
        if (lastAcceptedSequence == null)
        {
            return; 
        }

        // 3. Monotonically Increasing Sequence Check
        if (incomingSequence <= lastAcceptedSequence)
        {
            throw new ReplayAttackException(incomingSequence, 
                $"Replay attack or duplicate packet detected. Incoming: {incomingSequence}, Last Accepted: {lastAcceptedSequence}");
        }
    }
}