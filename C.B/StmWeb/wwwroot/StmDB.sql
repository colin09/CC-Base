/*
 Navicat Premium Data Transfer

 Source Server         : local
 Source Server Type    : MySQL
 Source Server Version : 80013
 Source Host           : localhost:3306
 Source Schema         : StmDB

 Target Server Type    : MySQL
 Target Server Version : 80013
 File Encoding         : 65001

 Date: 03/01/2020 20:44:23
*/


SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

--DROP DATABASE IF EXISTS `StmDB`;
CREATE DATABASE `StmDB` CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_unicode_ci';
USE StmDB;


-- ----------------------------
-- Table structure for Document
-- ----------------------------
DROP TABLE IF EXISTS `Document`;
CREATE TABLE `Document`  (
  `Id` bigint(20) NOT NULL,
  `DocType` int(8) NULL DEFAULT NULL,
  `Title` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `SubTitle` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Content` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `SimpleContent` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `Url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Author` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `PublishTime` datetime(0) NULL DEFAULT NULL,
  `CreateTime` datetime(0) NULL DEFAULT NULL,
  `UpdateTime` datetime(0) NULL DEFAULT NULL,
  `IsDeleted` bit(8) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for EventInfo
-- ----------------------------
DROP TABLE IF EXISTS `EventInfo`;
CREATE TABLE `EventInfo`  (
  `Id` bigint(11) NOT NULL AUTO_INCREMENT,
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
  `DocumentId` bigint(20) NOT NULL,
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
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for ExpertInfo
-- ----------------------------
DROP TABLE IF EXISTS `ExpertInfo`;
CREATE TABLE `ExpertInfo`  (
  `Id` bigint(11) NOT NULL AUTO_INCREMENT,
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
  `DocumentId` bigint(20) NOT NULL,
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
  `Id` bigint(11) NOT NULL AUTO_INCREMENT,
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
  `DocumentId` bigint(20) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for Notice
-- ----------------------------
DROP TABLE IF EXISTS `Notice`;
CREATE TABLE `Notice`  (
  `Id` bigint(11) NOT NULL AUTO_INCREMENT,
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
  `DocumentId` bigint(20) NOT NULL,
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
-- Table structure for AuthNavs
-- ----------------------------
DROP TABLE IF EXISTS `AuthNavs`;
CREATE TABLE `AuthNavs`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateTime` datetime(0) NOT NULL,
  `UpdateTime` datetime(0) NOT NULL,
  `IsDeleted` int(11) NOT NULL,
  `ParentId`  int(11) NOT NULL,
  `NavCode` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `NavType` int(11) NOT NULL,
  `Title` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Desc` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Icon` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Url` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Module` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Target` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Sort` int(11) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;


-- ----------------------------
-- Table structure for AuthRole
-- ----------------------------
DROP TABLE IF EXISTS `AuthRole`;
CREATE TABLE `AuthRole`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateTime` datetime(0) NOT NULL,
  `UpdateTime` datetime(0) NOT NULL,
  `IsDeleted` int(11) NOT NULL,
  `AuthRoleType` int(11) NOT NULL,
  `RoleName` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `RoleCode` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Sort` int(11) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for AuthRoleNavs
-- ----------------------------
DROP TABLE IF EXISTS `AuthRoleNavs`;
CREATE TABLE `AuthRoleNavs`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateTime` datetime(0) NOT NULL,
  `UpdateTime` datetime(0) NOT NULL,
  `IsDeleted` int(11) NOT NULL,
  `AuthRoleId` int(11) NOT NULL,
  `AuthNavId` int(11) NOT NULL,
  `Sort` int(11) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;


-- ----------------------------
-- Table structure for AuthUser
-- ----------------------------
DROP TABLE IF EXISTS `AuthUser`;
CREATE TABLE `AuthUser`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateTime` datetime(0) NOT NULL,
  `UpdateTime` datetime(0) NOT NULL,
  `IsDeleted` int(11) NOT NULL,
  `UserName` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Password` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `TrueName` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `MobileNo` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `EMail` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `LastLoginTime` datetime(0) NOT NULL,
  `Status` int(11) NOT NULL,
  `AuthRoleId` int(11) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;


-- ----------------------------
-- Table structure for AuthUserNavs
-- ----------------------------
DROP TABLE IF EXISTS `AuthUserNavs`;
CREATE TABLE `AuthUserNavs`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreateTime` datetime(0) NOT NULL,
  `UpdateTime` datetime(0) NOT NULL,
  `IsDeleted` int(11) NOT NULL,
  `AuthUserId` int(11) NOT NULL,
  `AuthNavId` int(11) NOT NULL,
  `Sort` int(11) NOT NULL,
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




INSERT INTO `EventType` (`CreateTime`,`UpdateTime`,`IsDeleted`,`ParentId`,`Name`,`Level`,`IsShow`,`Icon`,`SortNo`)
VALUES ('2018-12-16 16:45:31', '2018-12-16 16:45:31', '0', '0', '赛事分类', '1', '0', '', '1');

INSERT INTO `UserInfo` (`CreateTime`,`UpdateTime`,`IsDeleted`,`UserName`,`Password`,`TrueName`,`Department`,`State`,`AuthType`,`MobileNo`,`Email`,`Gender`)
VALUES ('2018-12-12 12:12:12', '2018-12-12 12:12:12', '0', 'dev', 'e10adc3949ba59abbe56e057f20f883e', 'developer', 'developer', '1', '1', '18900001111', '1@q.com', '0');




INSERT INTO `StmDB`.`AuthRole`(`CreateTime`, `UpdateTime`, `IsDeleted`,`AuthRoleType`, `RoleName`, `RoleCode`, `Sort`) 
VALUES (now(), now(), 0, 1, 'System', 'System', 0);

INSERT INTO `StmDB`.`AuthUser`(`CreateTime`, `UpdateTime`, `IsDeleted`, `UserName`, `Password`, `TrueName`, `MobileNo`, `EMail`, `LastLoginTime`, `Status`, `AuthRoleId`) 
VALUES (now(),now(), 0, 'System', 'e10adc3949ba59abbe56e057f20f883e', 'System', '99999999999', '99@99.com', now(), 1, 1);


