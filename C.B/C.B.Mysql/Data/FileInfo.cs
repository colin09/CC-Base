using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace C.B.MySql.Data
{
    public class FileInfo : BaseEntity
    {

        [MaxLength(128)]
        public string filepath { set; get; }
        [MaxLength(64)]
        public string fileMd5 { set; get; }
        
        [MaxLength(64)]
        public string fileName { set; get; }
        [MaxLength(32)]
        public string fileType { set; get; }


    }
}




/*
DROP TABLE IF EXISTS `fileinfo`;
CREATE TABLE `fileinfo` (
  `id` int(11) NOT NULL,
  `filepath` varchar(128) DEFAULT NULL COMMENT '文件路径',
  `fileMd5` varchar(64) DEFAULT NULL COMMENT '文件md5值',
  `fileName` varchar(64) DEFAULT NULL COMMENT '文件名称',
  `fileType` varchar(64) DEFAULT NULL COMMENT '文件类型，大图；小图',
  `uploadTime` datetime DEFAULT NULL COMMENT '文件上传时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='素材表';
 */
