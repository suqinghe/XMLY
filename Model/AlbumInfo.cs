using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XMLY.Model
{
    public class MetasItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int metaValueId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int metaDataId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int categoryId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isSubCategory { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string categoryName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string categoryPinyin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string metaValueCode { get; set; }
        /// <summary>
        /// 3-6岁
        /// </summary>
        public string metaDisplayName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string link { get; set; }
    }

    public class MainInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int albumStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool showApplyFinishBtn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool showEditBtn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool showTrackManagerBtn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool showInformBtn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cover { get; set; }
        /// <summary>
        /// 十万个为什么[446集全 儿童百科]
        /// </summary>
        public string albumTitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string categoryPinyin { get; set; }
        /// <summary>
        /// 儿童
        /// </summary>
        public string categoryTitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string subcategoryCode { get; set; }
        /// <summary>
        /// 科普
        /// </summary>
        public string subcategoryTitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string updateDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int playCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool isPaid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int isFinished { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<MetasItem> metas { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool isSubscribe { get; set; }
        /// <summary>
        /// 微信公众号：一叶叶窗。 
        public string richIntro { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public string detailRichIntro { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool isPublic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool hasBuy { get; set; }
    }

    public class AnchorAlbumListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int albumId { get; set; }
        /// <summary>
        /// 林汉达写给儿童的中国历史故事集
        /// </summary>
        public string albumTitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cover { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int playCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int anchorId { get; set; }
        /// <summary>
        /// 一叶叶窗
        /// </summary>
        public string anchorName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
    }

    public class AnchorInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int anchorId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string anchorCover { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string showFollowBtn { get; set; }
        /// <summary>
        /// 一叶叶窗
        /// </summary>
        public string anchorName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int anchorGrade { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int anchorGradeType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int anchorAlbumsCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int anchorTracksCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int anchorFollowsCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int anchorFansCount { get; set; }
        /// <summary>
        /// 儿童
        /// </summary>
        public string personalIntroduction { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string showAnchorAlbumModel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<AnchorAlbumListItem> anchorAlbumList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hasMoreBtn { get; set; }
    }

    public class TracksItem
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
        /// 
        /// </summary>
        public string isPaid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int tag { get; set; }
        /// <summary>
        /// 十万个为什么001 太阳上有什么
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int playCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string showLikeBtn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isLike { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string showShareBtn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string showCommentBtn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string showForwardBtn { get; set; }
        /// <summary>
        /// 1年前
        /// </summary>
        public string createDateFormat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
    }

    public class TracksInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int trackTotalCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sort { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //public List<TracksItem> tracks { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int pageNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int pageSize { get; set; }
    }

    public class RecommendInfoItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string cover { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int albumId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int anchorId { get; set; }
        /// <summary>
        /// 西瓜老鼠橙子猫
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 司南时代
        /// </summary>
        public string anchorName { get; set; }
    }

    public class Data
    {
        /// <summary>
        /// 
        /// </summary>
        public bool isSelfAlbum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int currentUid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int albumId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public MainInfo mainInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //public AnchorInfo anchorInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public TracksInfo tracksInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //public List<RecommendInfoItem> recommendInfo { get; set; }
    }
    /// <summary>
    /// 专辑信息
    /// </summary>
    public class AlbumInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int ret { get; set; }
        /// <summary>
        /// 成功
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Data data { get; set; }
    }





   

    public class PageAlbumData
    {
        /// <summary>
        /// 
        /// </summary>
        public int currentUid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int albumId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int trackTotalCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sort { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<TracksItem> tracks { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int pageNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int pageSize { get; set; }
    }

    public class PageAlbumInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int ret { get; set; }
        /// <summary>
        /// 成功
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public PageAlbumData data { get; set; }
    }
}
