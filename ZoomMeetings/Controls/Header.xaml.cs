using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using ZoomMeetings.Pages;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ZoomMeetings.Controls;

[ContentProperty(Name = nameof(PageContent))]
public sealed partial class Header : UserControl
{

    public string Title { get; set; }

    public Header()
    {
        this.InitializeComponent();
    }

    public object PageContent
    {
        get { return (object)GetValue(PageContentProperty); }
        set { SetValue(PageContentProperty, value); }
    }

    // Using a DependencyProperty as the backing store for PageContent.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty PageContentProperty =
        DependencyProperty.Register("PageContent", typeof(object), typeof(Header), new PropertyMetadata(new Grid()));
}
