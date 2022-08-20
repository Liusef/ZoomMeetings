using System.Text.RegularExpressions;

namespace ZoomAPI;

public class Meeting
{
    public string Name { get; set; }
    public string? Desc { get; set; }
    public string Code { get; set; }
    public string? Pwd { get; set; }

    public bool HasDesc => !string.IsNullOrWhiteSpace(Desc);
    public bool HasPwd => !string.IsNullOrWhiteSpace(Pwd);
    public string FormattedCode => $"{Code[..3]} {Code[3..(Code.Length-4)]} {Code[^4..]}";
    public Uri LaunchUri => new($"zoommtg://zoom.us/join?" +
                                $"confno={Code}" +
                                (HasPwd ? $"&pwd={Pwd}" : String.Empty));
    public Uri ShareUri => new($"https://zoom.us/j/{Code}" +
                                (HasPwd ? $"?pwd={Pwd}" : String.Empty));


    public override string ToString()
    {
        return $"Meeting Details for {Name}\n" +
               (HasDesc ? $"{Desc}\n" : string.Empty) + 
               $"\n" +
               $"Meeting Code: {FormattedCode}\n" +
               (HasPwd ? $"Meeting Password: {Pwd}\n" : string.Empty) +
               $"{ShareUri}";
    }


    public static bool ValidMeetingID(string ID)
    {
        var s = Regex.Replace(ID, "[^0-9]", "");
        if (s.Length != 10 && s.Length != 11) return false;
        foreach (var c in s)
        {
            if (c < '0' || c > '9') return false;
        }
        return true;
    }
}


