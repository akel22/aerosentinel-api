

namespace AeroSentinel.Application.Security;

public sealed class ReplayProtectionService
    : IReplayProtectionService
{
    private static readonly TimeSpan AllowedClockDrift = TimeSpan.FromSeconds(30);

    public void ValidateSequence(
        DateTime timestampUtc,
        long incomingSequence,
        long lastAcceptedSequence)
    {
        if(timestampUtc < (DateTime.UtcNow - AllowedClockDrift))
        {
            throw new ReplayAttackException(null,
                "Telemetry timestamp expired.");
        }

        if(incomingSequence <= lastAcceptedSequence)
        {
            throw new ReplayAttackException(incomingSequence,
                $"Replay of telemetry detected");
        }
    }
}