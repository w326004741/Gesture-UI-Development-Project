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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPMusicPlayer.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MyMusicPage : Page
    {

        private IList<Song> songs;
        public static MyMusicPage Current;

        public MyMusicPage()
        {
            this.InitializeComponent();
            Current = this;
        }

        /// <summary>
        /// OnNavigatedTo ： 为ListView控件绑定数据
        /// 重载MyMusicPage的OnNavigatedTo方法，将SongManager中读取的歌曲集合赋值给ListView的ItemSource就可以了
        /// songs：是存放Song类型的IList集合，为MyMusicPage类的一个私有字段，外界可以通过Songs属性访问，但是不可以重新赋值，
        /// 这样做是为了便于与其他页面交换歌曲输出，而不是再次从安装包中读取。
        /// </summary>
        /// <param name="e"></param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            this.Loading.IsActive = true;
            songs = await SongManager.BuildSongAsync();
            LstViewPlayList.ItemsSource = songs;
            this.Loading.IsActive = false;
            // base.OnNavigatedTo(e);
        }

        private void LstViewPlayList_ItemClick(object sender, ItemClickEventArgs e)
        {
            SongPlayPage.Current.PlaySong(e.ClickedItem as Song);
            
        }

        public IList<Song> Songs
        {
            get { return songs; }
        }
    }
}
