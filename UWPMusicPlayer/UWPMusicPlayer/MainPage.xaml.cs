using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWPMusicPlayer.Models;
using UWPMusicPlayer.Pages;
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

        /// <summary>
        ///  Use data binding to bind the menu to the panel 使用数据绑定将菜单绑定到面板上
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // NavControl,  FootControl in ItemsBuilder.cs
            this.NavControl.ItemsSource = ItemsBuilder.BuildNavItems();
            this.FootControl.ItemsSource = ItemsBuilder.BuildFootItems();
            this.MasterFrame.Navigate(typeof(MyMusicPage));
            this.MusicPlayerFrame.Navigate(typeof(SongPlayPage));
        }

        /// <summary>
        ///   Hamburger button click event... 汉堡按钮的点击事件。 Toggle 切换 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleButton_Click(Object sender, RoutedEventArgs e)
        {
            this.MasterSplitView.IsPaneOpen = !this.MasterSplitView.IsPaneOpen;
        }

        /// <summary>
        /// Menu Item Click Event 菜单项目点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavControl_ItemClick(object sender, ItemClickEventArgs e)
        {
            NavItem item = e.ClickedItem as NavItem;
            if (item.View != null)
            {
                this.MasterFrame.Navigate(item.View, item.Title);
            }
        }
        /// <summary>
        /// Menu Foot Item Click Event 菜单底部项目点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FootControl_ItemClick(object sender, ItemClickEventArgs e)
        {
            NavItem item = e.ClickedItem as NavItem;
            if (item.View != null)
            {
                this.MasterFrame.Navigate(item.View, item.Title);
            }
        }
    }
}
