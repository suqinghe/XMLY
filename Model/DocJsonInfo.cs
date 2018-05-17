using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLY
{
    public class DocJsonInfo
    {
        private string id
        {
            get;
            set;
        }

        private string play_path
        {
            get;
            set;
        }

        private string duration
        {
            get;
            set;
        }

        private string title
        {
            get;
            set;
        }

        private string album_title
        {
            get;
            set;
        }
    }

     



    public class TracksForAudioPlayItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int index { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int trackId { get; set; }
        /// <summary>
        /// 《三字经》教读01
        /// </summary>
        public string trackName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string trackUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string trackCoverPath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int albumId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sourceAlbumId { get; set; }
        /// <summary>
        /// 儿童读经 - 儿歌《三字经》
        /// </summary>
        public string albumName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string albumUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int anchorId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool canPlay { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool isPaid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int duration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string src { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool hasBuy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool albumIsSample { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sampleDuration { get; set; }
        /// <summary>
        /// 4月前
        /// </summary>
        public string updateTime { get; set; }
        /// <summary>
        /// 1年前
        /// </summary>
        public string createTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool isLike { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool isCopyright { get; set; }
    }

    public class Data
    {
        /// <summary>
        /// 
        /// </summary>
        public List<TracksForAudioPlayItem> tracksForAudioPlay { get; set; }
    }

    /// <summary>
    /// 音频信息
    /// </summary>
    public class TrackInfos
    {
        /// <summary>
        /// 
        /// </summary>
        public int ret { get; set; }
        /// <summary>
        /// 声音播放信息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Data data { get; set; }
    }
}
