using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace C.B.MySql.Data
{
    public class Notice : BaseEntity
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
        
        public double SortNo { set; get; }
    }
}




/*
DROP TABLE IF EXISTS `notice`;
CREATE TABLE `notice` (
  `id` int(11) NOT NULL,
  `title` varchar(45) DEFAULT NULL,
  `content` varchar(2048) DEFAULT NULL,
  `pubtime` datetime DEFAULT NULL,
  `puborg` varchar(45) DEFAULT NULL,
  `author` varchar(45) DEFAULT NULL,
  `utime` datetime DEFAULT NULL,
  `isShow` int(11) DEFAULT NULL,
  `isTop` int(11) DEFAULT NULL,
  `isRoll` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
 */
