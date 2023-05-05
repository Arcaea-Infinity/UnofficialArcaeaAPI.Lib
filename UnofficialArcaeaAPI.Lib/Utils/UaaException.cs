namespace UnofficialArcaeaAPI.Lib.Utils;

public class UaaException : Exception
{
    public int Status;

    public UaaException(int status, string message)
        : base(message)
    {
        Status = status;
    }
}