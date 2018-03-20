using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWPMusicPlayer.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPMusicPlayer.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SongPlayPage : Page
    {

        public static SongPlayPage Current;
        public Song currentSong;
        //public Song _artists;

        public SongPlayPage()
        {
            this.InitializeComponent();
            Current = this;
            this.SizeChanged += SongPlayPage_SizeChanged;

        }

        private void SongPlayPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.MediaPlayer.Width = e.NewSize.Width - 12;
            this.MediaPlayer.Height = this.MediaGrid.ActualHeight;
            System.Diagnostics.Debug.WriteLine("this.MediaGrid.ActualHeight : {0}", this.MediaGrid.ActualHeight);
            this.PlayerPostion.Width = e.NewSize.Width - 12;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            this.BttnPlay.Visibility = Visibility.Collapsed;
            this.MediaPlayer.PosterSource = await SongManager.GetDefaultThumbnailAsync();
        }

        /// <summary>
        /// 音量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (element != null)
            {
                FlyoutBase.ShowAttachedFlyout(element);
            }
        }

        /// <summary>
        /// 播放歌曲为空时，
        /// </summary>
        /// <param name="song"></param>
        public void PlaySong(Song song = null)
        {
            if (song != null)
            {
                currentSong = song;
                PlaySongInternal(currentSong);
                //PlaySongInternal(_artists);
                this.BttnPlay.Visibility = Visibility.Visible;
                this.BttnStop.Visibility = Visibility.Collapsed;
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// 播放歌曲内部，设置歌曲URI路径
        /// </summary>
        /// <param name="song"></param>
        private void PlaySongInternal(Song song)
        {
            this.MediaPlayer.Source = new Uri(song.Path, UriKind.Absolute);
            if (song.Thumbnail == null)
            {
                this.MediaPlayer.PosterSource = new BitmapImage(new Uri("ms-appx:///MusicCollections/Thumbnails/MusicLogo.PNG"));
            }
            else
            {
                this.MediaPlayer.PosterSource = song.Thumbnail;
            }
            this.MediaPlayer.Play();
        }

        /// <summary>
        /// 播放歌曲内部 当前歌曲
        /// </summary>
        private void PlaySongInternal()
        {
            if (IsSongSelected())
            {
                PlaySongInternal(currentSong);
                //PlaySongInternal(_artists);
            }
        }


        /// <summary>
        /// 停止歌曲内部 暂停 Pause
        /// </summary>
        private void StopSongInternal()
        {
            this.MediaPlayer.Pause();
        }

        /// <summary>
        /// Button Play Click Event 播放按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BttnPlay_Click(object sender, RoutedEventArgs e)
        {
            StopSongInternal();
            this.BttnPlay.Visibility = Visibility.Collapsed;
            this.BttnStop.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 播放停止点击事件 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BttnStop_Click(object sender, RoutedEventArgs e)
        {
            if (IsSongSelected())
            {
                PlaySongInternal(currentSong);
                this.BttnPlay.Visibility = Visibility.Visible;
                this.BttnStop.Visibility = Visibility.Collapsed;
            }
            else
            {
                ShowErorAsync("Select Song Please");
            }
        }

        /// <summary>
        /// 上一首点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BttnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (IsSongSelected())
            {
                GetPreviousSong();
                PlaySongInternal();
            }
        }
        /// <summary>
        /// 下一首点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BttnNext_Click(object sender, RoutedEventArgs e)
        {
            if (IsSongSelected())
            {
                GetNextSong();
                PlaySongInternal();
            }
        }


       
        private void MediaPlayer_MediaOpened(object sender, RoutedEventArgs e)
        {
            double songTimes = this.MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;//
            this.PlayerPostion.Maximum = songTimes;
        }

        private void MediaPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (this.SwtchMediaRepeat.IsOn)
            {
                GetNextSong();
            }
            PlaySongInternal(currentSong);
        }

        /// <summary>
        /// 下一首 BttnNext_Click
        /// </summary>
        private void GetNextSong()
        {
            IList<Song> songs = MyMusicPage.Current.Songs;
            int index = songs.IndexOf(currentSong) + 1;
            if (index == songs.Count)
            {
                index = 0;
            }
            currentSong = songs[index];
        }

        /// <summary>
        /// 上一首， BttnPrevious_Click Event
        /// </summary>
        private void GetPreviousSong()
        {
            IList<Song> songs = MyMusicPage.Current.Songs;
            int index = songs.IndexOf(currentSong) - 1;
            if (index == -1)
            {
                index = songs.Count - 1;
            }
            currentSong = songs[index];
        }

        /// <summary>
        /// Slider Value 音量条
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SldrVolume_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            this.MediaPlayer.Volume = this.SldrVolume.Value / 100.0;
        }

        /// <summary>
        /// show Error Message
        /// </summary>
        /// <param name="message"></param>
        private async void ShowErorAsync(string message)
        {
            MessageDialog md = new MessageDialog(message);
            await md.ShowAsync();
        }

        /// <summary>
        ///  歌曲选择
        /// </summary>
        /// <returns></returns>
        private bool IsSongSelected()
        {
            if (currentSong == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
