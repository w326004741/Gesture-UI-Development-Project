using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Myo;
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
using System.Diagnostics;
using Windows.UI.Core;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPMusicPlayer.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SongPlayPage : Page, INotifyPropertyChanged
    {
        private readonly global::Myo.Myo _myo;

        public static SongPlayPage Current;
        public Song currentSong; // myo control media play

        public event PropertyChangedEventHandler PropertyChanged;

     

        public SongPlayPage()
        {
            this.InitializeComponent();
            Current = this;
            this.SizeChanged += SongPlayPage_SizeChanged;
            _myo = new global::Myo.Myo();
            _myo.Connect();

            _myo.OnPoseDetected += _myo_OnPoseDetected;
            _myo.DataAvailable += _myo_DataAvailable;
            _myo.OnError += _myo_OnError;

        }

        /// <summary>
        /// Myo Error
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _myo_OnError(object sender, MyoErrorEventArgs e)
        {
            Debug.WriteLine("Problem connecting to Myo." + Environment.NewLine + e.Message);
        }

        private void _myo_DataAvailable(object sender, MyoDataEventArgs e)
        {
            
        }
        /// <summary>
        /// Myo control  play music, Pose Type is Double Tap
        /// </summary>
        private async void playMusic()
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                {
                    this.MediaPlayer.Play();
                    //this.MediaPlayer.Pause();
                    this.BttnPlay.Visibility = Visibility.Visible;
                    this.BttnStop.Visibility = Visibility.Collapsed;
                });
        }
        /// <summary>
        /// Myo control  stop music, Pose Type is FingersSpread.
        /// </summary>
        private async void stopMusic()
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                {
                    this.MediaPlayer.Stop();
                    this.BttnPlay.Visibility = Visibility.Collapsed;
                    this.BttnStop.Visibility = Visibility.Visible;
                });
        }
        /// <summary>
        /// Myo control play previous music, Pose Type is Wave In
        /// </summary>
        private async void playPrevious()
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                {
                    if (IsSongSelected())
                    {
                        GetPreviousSong();
                        PlaySongInternal();
                    }
                    this.BttnPlay.Visibility = Visibility.Visible;
                    this.BttnStop.Visibility = Visibility.Collapsed;
                });
        }
        /// <summary>
        /// Myo control play Next music, Pose Type is Wave Out.
        /// </summary>
        private async void playNext()
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                {
                    if (IsSongSelected())
                    {
                        GetNextSong();
                        PlaySongInternal();
                    }
                    this.BttnPlay.Visibility = Visibility.Visible;
                    this.BttnStop.Visibility = Visibility.Collapsed;
                });
            
        }
        /// <summary>
        /// Myo control pause music, Pose Type is Fist
        /// </summary>
        private async void pauseMusic()
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                {
                    this.MediaPlayer.Pause();
                    this.BttnPlay.Visibility = Visibility.Collapsed;
                    this.BttnStop.Visibility = Visibility.Visible;
                });
        }

        /// <summary>
        /// Myo Pose Detected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _myo_OnPoseDetected(object sender, MyoPoseEventArgs e)
        {
            if (e.Pose == MyoPoseEventArgs.PoseType.DoubleTap) // Double Tap = Play Music
            {
                playMusic();
               
            }
            if (e.Pose == MyoPoseEventArgs.PoseType.FingersSpread) // Fingers Spread = Stop Music
            {
                stopMusic();
            }
            if (e.Pose == MyoPoseEventArgs.PoseType.WaveIn) // Wave In (Left) = Previous
            {
                playPrevious();
            }
            if (e.Pose == MyoPoseEventArgs.PoseType.WaveOut) // Wave Out (Right) = Next
            {
                playNext();
            }
            if (e.Pose == MyoPoseEventArgs.PoseType.Fist) // Fist Roll = Volume down & up
            {
                pauseMusic();
            }

            switch (e.Pose)
            {
                case MyoPoseEventArgs.PoseType.Rest:
                    Debug.WriteLine("Rest");
                    break;
                case MyoPoseEventArgs.PoseType.Fist:
                    Debug.WriteLine("Fist");
                    break;
                case MyoPoseEventArgs.PoseType.WaveIn:
                    Debug.WriteLine("WaveIn");
                    break;
                case MyoPoseEventArgs.PoseType.WaveOut:
                    Debug.WriteLine("WaveOut");
                    break;
                case MyoPoseEventArgs.PoseType.DoubleTap:
                    Debug.WriteLine("DoubleTap");
                    break;
                case MyoPoseEventArgs.PoseType.FingersSpread:
                    Debug.WriteLine("FingersSpread");
                    break;
                default:
                    break;
            }
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
        /// Volume
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
        /// 如果播放歌曲为空
        ///  If Play Song is null
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
        /// Play Song Internal, Setting song Path
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
        /// Play Song Internal, Current Song
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
        /// 停止歌曲内部 暂停 
        ///  Stop Song Internal, Pause
        /// </summary>
        private void StopSongInternal()
        {
            this.MediaPlayer.Pause();
        }

        /// <summary>
        /// Button Play Click Event
        /// 播放按钮点击事件
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
        /// Button Stop Click Event
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
        /// Button Previous Click Event
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
        /// Button Next Click Event
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
        ///  Song Selected
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
