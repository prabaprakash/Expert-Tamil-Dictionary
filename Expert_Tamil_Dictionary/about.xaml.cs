using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Navigation;
namespace Expert_Tamil_Dictionary
{
    public partial class WindowsPhoneControl1 : UserControl
    {
        public WindowsPhoneControl1()
        {
            InitializeComponent();
        }
         private async void Button_Click_1(object sender, RoutedEventArgs e)
    {
        string uriToLaunch = @"https://twitter.com/@Prabakaran1993";

        // Create a Uri object from a URI string 
        var uri = new Uri(uriToLaunch);
        var success = await Windows.System.Launcher.LaunchUriAsync(uri);

    }

    private async void Button_Click_2(object sender, RoutedEventArgs e)
    {
        string uriToLaunch = @"http://www.facebook.com/prabakaran1993";

        // Create a Uri object from a URI string 
        var uri = new Uri(uriToLaunch);
        var success = await Windows.System.Launcher.LaunchUriAsync(uri);
    }

    private async void Button_Click_3(object sender, RoutedEventArgs e)
    {
        string uriToLaunch = @"http://www.linkedin.com/pub/praba-prakash/48/35a/6a8";

        // Create a Uri object from a URI string 
        var uri = new Uri(uriToLaunch);
        var success = await Windows.System.Launcher.LaunchUriAsync(uri);
    }

    private async void Button_Click_4(object sender, RoutedEventArgs e)
    {
        string uriToLaunch = @"https://plus.google.com/113387907629278627187";

        // Create a Uri object from a URI string 
        var uri = new Uri(uriToLaunch);
        var success = await Windows.System.Launcher.LaunchUriAsync(uri);
    

    }

  
    }
}
