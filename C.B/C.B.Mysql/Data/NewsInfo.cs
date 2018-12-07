﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace C.B.MySql.Data
{
    public class NewsInfo : BaseEntity
    {

        [MaxLength(64)]
        public string Title { set; get; }
        [MaxLength(2048)]
        public string Content { set; get; }
        public DateTime PubTime { set; get; }
        [MaxLength(64)]
        public string PubOrg { set; get; }
        [MaxLength(32)]
        public string Author { set; get; }

        public int IsShow{set;get;}
        public int IsTop{set;get;}
        public int IsRoll{set;get;}
        
        public int ThumbId{set;get;}
        public int NewsType{set;get;}
        public int FileId{set;get;}


        public int SortNo { set; get; }
    }
}




/*
DROP TABLE IF EXISTS `newsinfo`;
CREATE TABLE `newsinfo` (
  `id` int(11) NOT NULL,
  `title` varchar(48) DEFAULT NULL COMMENT '标题',
  `content` text COMMENT '内容',
  `pubtime` datetime DEFAULT NULL COMMENT '发布时间',
  `updatetime` datetime DEFAULT NULL COMMENT '更新时间',
  `author` varchar(24) DEFAULT NULL COMMENT '作者',
  `isShow` int(11) DEFAULT NULL COMMENT '是否显示',
  `isTop` int(11) DEFAULT NULL COMMENT '是否置顶',
  `isRoll` int(11) DEFAULT NULL COMMENT '是否滚动',
  `thumbId` int(11) DEFAULT NULL COMMENT '缩略图ID',
  `newstype` int(11) DEFAULT NULL COMMENT '图片新闻\n赛事新闻\n视频新闻\n',
  `fileId` int(11) DEFAULT NULL COMMENT '视频文件id',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='比赛新闻表';
 */
