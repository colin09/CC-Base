CREATE DATABASE  IF NOT EXISTS `kjgweb-db` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `kjgweb-db`;
-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: localhost    Database: kjgweb-db
-- ------------------------------------------------------
-- Server version	5.7.20-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `event`
--

DROP TABLE IF EXISTS `event`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `event` (
  `id` int(11) NOT NULL,
  `eventName` varchar(45) DEFAULT NULL COMMENT '名称',
  `level` int(2) DEFAULT NULL COMMENT '级别',
  `isShow` int(2) DEFAULT NULL COMMENT '是否显示',
  `parentId` int(2) DEFAULT NULL COMMENT '父栏目对应本表ID',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='赛事分类表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `event`
--

LOCK TABLES `event` WRITE;
/*!40000 ALTER TABLE `event` DISABLE KEYS */;
/*!40000 ALTER TABLE `event` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `eventinfo`
--

DROP TABLE IF EXISTS `eventinfo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
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
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `eventinfo`
--

LOCK TABLES `eventinfo` WRITE;
/*!40000 ALTER TABLE `eventinfo` DISABLE KEYS */;
/*!40000 ALTER TABLE `eventinfo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `expertinfo`
--

DROP TABLE IF EXISTS `expertinfo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
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
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `expertinfo`
--

LOCK TABLES `expertinfo` WRITE;
/*!40000 ALTER TABLE `expertinfo` DISABLE KEYS */;
/*!40000 ALTER TABLE `expertinfo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fileinfo`
--

DROP TABLE IF EXISTS `fileinfo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fileinfo` (
  `id` int(11) NOT NULL,
  `filepath` varchar(128) DEFAULT NULL COMMENT '文件路径',
  `fileMd5` varchar(64) DEFAULT NULL COMMENT '文件md5值',
  `fileName` varchar(64) DEFAULT NULL COMMENT '文件名称',
  `fileType` varchar(64) DEFAULT NULL COMMENT '文件类型，大图；小图',
  `uploadTime` datetime DEFAULT NULL COMMENT '文件上传时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='素材表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fileinfo`
--

LOCK TABLES `fileinfo` WRITE;
/*!40000 ALTER TABLE `fileinfo` DISABLE KEYS */;
/*!40000 ALTER TABLE `fileinfo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `hiseventinfo`
--

DROP TABLE IF EXISTS `hiseventinfo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `hiseventinfo` (
  `id` int(11) NOT NULL,
  `year` int(11) DEFAULT NULL,
  `eventName` varchar(45) DEFAULT NULL,
  `eventContent` varchar(512) DEFAULT NULL,
  `picId` int(11) DEFAULT NULL,
  `enentLink` varchar(128) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `hiseventinfo`
--

LOCK TABLES `hiseventinfo` WRITE;
/*!40000 ALTER TABLE `hiseventinfo` DISABLE KEYS */;
/*!40000 ALTER TABLE `hiseventinfo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `message`
--

DROP TABLE IF EXISTS `message`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `message` (
  `id` int(11) NOT NULL,
  `title` varchar(45) DEFAULT NULL,
  `region` varchar(45) DEFAULT NULL,
  `name` varchar(45) DEFAULT NULL,
  `content` varchar(512) DEFAULT NULL,
  `ctime` datetime DEFAULT NULL,
  `replycontent` varchar(512) DEFAULT NULL,
  `replyname` varchar(45) DEFAULT NULL,
  `replytime` datetime DEFAULT NULL,
  `isShow` int(11) DEFAULT NULL,
  `isHot` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `message`
--

LOCK TABLES `message` WRITE;
/*!40000 ALTER TABLE `message` DISABLE KEYS */;
/*!40000 ALTER TABLE `message` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `newsinfo`
--

DROP TABLE IF EXISTS `newsinfo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
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
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `newsinfo`
--

LOCK TABLES `newsinfo` WRITE;
/*!40000 ALTER TABLE `newsinfo` DISABLE KEYS */;
/*!40000 ALTER TABLE `newsinfo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `notice`
--

DROP TABLE IF EXISTS `notice`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
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
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `notice`
--

LOCK TABLES `notice` WRITE;
/*!40000 ALTER TABLE `notice` DISABLE KEYS */;
/*!40000 ALTER TABLE `notice` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `userinfo`
--

DROP TABLE IF EXISTS `userinfo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
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
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `userinfo`
--

LOCK TABLES `userinfo` WRITE;
/*!40000 ALTER TABLE `userinfo` DISABLE KEYS */;
/*!40000 ALTER TABLE `userinfo` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-12-06 18:31:07
