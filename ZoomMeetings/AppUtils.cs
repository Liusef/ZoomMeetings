using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using ZoomAPI;
using ZoomMeetings.Pages;

namespace ZoomMeetings;

public static class AppUtils
{
    public static ObservableCollection<Meeting> GetMeetings()
    {
        try
        {
            return new ObservableCollection<Meeting>
                (Utils.Deserialize<Meeting[]>($"{GlobalVars.DataFolder}{GlobalVars.MeetingsFilename}"));
        } 
        catch
        {
            return new ObservableCollection<Meeting>();
        }
    }

    public static void SaveMeetings()
    {
        Utils.Serialize<IEnumerable<Meeting>>(App.Meetings, $"{GlobalVars.DataFolder}{GlobalVars.MeetingsFilename}");
    }

    /// <summary>
    /// Display a ContentDialog with specified text. Will not open if a dialog box is already open
    /// </summary>
    /// <param name="app">The current App instance</param>
    /// <param name="title">The title of the dialog box</param>
    /// <param name="body">The body of the dialog box</param>
    /// <param name="closeText">The text to display on the button that closes the box</param>
    /// <returns>An awaitable task</returns>
    public static async Task ShowBasicDialog(this App app, string title, string body, string closeText = "OK")
    {
        if (app.IsCdOpen) return;
        app.IsCdOpen = true;

        var b = new DialogBody();
        b.Body.Text = body;

        var cd = new ContentDialog
        {
            Title = title,
            IsPrimaryButtonEnabled = false,
            IsSecondaryButtonEnabled = false,
            CloseButtonText = closeText,
            Content = b,
            XamlRoot = app.Main.Content.XamlRoot
        };

        cd.CloseButtonClick += (sender, args) => app.IsCdOpen = false;

        await cd.ShowAsync();
    }

    /// <summary>
    /// Add a string to the user's clipboard
    /// </summary>
    /// <param name="content">The content to be added to the clipboard</param>
    public static void AddToClipboard(string content)
    {
        var dp = new DataPackage();
        dp.SetText(content);
        Clipboard.SetContent(dp);
    }
}
