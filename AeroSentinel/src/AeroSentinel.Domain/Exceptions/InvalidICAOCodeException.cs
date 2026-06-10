namespace AeroSentinel.Domain.Exceptions;

public sealed class InvalidICAOCodeException : DomainException
    {
        public InvalidICAOCodeException(string value, string message)
            : base($"Invalid ICAO code: '{value}'. {message}")
        {
        }
    }