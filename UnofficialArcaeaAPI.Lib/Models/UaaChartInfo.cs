using System.Text.Json.Serialization;

namespace UnofficialArcaeaAPI.Lib.Models;

#pragma warning disable CS8618

public class UaaChartInfo
{
    [JsonPropertyName("name_en")]
    public string NameEn { get; set; }

    [JsonPropertyName("name_jp")]
    public string NameJp { get; set; }

    [JsonPropertyName("artist")]
    public string Artist { get; set; }

    [JsonPropertyName("bpm")]
    public string Bpm { get; set; }

    [JsonPropertyName("bpm_base")]
    public double BpmBase { get; set; }

    [JsonPropertyName("set")]
    public string Set { get; set; }

    [JsonPropertyName("set_friendly")]
    public string SetFriendly { get; set; }

    [JsonPropertyName("time")]
    public int Time { get; set; }

    [JsonPropertyName("side")]
    public int Side { get; set; }

    [JsonPropertyName("world_unlock")]
    public bool WorldUnlock { get; set; }

    [JsonPropertyName("remote_download")]
    public bool RemoteDownload { get; set; }

    [JsonPropertyName("bg")]
    public string Bg { get; set; }

    [JsonPropertyName("date")]
    public long Date { get; set; }

    [JsonPropertyName("version")]
    public string Version { get; set; }

    [JsonPropertyName("difficulty")]
    public int Difficulty { get; set; }

    [JsonPropertyName("rating")]
    public int Rating { get; set; }

    [JsonPropertyName("note")]
    public int Note { get; set; }

    [JsonPropertyName("chart_designer")]
    public string ChartDesigner { get; set; }

    [JsonPropertyName("jacket_designer")]
    public string JacketDesigner { get; set; }

    [JsonPropertyName("jacket_override")]
    public bool JacketOverride { get; set; }

    [JsonPropertyName("audio_override")]
    public bool AudioOverride { get; set; }
}