using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml.Media.Imaging;

namespace UWPMusicPlayer.Models
{
    /// <summary>
    /// SongManager 是一个静态工具类，有两个静态私有的字段：
    /// _songs: IList类型集合，存在Song类型的对象。
    /// _thumbnail: BitmapImage类型，MP3歌曲制定默认的封面。
    /// </summary>
    public static class SongManager
    {
        private static IList<Song> _songs;
        private static BitmapImage _thumbnail;

        // InitSongAsyc 将获得到的MP3文件初始化成我们所需要的Song的类型对象。
        // MusicProperties:  using Windows.Storage.FileProperties;
        private static async Task<Song> InitSongAsyc(StorageFile songFile)
        {
            Song song = new Song();
            song.Name = songFile.DisplayName;
            song.Path = songFile.Path;
            MusicProperties musicProperties = await songFile.Properties.GetMusicPropertiesAsync();
            song.Title = musicProperties.Title;
            song.Ablum = musicProperties.Album;
            song.Artists = musicProperties.Artist;
            return song;
        }

        // BuildSongAsync 该方法用于读取安装包下的MusicCollections文件夹下的MP3文件
        public static async Task<IList<Song>> BuildSongAsync()
        {
            if (_songs == null || _songs.Count <= 0)
            {
                _songs = new List<Song>();
                StorageFolder localFolder = Package.Current.InstalledLocation;
                StorageFolder musicFolder = await localFolder.GetFolderAsync("MusicCollections");
                foreach (StorageFile file in await musicFolder.GetFilesAsync())
                {
                    _songs.Add(await InitSongAsyc(file));
                }
            }
            return _songs;
        }
        // GetDefaultThumbnail  use to got default cover of mp3 file.
        // 该方法用于获取默认的MP3文件的封面
        //  StorageFolder & StorageFile: using Windows.Storage;
        public static async Task<BitmapImage> GetDefaultThumbnailAsync()
        {
            if (_thumbnail == null)   // 如果thumbnail缩略图为空，则new一个新的从BitmapImage().
            {
                _thumbnail = new BitmapImage();
                StorageFolder localFolder = Package.Current.InstalledLocation; 
                StorageFolder musicFolder = await localFolder.GetFolderAsync("MusicCollections");
                StorageFolder thumbnailFolder = await musicFolder.GetFolderAsync("Thumbnails");
                StorageFile thumbnailFile = await thumbnailFolder.GetFileAsync("MusicLogo.PNG");
                _thumbnail.UriSource = new Uri(thumbnailFile.Path, UriKind.Absolute);
            }
            return _thumbnail;
        }
    }
}
