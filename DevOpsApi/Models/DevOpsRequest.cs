namespace DevOpsApi.Models;

public class DevOpsRequest
{
    public string Message { get; set; } = "";
    public string To { get; set; } = "";
    public string From { get; set; } = "";
    public int TimeToLifeSec { get; set; }
}
