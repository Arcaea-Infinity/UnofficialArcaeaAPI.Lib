namespace ArcaeaUnlimitedAPI.Lib.Utils;

public class AuaException : Exception
{
    public int Status;

    public AuaException(int status, string message)
        : base(message)
    {
        Status = status;
    }
}