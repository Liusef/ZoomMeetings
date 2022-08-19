using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using ZoomAPI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ZoomMeetings.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MeetingPage : Page
{

    public Meeting curr { get; set; }

    public MeetingPage()
    {
        this.InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        curr = e.Parameter as Meeting;
        base.OnNavigatedTo(e);
    }

    private async void Join(object sender, RoutedEventArgs e) => 
        await Windows.System.Launcher.LaunchUriAsync(curr.LaunchUri);
    

    private void CopyLink(object sender, RoutedEventArgs e) =>
        AppUtils.AddToClipboard(curr.ShareUri.ToString());

    private void CopyDetails(object sender, RoutedEventArgs e) =>
        AppUtils.AddToClipboard(curr.ToString());

    private void Delete(object sender, RoutedEventArgs e)
    {
        App.Meetings.Remove(curr);
        AppUtils.SaveMeetings();
        App.Inst().Main.Frame.Navigate(typeof(HomePage));
    }
    private async void Edit(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Name.Text))
        {
            await App.Inst().ShowBasicDialog("Oops!", "You have to enter a name!");
            return;
        }
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


        curr.Name = Name.Text;
        curr.Desc = string.IsNullOrWhiteSpace(Desc.Text) ? null : Desc.Text;
        curr.Code = Code.Text.Replace(" ", "");
        curr.Pwd = string.IsNullOrWhiteSpace(Pwd.Text) ? null : Pwd.Text;

        AppUtils.SaveMeetings();
        App.Inst().Main.Frame.Navigate(typeof(HomePage));
    }
}
