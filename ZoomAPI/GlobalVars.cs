using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoomAPI;

public class GlobalVars
{
    public static readonly char Sep = Path.DirectorySeparatorChar;

    public static readonly string DataFolder = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}{Sep}ZoomMeetings{Sep}";

    public const string MeetingsFilename = "meetings.json";

    public static void EnsureDataDir()
    {
        Utils.EnsurePath(DataFolder);
    }

}
