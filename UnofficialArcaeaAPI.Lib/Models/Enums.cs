namespace UnofficialArcaeaAPI.Lib.Models;

[Flags]
public enum UaaReplyWith
{
    None = 0,
    SongInfo = 1,
    Recent = 2,
    All = SongInfo | Recent
}

public enum UaaSongQueryType
{
    SongName,
    SongId,
    FileName
}

public enum ArcaeaDifficulty
{
    Past = 0,
    Present = 1,
    Future = 2,
    Beyond = 3
}