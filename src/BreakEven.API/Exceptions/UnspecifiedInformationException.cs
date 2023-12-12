namespace BreakEven.API.Exceptions;

public class UnspecifiedInformationException(object? obj)
    : Exception($"Unspecified information for {obj?.GetType().Name}");