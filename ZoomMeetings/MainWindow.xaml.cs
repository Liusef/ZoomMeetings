using Microsoft.UI;
using Microsoft.UI.Windowing;
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

namespace ZoomMeetings;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{

    public Frame Frame => MainFrame;

    public MainWindow()
    {
        this.InitializeComponent();
        Title = "Zoom Meetings";
        ExtendsContentIntoTitleBar = true;
        SetTitleBar(TitleBar);
        SetWindowIcon("Assets/Icon.ico");
        Closed += (object sender, WindowEventArgs args) =>
        {
            AppUtils.SaveMeetings();
        };
    }

    /// <summary>
    /// Sets the window icon to a string path. Must be an ICO file
    /// </summary>
    /// <param name="location"></param>
    private void SetWindowIcon(string location)
    {
        IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
        WindowId myWndId = Win32Interop.GetWindowIdFromWindow(hWnd);
        var appWindow = AppWindow.GetFromWindowId(myWndId); // Get AppWindow obj of this

        appWindow.SetIcon(location); // And then set the icon
    }

}
