using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWPMusicPlayer.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPMusicPlayer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        // Use data binding to bind the menu to the panel
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // NavControl,  FootControl in ItemsBuilder.cs
            this.NavControl.ItemsSource = ItemsBuilder.BuildNavItems();
            this.FootControl.ItemsSource = ItemsBuilder.BuildFootItems();
            
            //base.OnNavigatedTo(e);
        }

        // Hamburger button click event...  Toggle 切换
        private void ToggleButton_Click(Object sender, RoutedEventArgs e)
        {
            this.MasterSplitView.IsPaneOpen = !this.MasterSplitView.IsPaneOpen;
        }

        private void NavControl_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void FootControl_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}
