using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using C.B.Models.Enums;

namespace C.B.MySql.Data
{
    public class UserInfo : BaseEntity
    {

        [MaxLength(32)]
        public string UserName { set; get; }
        [MaxLength(32)]
        public string Password { set; get; }
        [MaxLength(32)]
        public string TrueName { set; get; }
        [MaxLength(32)]
        public string Department { set; get; }
        
        public string State { set; get; }

        /// <summary>
        /// 权限分类：注册用户-user，管理用户-admin，开发用户-develop
        /// </summary>
        /// <value></value>
        public UserAuthType AuthType{set;get;}



        public string MobileNo { set; get; }
        public string Email { set; get; }
        public string Gender { set; get; }

    }
}




/*
DROP TABLE IF EXISTS `userinfo`;
CREATE TABLE `userinfo` (
  `id` int(11) NOT NULL,
  `username` varchar(32) DEFAULT NULL,
  `password` varchar(32) DEFAULT NULL,
  `truename` varchar(32) DEFAULT NULL,
  `department` varchar(32) DEFAULT NULL,
  `ctime` datetime DEFAULT NULL,
  `utime` datetime DEFAULT NULL,
  `isvalid` int(4) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
 */
