/*
Navicat MySQL Data Transfer

Source Server         : JJZX
Source Server Version : 50171
Source Host           : 10.0.0.158:3306
Source Database       : jjzx

Target Server Type    : MYSQL
Target Server Version : 50171
File Encoding         : 65001

Date: 2014-03-11 17:44:19
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `JJZX_FY`
-- ----------------------------
DROP TABLE IF EXISTS `JJZX_FY`;
CREATE TABLE `JJZX_FY` (
  `RESVID` varchar(40) NOT NULL,
  `GUEST_ID` varchar(25) DEFAULT NULL,
  `NAME` varchar(400) DEFAULT NULL,
  `UNIT_ID` varchar(25) DEFAULT NULL,
  `ORDER_STATUS` varchar(25) DEFAULT NULL,
  `MOBILE` varchar(25) DEFAULT NULL,
  `ARRD` timestamp NULL DEFAULT NULL,
  `DEPD` timestamp NULL DEFAULT NULL,
  `CHANNEL` varchar(100) DEFAULT NULL,
  `PLATFORM_CODE` varchar(25) DEFAULT NULL,
  `MONEY` varchar(25) DEFAULT NULL,
  `CZD` timestamp NULL DEFAULT NULL,
  `SRESULT` varchar(25) DEFAULT NULL,
  `PRO` varchar(100) DEFAULT NULL,
  `YYS` varchar(100) DEFAULT NULL,
  `CODE` varchar(25) DEFAULT NULL,
  `PRICE` varchar(25) DEFAULT NULL,
  `GSTATUS` varchar(25) DEFAULT NULL,
  `GRESULT` varchar(25) DEFAULT NULL,
  `MSGRES` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`RESVID`),
  KEY `jjzx_fy_arrd_index` (`ARRD`) USING BTREE,
  KEY `jjzx_fy_czd_index` (`CZD`) USING BTREE,
  KEY `jjzx_fy_gresult_index` (`GRESULT`) USING BTREE
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of JJZX_FY
-- ----------------------------
INSERT INTO `JJZX_FY` VALUES ('CC20131227012924', '28797900', '刘爽', '6021', '', '13816674708', '2014-02-27 00:00:00', '2014-03-01 00:00:00', 'HOTELVP', 'IOS', '1', '2014-02-28 00:00:00', '1004', '', '', '', '', null, null, null);
INSERT INTO `JJZX_FY` VALUES ('CC20131227013026', '28797900', '张金伟', '6021', '', '13816674708', '2014-02-27 00:00:00', '2014-03-01 00:00:00', 'HOTELVP', 'IOS', '1', '2014-02-28 00:00:00', '0000', '', '', '', '1.10', '-1000', '1002', null);
INSERT INTO `JJZX_FY` VALUES ('CC20140130001724', '18054883', '孙玉凤', '0878', '', '18785013747', '2014-02-27 00:00:00', '2014-03-01 00:00:00', 'HOTELVP', 'IOS', '1', '2014-02-28 15:37:18', '1005', '', '', '', '', null, null, null);
INSERT INTO `JJZX_FY` VALUES ('CC20140106014525', '329373', '钟昕', '0078', '', '15922443257', '2014-02-27 00:00:00', '2014-02-28 00:00:00', 'HOTELVP', 'IOS', '1', '2014-02-28 15:37:18', '1002', '', '', '', '', null, null, null);
INSERT INTO `JJZX_FY` VALUES ('CC20131006015199', '12656635', '段惠宝', '0501', '', '15525200122', '2014-03-02 00:00:00', '2014-03-03 00:00:00', 'HOTELVP', 'IOS', '1', '2014-03-03 17:00:27', '1002', '', '', '', '', null, null, null);
INSERT INTO `JJZX_FY` VALUES ('CC20140202008495', '20502070', '岳峰', '0255', '', '18638659780', '2014-03-02 00:00:00', '2014-03-03 00:00:00', 'HOTELVP', 'IOS', '1', '2014-03-03 17:00:31', '1002', '', '', '', '', null, null, null);
INSERT INTO `JJZX_FY` VALUES ('CC20140129007386', '487350', '闻祥', '0072', '', '18621613912', '2014-03-02 00:00:00', '2014-03-04 00:00:00', 'HOTELVP', 'IOS', '1', '2014-03-03 17:00:33', '1005', '', '', '', '', null, null, null);
INSERT INTO `JJZX_FY` VALUES ('CC20140126003490', '12375053', '闫志钢', '0718', '', '13395358697', '2014-03-02 00:00:00', '2014-03-03 00:00:00', 'HOTELVP_JJZX_web', 'ANDROID', '1', '2014-03-03 17:00:34', '1002', '', '', '', '', null, null, null);
INSERT INTO `JJZX_FY` VALUES ('CC20140127011445', '18734691', '刘雯', '0146', '', '15922210770', '2014-03-02 00:00:00', '2014-03-03 00:00:00', 'HOTELVP', 'IOS', '1', '2014-03-03 17:00:34', '1002', '', '', '', '', null, null, null);
INSERT INTO `JJZX_FY` VALUES ('CC20131021007397', '39378', '徐隽', '0875', '', '18918001199', '2014-03-02 00:00:00', '2014-03-05 00:00:00', 'HOTELVP', 'IOS', '1', '2014-03-03 17:00:34', '1002', '', '', '', '', null, null, null);
INSERT INTO `JJZX_FY` VALUES ('CC20131021006089', '13952034', '刘佳', '0078', '', '18055230788', '2014-03-02 00:00:00', '2014-03-08 00:00:00', 'HOTELVP_JJZX_A8', 'ANDROID', '1', '2014-03-03 17:00:35', '1002', '', '', '', '', null, null, null);
INSERT INTO `JJZX_FY` VALUES ('CC20131230008975', '11975370', '黄智冠', '0359', '', '18963619786', '2014-03-02 00:00:00', '2014-03-03 00:00:00', 'HOTELVP_JJZX_web', 'ANDROID', '1', '2014-03-03 17:00:35', '1002', '', '', '', '', null, null, null);
INSERT INTO `JJZX_FY` VALUES ('CC20140131010735', '19089286', '张良', '0675', '', '18668290660', '2014-03-02 00:00:00', '2014-03-03 00:00:00', 'HOTELVP_JJZX_WEB', 'ANDROID', '1', '2014-03-03 17:00:36', '1002', '', '', '', '', null, null, null);
INSERT INTO `JJZX_FY` VALUES ('CC20140128000298', '234800', '张茵、华臻', '0347', '', '13057317606', '2014-03-02 00:00:00', '2014-03-05 00:00:00', 'HOTELVP', 'IOS', '1', '2014-03-03 17:00:36', '1002', '', '', '', '', null, null, null);
INSERT INTO `JJZX_FY` VALUES ('CC20140115017553', '8898855', '李亚娟', '0872', '', '13056831686', '2014-03-02 00:00:00', '2014-03-03 00:00:00', 'HOTELVP_JJZX_13', 'ANDROID', '1', '2014-03-03 17:00:36', '1002', '', '', '', '', null, null, null);
INSERT INTO `JJZX_FY` VALUES ('CC20140121002001', '6087026', '朱家川', '0689', '', '18817308905', '2014-03-02 00:00:00', '2014-03-03 00:00:00', 'HOTELVP_JJZX_web', 'ANDROID', '1', '2014-03-03 17:00:37', '1002', '', '', '', '', null, null, null);
INSERT INTO `JJZX_FY` VALUES ('CC20140107018940', '16339021', '张云霞', '0114', '', '18001165163', '2014-03-02 00:00:00', '2014-03-03 00:00:00', 'HOTELVP_JJZX_web', 'ANDROID', '1', '2014-03-03 17:00:37', '1002', '', '', '', '', null, null, null);
INSERT INTO `JJZX_FY` VALUES ('CC20140208008783', '212659', '岳蕾', '1107', '', '18628076955', '2014-03-02 00:00:00', '2014-03-03 00:00:00', 'HOTELVP', 'IOS', '1', '2014-03-03 17:00:37', '1002', '', '', '', '', null, null, null);
INSERT INTO `JJZX_FY` VALUES ('CC20140131010671', '19089286', '张良', '0675', '', '18668290660', '2014-03-02 00:00:00', '2014-03-03 00:00:00', 'HOTELVP_JJZX_WEB', 'ANDROID', '1', '2014-03-03 17:00:38', '1002', '', '', '', '', null, null, null);
INSERT INTO `JJZX_FY` VALUES ('CC20140204014888', '4536015', '季亚枫', '0822', '', '13913870112', '2014-03-02 00:00:00', '2014-03-03 00:00:00', 'HOTELVP_JJZX_13', 'ANDROID', '1', '2014-03-03 17:00:38', '1002', '', '', '', '', null, null, null);
INSERT INTO `JJZX_FY` VALUES ('CC20131021006518', '1057621', '黄月琴', '0875', '', '13564334747', '2014-03-02 00:00:00', '2014-03-05 00:00:00', 'HOTELVP', 'IOS', '1', '2014-03-03 17:00:38', '1002', '', '', '', '', null, null, null);
INSERT INTO `JJZX_FY` VALUES ('CC20140202015018', '269540', '袁捷', '0851', '', '13816156403', '2014-03-02 00:00:00', '2014-03-03 00:00:00', 'HOTELVP_JJZX_13', 'ANDROID', '1', '2014-03-03 17:00:39', '1002', '', '', '', '', null, null, null);
INSERT INTO `JJZX_FY` VALUES ('CC20131021006404', '39378', '徐隽', '0875', '', '18918001199', '2014-03-02 00:00:00', '2014-03-05 00:00:00', 'HOTELVP', 'IOS', '1', '2014-03-03 17:00:39', '1002', '', '', '', '', null, null, null);
INSERT INTO `JJZX_FY` VALUES ('CC20140130004155', '21292167', '曹昕', '0370', '', '15905438211', '2014-03-02 00:00:00', '2014-03-04 00:00:00', 'HOTELVP', 'IOS', '1', '2014-03-03 17:00:39', '1002', '', '', '', '', null, null, null);
