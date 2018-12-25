/*
 Navicat Premium Data Transfer

 Source Server         : 
 Source Server Type    : MySQL
 Source Server Version : 50724
 Source Host           : 127.0.0.1:3306
 Source Schema         : StmDB

 Target Server Type    : MySQL
 Target Server Version : 50724
 File Encoding         : 65001

 Date: 25/12/2018 17:27:20
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;


CREATE DATABASE `StmDB` CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_unicode_ci';
USE StmDB;

-- ----------------------------
-- Table structure for EventInfo
-- ----------------------------
DROP TABLE IF EXISTS `EventInfo`;
CREATE TABLE `EventInfo`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateTime` datetime(0) NOT NULL,
  `UpdateTime` datetime(0) NOT NULL,
  `IsDeleted` int(11) NOT NULL,
  `EventId` int(11) NOT NULL,
  `Title` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Content` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `Author` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `IsShow` int(11) NOT NULL,
  `IsTop` int(11) NOT NULL,
  `ThumbUrl` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `ThumbId` int(11) NOT NULL,
  `SortNo` int(11) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for EventType
-- ----------------------------
DROP TABLE IF EXISTS `EventType`;
CREATE TABLE `EventType`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateTime` datetime(0) NOT NULL,
  `UpdateTime` datetime(0) NOT NULL,
  `IsDeleted` int(11) NOT NULL,
  `ParentId` int(11) NOT NULL,
  `Name` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Level` int(11) NOT NULL,
  `IsShow` int(11) NOT NULL,
  `Icon` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `SortNo` int(11) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for ExpertInfo
-- ----------------------------
DROP TABLE IF EXISTS `ExpertInfo`;
CREATE TABLE `ExpertInfo`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateTime` datetime(0) NOT NULL,
  `UpdateTime` datetime(0) NOT NULL,
  `IsDeleted` int(11) NOT NULL,
  `Title` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Content` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `PicFileId` int(11) NOT NULL,
  `PicUrl` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Author` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `IsShow` int(11) NOT NULL,
  `SortNo` double NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for HisEventInfo
-- ----------------------------
DROP TABLE IF EXISTS `HisEventInfo`;
CREATE TABLE `HisEventInfo`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateTime` datetime(0) NOT NULL,
  `UpdateTime` datetime(0) NOT NULL,
  `IsDeleted` int(11) NOT NULL,
  `Title` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Content` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `Year` int(11) NOT NULL,
  `PicId` int(11) NOT NULL,
  `Link` int(11) NOT NULL,
  `SortNo` int(11) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for Message
-- ----------------------------
DROP TABLE IF EXISTS `Message`;
CREATE TABLE `Message`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateTime` datetime(0) NOT NULL,
  `UpdateTime` datetime(0) NOT NULL,
  `IsDeleted` int(11) NOT NULL,
  `Title` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Content` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Region` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Name` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `ReplyContent` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `ReplyName` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `ReplyTime` datetime(0) NULL DEFAULT NULL,
  `IsShow` int(11) NOT NULL,
  `IsTop` int(11) NOT NULL,
  `SortNo` double NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for NewsInfo
-- ----------------------------
DROP TABLE IF EXISTS `NewsInfo`;
CREATE TABLE `NewsInfo`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateTime` datetime(0) NOT NULL,
  `UpdateTime` datetime(0) NOT NULL,
  `IsDeleted` int(11) NOT NULL,
  `Title` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Content` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `PubTime` datetime(0) NOT NULL,
  `PubOrg` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Author` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `IsShow` int(11) NOT NULL,
  `IsTop` int(11) NOT NULL,
  `IsRoll` int(11) NOT NULL,
  `ThumbId` int(11) NOT NULL,
  `ThumUrl` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `NewsType` int(11) NOT NULL,
  `VideoId` int(11) NOT NULL,
  `VideoUrl` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `SortNo` double NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for Notice
-- ----------------------------
DROP TABLE IF EXISTS `Notice`;
CREATE TABLE `Notice`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateTime` datetime(0) NOT NULL,
  `UpdateTime` datetime(0) NOT NULL,
  `IsDeleted` int(11) NOT NULL,
  `Title` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Content` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `PubTime` datetime(0) NOT NULL,
  `PubOrg` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Author` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `IsShow` int(11) NOT NULL,
  `IsTop` int(11) NOT NULL,
  `IsRoll` int(11) NOT NULL,
  `SortNo` double NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for ResourceInfo
-- ----------------------------
DROP TABLE IF EXISTS `ResourceInfo`;
CREATE TABLE `ResourceInfo`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateTime` datetime(0) NOT NULL,
  `UpdateTime` datetime(0) NOT NULL,
  `IsDeleted` int(11) NOT NULL,
  `Filepath` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Url` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `FileMd5` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `FileName` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `FileType` varchar(16) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for UserInfo
-- ----------------------------
DROP TABLE IF EXISTS `UserInfo`;
CREATE TABLE `UserInfo`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateTime` datetime(0) NOT NULL,
  `UpdateTime` datetime(0) NOT NULL,
  `IsDeleted` int(11) NOT NULL,
  `UserName` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Password` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `TrueName` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Department` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `State` int(11) NOT NULL,
  `AuthType` int(11) NOT NULL,
  `MobileNo` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `Email` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `Gender` int(11) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for __EFMigrationsHistory
-- ----------------------------
DROP TABLE IF EXISTS `__EFMigrationsHistory`;
CREATE TABLE `__EFMigrationsHistory`  (
  `MigrationId` varchar(150) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`MigrationId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;



-- ----------------------------
-- Init data
-- ----------------------------


INSERT INTO `EventType` (`CreateTime`,`UpdateTime`,`IsDeleted`,`ParentId`,`Name`,`Level`,`IsShow`,`Icon`,`SortNo`)
VALUES ('2018-12-16 16:45:31', '2018-12-16 16:45:31', '0', '0', '赛事分类', '1', '0', '', '1');

INSERT INTO `UserInfo` (`CreateTime`,`UpdateTime`,`IsDeleted`,`UserName`,`Password`,`TrueName`,`Department`,`State`,`AuthType`,`MobileNo`,`Email`,`Gender`)
VALUES ('2018-12-12 12:12:12', '2018-12-12 12:12:12', '0', 'dev', 'e10adc3949ba59abbe56e057f20f883e', 'developer', 'developer', '1', '1', '18900001111', '1@q.com', '0');
