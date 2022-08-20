using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using ZoomMeetings;
using ZoomAPI;
using System.Text.RegularExpressions;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ZoomMeetings.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class AddMeeting : Page
{

    bool linkMode = true;

    public AddMeeting()
    {
        this.InitializeComponent();
    }

    private async void AddMeetings(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Name.Text))
        {
            await App.Inst().ShowBasicDialog("Oops!", "You have to enter a name!");
            return;
        }

        string code;
        string pwd = null;

        if (linkMode)
        {
            var regex = new Regex(@"http[s*]://.*?\.*zoom\.us/j/([0-9]{10,11})(\?pwd=(\S+))?", RegexOptions.Compiled | RegexOptions.Multiline);
            var matches = regex.Matches(Link.Text);
            if (matches.Count == 0)
            {
                await App.Inst().ShowBasicDialog("Oops!", "The meeting link you entered was not valid! Make sure it's the full URL.");
                return;
            }
            code = matches[0].Groups[1].Value;
            if (matches[0].Groups.Count > 2) pwd = matches[0].Groups[3].Value;
            
        }
        else
        {
            if (string.IsNullOrWhiteSpace(Code.Text))
            {
                await App.Inst().ShowBasicDialog("Oops!", "You have to enter a code!");
                return;
            }
            if (!Meeting.ValidMeetingID(Code.Text))
            {
                await App.Inst().ShowBasicDialog("Oops!", "The meeting ID you entered was not valid!");
                return;
            }
            code = Code.Text.Replace(" ", "");
            pwd = string.IsNullOrWhiteSpace(Pwd.Text) ? null : Pwd.Text;
        }
        
        
        App.Meetings.Add(new ZoomAPI.Meeting
        {
            Name = Name.Text,
            Desc = string.IsNullOrWhiteSpace(Desc.Text) ? null : Desc.Text,
            Code = code,
            Pwd = pwd,
        });
        AppUtils.SaveMeetings();
        App.Inst().Main.Frame.Navigate(typeof(HomePage));
        
    }

    private void Switch(object sender, RoutedEventArgs e)
    {
        linkMode = !linkMode;
        AutoParse.Visibility = linkMode ? Visibility.Visible : Visibility.Collapsed;
        ManualEntry.Visibility = !linkMode ? Visibility.Visible : Visibility.Collapsed;
        SwitchModes.Content = linkMode ? "Manually Enter Details" : "Auto Parse Link";
    }
}
