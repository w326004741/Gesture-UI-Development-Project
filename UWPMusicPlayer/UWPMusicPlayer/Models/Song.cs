using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace UWPMusicPlayer.Models
{
    // mp3 file informations
   public class Song
    {
        /// <summary>
        /// song name MP3文件名字
        /// </summary>
        private string _name;
        /// <summary>
        /// title  MP3歌曲名字 从MP3文件中读取出来的
        /// </summary>
        private string _title;
        /// <summary>
        /// artisits 艺术家
        /// </summary>
        private string _artists;
        /// <summary>
        /// ablum 唱片集
        /// </summary>
        private string _ablum;
        /// <summary>
        /// cover 封面
        /// </summary>
        private BitmapImage _thumbnail;
        /// <summary>
        /// path
        /// </summary>
        private string _path;
        /// <summary>
        /// mp3 file specific default cover MP3文件指定默认的封面
        /// </summary>
        private const string _thumbnalPath = "ms-appx:///MusicCollections/Thumbnails/MusicLogo.PNG";



        /// <summary>
        /// MP3 file name MP3文件名字
        /// </summary>
        public string Name // MP3文件的名字
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// MP3歌曲名字 从MP3文件中读取出来的，可能不存在或者为“ ”。
        /// 当_name不为空且部位“ ”时，去除_name中的后缀名，然后返回，否则返回“无名音乐
        /// ???
        /// </summary>
        public string Title 
        {
            get
            {
                if (String.IsNullOrEmpty(_title)) // If no or empty title & name, return "No Name Music"
                {
                    if (string.IsNullOrEmpty(_name))
                    {
                        return "No Name Music";
                    }
                    else // if no or empty title but have name, then return music name.
                    {
                        return _name;
                    }
                }
                else // if is not null or empty title and then return title
                {
                    return _title;
                }
            }
            set { _title = value; }
        }
        /// <summary>
        /// Artist 艺术家
        /// </summary>
        public string Artists
        {
            get { return _artists; }
            set { _artists = value;}
        }

        /// <summary>
        /// Ablum 唱片集
        /// </summary>
        public string Ablum
        {
            get { return _ablum; }
            set { _ablum = value; }
        }

        /// <summary>
        /// Thumbnail 封面缩略图
        /// </summary>
        public BitmapImage Thumbnail
        {
            get
            {
                if (_thumbnail == null) // if no have thumbnail(缩略图), 
                        //then offer inside setting PNG(MusicCollection -> Thumbnails -> No Thumbnail.PNG)提供制定内部PNG图片
                {
                    return new BitmapImage(new Uri(_thumbnalPath));
                }
                else
                {
                    return _thumbnail;
                }
            }
            set { _thumbnail = value; }
        }

        /// <summary>
        /// Path 歌曲路径
        /// </summary>
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }
    }
}
