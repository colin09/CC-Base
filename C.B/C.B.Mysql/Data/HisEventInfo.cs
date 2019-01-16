using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using C.B.Models.Data;

namespace C.B.MySql.Data {
    public class HisEventInfo : BaseEntity {

        [MaxLength (64)]
        public string Title { set; get; }

        //[MaxLength (2048)]
        public string Content { set; get; }
        public int Year { set; get; }

        public int PicId { set; get; }

        [MaxLength (128)]
        public int Link { set; get; }
        public int SortNo { set; get; }
    }
}

/*
DROP TABLE IF EXISTS `hiseventinfo`;
CREATE TABLE `hiseventinfo` (
  `id` int(11) NOT NULL,
  `year` int(11) DEFAULT NULL,
  `eventName` varchar(45) DEFAULT NULL,
  `eventContent` varchar(512) DEFAULT NULL,
  `picId` int(11) DEFAULT NULL,
  `enentLink` varchar(128) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
 */