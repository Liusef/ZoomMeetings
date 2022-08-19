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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ZoomMeetings.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class HomePage : Page
{
    public HomePage()
    {
        this.InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        bool hasMeetings = App.Meetings.Count > 0;
        NoMeetings.Visibility = !hasMeetings ? Visibility.Visible : Visibility.Collapsed;
        MeetingsView.Visibility = hasMeetings ? Visibility.Visible : Visibility.Collapsed;
    }

    private void MeetingsView_ItemClick(object sender, ItemClickEventArgs e)
    {
        App.Inst().Main.Frame.Navigate(typeof(MeetingPage), e.ClickedItem);
    }
    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        App.Inst().Main.Frame.Navigate(typeof(AddMeeting));
    }

}
