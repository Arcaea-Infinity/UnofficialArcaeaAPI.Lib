namespace UnofficialArcaeaAPI.Lib;

public class UaaException : Exception
{
    public UaaException(string message) : base(message)
    {
    }
}

public class UaaRequestException : UaaException
{
    public int Status { get; }

    public UaaRequestException(int status, string message)
        : base(message)
    {
        Status = status;
    }
}

public sealed class UaaBestsSessionPendingException : UaaRequestException
{
    public bool IsQuerying => Status == -31;
    
    public bool IsWaitingForAccount => Status == -32;
    
    public int QueriedCharts { get; init; }
    
    public int CurrentAccount { get; init; }

    public UaaBestsSessionPendingException(int status, string message)
        : base(status, message)
    {
    }
}