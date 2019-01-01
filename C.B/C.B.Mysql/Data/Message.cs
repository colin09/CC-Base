using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace C.B.MySql.Data
{
    //互动留言
    public class Message : BaseEntity
    {

        [MaxLength(64)]
        public string Title { set; get; }
        [MaxLength(512)]
        public string Content { set; get; }
        [MaxLength(64)]
        public string Region { set; get; }
        [MaxLength(64)]
        public string Name { set; get; }
        [MaxLength(512)]
        public string ReplyContent { set; get; }
        [MaxLength(64)]
        public string ReplyName { set; get; }
        public DateTime? ReplyTime { set; get; }

        public int IsShow { set; get; }
        public int IsTop { set; get; }

        public double SortNo { set; get; }
    }
}




/*
DROP TABLE IF EXISTS `message`;
CREATE TABLE `message` (
  `id` int(11) NOT NULL,
  `title` varchar(45) DEFAULT NULL,
  `content` varchar(512) DEFAULT NULL,
  `region` varchar(45) DEFAULT NULL,
  `name` varchar(45) DEFAULT NULL,
  `ctime` datetime DEFAULT NULL,
  `replycontent` varchar(512) DEFAULT NULL,
  `replyname` varchar(45) DEFAULT NULL,
  `replytime` datetime DEFAULT NULL,
  `isShow` int(11) DEFAULT NULL,
  `isHot` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
 */
