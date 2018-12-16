using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace C.B.MySql.Data {

    /// <summary>
    /// 赛事内容
    /// </summary>
    public class EventInfo : BaseEntity {
        /// <summary>
        /// 赛事Id
        /// </summary>
        /// <value></value>
        public int EventId { set; get; }

        [MaxLength (64)]
        public string Title { set; get; }
        /// <summary>
        /// 内容，图文混排
        /// </summary>
        /// <value></value>
        public string Content { set; get; }

        [MaxLength (32)]
        public string Author { set; get; }

        public int IsShow { set; get; }
        public int IsTop { set; get; }

        [MaxLength (64)]
        public string ThumbUrl { set; get; }
        public int ThumbId { set; get; }

        public int SortNo { set; get; }
    }
}

/*
DROP TABLE IF EXISTS `eventinfo`;
CREATE TABLE `eventinfo` (
  `id` int(11) NOT NULL,
  `eventId` int(11) DEFAULT NULL COMMENT '赛事Id Eventid',
  `title` varchar(64) DEFAULT NULL COMMENT '标题',
  `content` text COMMENT '内容，图文混排',
  `pubTime` datetime DEFAULT NULL COMMENT '发布时间',
  `pubAuthor` varchar(45) DEFAULT NULL COMMENT '发布作者',
  `IsShow` int(11) DEFAULT NULL COMMENT '是否显示',
  `IsHot` int(11) DEFAULT NULL COMMENT '是否置顶',
  `thumbPath` varchar(45) DEFAULT NULL COMMENT '缩略图路径\n',
  `thumbpicId` int(11) DEFAULT NULL COMMENT '缩略图ID 同fileinfoID',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='赛事内容';
 */