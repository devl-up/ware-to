namespace Infrastructure.Exceptions;

internal sealed class InfrastructureException : Exception
{
    public InfrastructureException(string message) : base(message)
    {
    }
}