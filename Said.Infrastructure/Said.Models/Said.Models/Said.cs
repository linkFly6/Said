﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Said.Models
{
    /// <summary>
    /// 听说（文章）
    /// </summary>
    public class Said : BaseModel
    {
        /// <summary>
        /// Said Id
        /// </summary>
        public string SaidId { get; set; }
        /// <summary>
        /// XML
        /// </summary>
        public string SXML { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string STitle { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string STag { get; set; }
        /// <summary>
        /// 修剪后的描述
        /// </summary>
        public string SSummaryTrim { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string SSummary { get; set; }
        /// <summary>
        /// 歌曲ID
        /// </summary>
        public int SSongId { get; set; }
        /// <summary>
        /// 脚本（如果有的话）
        /// </summary>
        public string SScript { get; set; }
        /// <summary>
        /// 是否转载（0:否 1：是）
        /// </summary>
        public int SReprint { get; set; }
        /// <summary>
        /// 浏览量
        /// </summary>
        public int SPV { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string SName { get; set; }
        /// <summary>
        /// 最后一次评论的用户名
        /// </summary>
        public string SLastCommentUser { get; set; }
        /// <summary>
        /// 最有一次评论的内容
        /// </summary>
        public string SLastComment { get; set; }
        /// <summary>
        /// 是否置顶（0：否 1：是 2：所有类别置顶）
        /// </summary>
        public int SIsTop { get; set; }
        /// <summary>
        /// 缩略图（裁剪过后缩放的）
        /// </summary>
        public string SImgTrim { get; set; }
        /// <summary>
        /// 缩略图（大图）
        /// </summary>
        public string SImg { get; set; }
        /// <summary>
        /// 文章HTML
        /// </summary>
        public string SHTML { get; set; }
        /// <summary>
        /// 文章发表时间
        /// </summary>
        public DateTime SDate { get; set; }
        /// <summary>
        /// CSS（如果有的话）
        /// </summary>
        public string SCSS { get; set; }
        /// <summary>
        /// 源码（markdown）
        /// </summary>
        public string SContext { get; set; }
        /// <summary>
        /// 评论量
        /// </summary>
        public int SComment { get; set; }
        /// <summary>
        /// 点击量
        /// </summary>
        public int SClick { get; set; }
        /// <summary>
        /// 类型ID（如果有的话）
        /// </summary>
        public int SClassifyId { get; set; }
    }
}