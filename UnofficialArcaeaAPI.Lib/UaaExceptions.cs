namespace UnofficialArcaeaAPI.Lib;

public class UaaException : Exception
{
    public UaaException(string message) : base(message)
    {
    }
}

public sealed class UaaRequestException : UaaException
{
    public int Status { get; }

    public UaaRequestException(int status, string message)
        : base(message)
    {
        Status = status;
    }
}