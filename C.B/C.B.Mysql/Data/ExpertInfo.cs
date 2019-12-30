using System.ComponentModel.DataAnnotations;

namespace C.B.MySql.Data {
  //专家简介
  public class ExpertInfo : BaseEntity {

    [MaxLength (64)]
    public string Title { set; get; }

    [MaxLength (512)]
    public string Content { set; get; }
    public int Type { set; get; }

    public int PicFileId { set; get; }

    [MaxLength (128)]
    public string PicUrl { set; get; }

    [MaxLength (32)]
    public string Author { set; get; }
    public int IsShow { set; get; }
    public double SortNo { set; get; }

    public int DocumentId { get; set; }

  }
}

/*
DROP TABLE IF EXISTS `expertinfo`;
CREATE TABLE `expertinfo` (
  `id` int(11) NOT NULL,
  `name` varchar(45) DEFAULT NULL,
  `insContent` varchar(512) DEFAULT NULL,
  `picFileId` int(11) DEFAULT NULL,
  `pubTime` datetime DEFAULT NULL,
  `authur` varchar(45) DEFAULT NULL,
  `isShow` int(11) DEFAULT NULL,
  `showSeq` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
 */