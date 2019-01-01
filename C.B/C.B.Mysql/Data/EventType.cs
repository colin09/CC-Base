using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace C.B.MySql.Data {

    /// <summary>
    /// 赛事分类
    /// </summary>
    public class EventType: BaseEntity {
        /// <summary>
        /// 赛事Id
        /// </summary>
        /// <value></value>
        public int ParentId { set; get; }

        [MaxLength (64)]
        public string Name { set; get; }

        public int Level { set; get; }

        public int IsShow { set; get; }

        [MaxLength (128)]
        public string Icon { set; get; }
        public double SortNo { set; get; }
    }
}

/*
DROP TABLE IF EXISTS `event`;
CREATE TABLE `event` (
  `id` int(11) NOT NULL,
  `eventName` varchar(45) DEFAULT NULL COMMENT '名称',
  `level` int(2) DEFAULT NULL COMMENT '级别',
  `isShow` int(2) DEFAULT NULL COMMENT '是否显示',
  `parentId` int(2) DEFAULT NULL COMMENT '父栏目对应本表ID',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='赛事分类表';
 */