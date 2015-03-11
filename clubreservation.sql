/*
Navicat MySQL Data Transfer

Source Server         : legend
Source Server Version : 50619
Source Host           : localhost:3307
Source Database       : clubreservation

Target Server Type    : MYSQL
Target Server Version : 50619
File Encoding         : 65001

Date: 2014-08-23 23:48:39
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for activity
-- ----------------------------
DROP TABLE IF EXISTS `activity`;
CREATE TABLE `activity` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '活动id',
  `club_id` int(11) NOT NULL COMMENT '俱乐部id',
  `user_id` int(11) DEFAULT NULL COMMENT '创建者id',
  `activityName` varchar(100) DEFAULT NULL COMMENT '活动名称',
  `activityPlace` varchar(100) DEFAULT NULL COMMENT '活动地点',
  `activityInfo` text COMMENT '活动介绍',
  `minNumber` int(11) DEFAULT NULL COMMENT '最低限制人数',
  `maxNumber` int(11) DEFAULT NULL COMMENT '最高限制人数',
  `state` int(11) DEFAULT NULL COMMENT '活动是否结束的状态',
  `createDate` datetime DEFAULT NULL COMMENT '创建时间',
  `startTime` datetime DEFAULT NULL COMMENT '活动开始时间',
  `endTime` datetime DEFAULT NULL COMMENT '活动结束时间',
  `updateDate` datetime DEFAULT NULL COMMENT '活动更新时间',
  `updateUser_id` int(11) DEFAULT NULL COMMENT '活动信息修改者',
  `enrollNo` int(11) DEFAULT NULL COMMENT '报名人数',
  `enrollEndTime` datetime DEFAULT NULL COMMENT '报名截止时间',
  PRIMARY KEY (`id`),
  KEY `club_id` (`club_id`),
  KEY `user_id` (`user_id`),
  CONSTRAINT `activity_ibfk_1` FOREIGN KEY (`club_id`) REFERENCES `club` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of activity
-- ----------------------------
INSERT INTO `activity` VALUES ('19', '1', '15', '斯蒂芬感受到', '上的', '22', '1', '22', '0', '2014-08-23 16:28:52', '2014-08-23 16:28:30', '2014-08-23 16:28:30', '2014-08-23 16:28:52', '0', '1', '2014-08-24 16:28:30');
INSERT INTO `activity` VALUES ('20', '1', '15', '电饭锅', '电饭锅', '11', '1', '22', '0', '2014-08-23 16:31:03', '2014-08-23 16:30:37', '2014-09-23 16:30:37', '2014-08-23 16:31:03', '0', '2', '2014-08-23 16:30:37');

-- ----------------------------
-- Table structure for club
-- ----------------------------
DROP TABLE IF EXISTS `club`;
CREATE TABLE `club` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '俱乐部id',
  `clubName` varchar(100) DEFAULT NULL COMMENT '俱乐部名',
  `clubCode` varchar(10) DEFAULT NULL COMMENT '俱乐部编号',
  `clubInfo` text COMMENT '俱乐部介绍',
  `createDate` datetime DEFAULT NULL COMMENT '创建时间',
  `user_id` int(11) DEFAULT NULL COMMENT '更改者的ID',
  `updateDate` datetime DEFAULT NULL COMMENT '更新信息的时间',
  `number` int(11) DEFAULT NULL COMMENT '俱乐部人数',
  `email` varchar(50) DEFAULT NULL COMMENT '俱乐部邮件列表',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of club
-- ----------------------------
INSERT INTO `club` VALUES ('1', '羽毛球', null, '羽毛球俱乐部，让他放假一天看节目HBV吧付电话费的日南昌工程vbn哦i绝对是个佛撒娇欧冠vjdsapofvkajposadfsadgdsa股份撒娇股份坡地上高速的开关风骚蛋糕VOD撒高v  ', '0001-01-01 00:00:00', '15', '2014-08-23 15:59:32', '0', 'ymq@126.com');
INSERT INTO `club` VALUES ('2', '桌游', null, '桌游俱乐部', '0001-01-01 00:00:00', '15', '2014-08-23 16:00:19', '0', 'zy@126.com');
INSERT INTO `club` VALUES ('3', '篮球', null, '篮球娱乐部', '2014-08-06 08:56:01', '0', '2014-08-06 08:56:04', '1', 'lq@126.com');

-- ----------------------------
-- Table structure for club_user
-- ----------------------------
DROP TABLE IF EXISTS `club_user`;
CREATE TABLE `club_user` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '俱乐部用户表id',
  `user_id` int(11) NOT NULL COMMENT '用户id',
  `club_id` int(11) NOT NULL COMMENT '俱乐部id',
  `joinTime` datetime DEFAULT NULL COMMENT '加入时间',
  `role_id` int(11) DEFAULT NULL COMMENT '用户等级',
  PRIMARY KEY (`id`),
  KEY `user_id` (`user_id`),
  KEY `club_id` (`club_id`),
  CONSTRAINT `club_user_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`),
  CONSTRAINT `club_user_ibfk_2` FOREIGN KEY (`club_id`) REFERENCES `club` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=46 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of club_user
-- ----------------------------
INSERT INTO `club_user` VALUES ('44', '15', '1', '2014-08-23 15:14:59', '2');
INSERT INTO `club_user` VALUES ('45', '15', '2', '2014-08-23 15:14:59', '3');

-- ----------------------------
-- Table structure for examine
-- ----------------------------
DROP TABLE IF EXISTS `examine`;
CREATE TABLE `examine` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '加入审核表id',
  `club_id` int(11) DEFAULT NULL COMMENT '俱乐部id',
  `user_id` int(11) DEFAULT NULL COMMENT '用户id',
  `createDate` datetime DEFAULT NULL COMMENT '申请时间',
  `state` int(11) DEFAULT NULL COMMENT '0未审核，1未通过',
  PRIMARY KEY (`id`),
  KEY `club_id` (`club_id`),
  KEY `user_id` (`user_id`),
  CONSTRAINT `examine_ibfk_1` FOREIGN KEY (`club_id`) REFERENCES `club` (`id`),
  CONSTRAINT `examine_ibfk_2` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of examine
-- ----------------------------
INSERT INTO `examine` VALUES ('4', '3', '15', '2014-08-23 15:48:22', '0');
INSERT INTO `examine` VALUES ('5', '3', '15', '2014-08-23 15:48:35', '0');
INSERT INTO `examine` VALUES ('6', '3', '15', '2014-08-23 16:00:30', '0');

-- ----------------------------
-- Table structure for power
-- ----------------------------
DROP TABLE IF EXISTS `power`;
CREATE TABLE `power` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '权限表id',
  `name` varchar(50) DEFAULT NULL COMMENT '权限名称',
  `controller` varchar(100) DEFAULT NULL COMMENT '控制器',
  `action` varchar(100) DEFAULT NULL COMMENT '操作',
  `level` int(11) DEFAULT NULL COMMENT '菜单等级',
  `parents` int(11) DEFAULT NULL COMMENT '父菜单',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=38 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of power
-- ----------------------------
INSERT INTO `power` VALUES ('1', '用户一览', 'User', 'Index', '1', '0');
INSERT INTO `power` VALUES ('2', '重置密码', 'User', 'ResetPassword', '2', '1');
INSERT INTO `power` VALUES ('3', '删除用户', 'User', 'Delete', '2', '1');
INSERT INTO `power` VALUES ('4', '用户编辑', 'User', 'Edit', '2', '1');
INSERT INTO `power` VALUES ('5', '用户详细', 'User', 'Details', '2', '1');
INSERT INTO `power` VALUES ('6', '获取权限', 'User', 'GetUserPowers', '2', '1');
INSERT INTO `power` VALUES ('7', '设置权限', 'User', 'SetUserPowers', '2', '1');
INSERT INTO `power` VALUES ('8', '用户添加', 'User', 'Create', '1', '0');
INSERT INTO `power` VALUES ('9', '俱乐部一览', 'Club', 'Index', '1', '0');
INSERT INTO `power` VALUES ('10', '俱乐部追加', 'Club', 'Create', '1', '0');
INSERT INTO `power` VALUES ('11', '俱乐部编辑', 'Club', 'Edit', '1', '1');
INSERT INTO `power` VALUES ('12', '俱乐部详细', 'Club', 'Details', '1', '1');
INSERT INTO `power` VALUES ('13', '俱乐部删除', 'Club', 'Delete', '1', '1');
INSERT INTO `power` VALUES ('14', '资产一览', 'Club', 'Property', '1', '1');
INSERT INTO `power` VALUES ('15', '资产增加', 'Club', 'PropertyCreate', '1', '1');
INSERT INTO `power` VALUES ('16', '资产详细', 'Club', 'PropertyDetails', '1', '1');
INSERT INTO `power` VALUES ('17', '资产编辑', 'Club', 'PropertyEdit', '1', '1');
INSERT INTO `power` VALUES ('18', '申请加入俱乐部', 'Club', 'ExamineCreate', '1', '1');
INSERT INTO `power` VALUES ('19', '报名一览', 'Club', 'Examine', '1', '1');
INSERT INTO `power` VALUES ('20', '报名通过', 'Club', 'ExaminePass', '1', '1');
INSERT INTO `power` VALUES ('21', '报名退回', 'Club', 'ExamineReturn', '1', '1');
INSERT INTO `power` VALUES ('22', '活动一览', 'Activity', 'Index', '1', '1');
INSERT INTO `power` VALUES ('23', '活动详细', 'Activity', 'Details', '1', '1');
INSERT INTO `power` VALUES ('24', '活动新建', 'Activity', 'Create', '1', '1');
INSERT INTO `power` VALUES ('25', '活动编辑', 'Activity', 'Edit', '1', '1');
INSERT INTO `power` VALUES ('26', '活动删除', 'Activity', 'Delete', '1', '1');
INSERT INTO `power` VALUES ('27', '活动报名一览', 'UserActivity', 'Index', '1', '1');
INSERT INTO `power` VALUES ('28', '活动报名', 'UserActivity', 'Enroll', '1', '1');
INSERT INTO `power` VALUES ('29', '个人中心主页显示', 'UserZone', 'Index', '1', '1');
INSERT INTO `power` VALUES ('30', '修改密码 ', 'UserZone', 'ChangePwd', '1', '1');
INSERT INTO `power` VALUES ('31', '取消报名', 'UserZone', 'CancelEnroll', '1', '1');
INSERT INTO `power` VALUES ('32', '用户活动统计一览', 'Statistics', 'Users', '1', '1');
INSERT INTO `power` VALUES ('33', '用户详细信息', 'Statistics', 'UserDetails', '1', '1');
INSERT INTO `power` VALUES ('34', '俱乐部信息统计', 'Statistics', 'Clubs', '1', '1');
INSERT INTO `power` VALUES ('35', '俱乐部详细信息', 'Statistics', 'ClubDetails', '1', '1');
INSERT INTO `power` VALUES ('36', '根据俱乐部ID显示折线图', 'Statistics', 'GetClubById', '1', '1');
INSERT INTO `power` VALUES ('37', '首页', 'User', 'Home', '1', '0');

-- ----------------------------
-- Table structure for property
-- ----------------------------
DROP TABLE IF EXISTS `property`;
CREATE TABLE `property` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '资产表id',
  `club_id` int(11) NOT NULL COMMENT '俱乐部id',
  `name` varchar(50) DEFAULT NULL COMMENT '资产名',
  `count` int(11) DEFAULT NULL COMMENT '数量',
  `price` double(10,2) DEFAULT NULL COMMENT '单价',
  `updateDate` datetime DEFAULT NULL COMMENT '资产最后更新时间',
  PRIMARY KEY (`id`),
  KEY `club_id` (`club_id`),
  CONSTRAINT `property_ibfk_1` FOREIGN KEY (`club_id`) REFERENCES `club` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of property
-- ----------------------------
INSERT INTO `property` VALUES ('13', '1', '测试资产', '1', '11.00', '2014-08-23 14:33:36');
INSERT INTO `property` VALUES ('14', '1', '测试资产', '1', '11.00', '2014-08-23 15:46:24');
INSERT INTO `property` VALUES ('15', '1', '测试资产', '1', '11.00', '2014-08-23 14:35:23');
INSERT INTO `property` VALUES ('16', '1', '测试4', '11', '11.00', '2014-08-23 15:46:03');

-- ----------------------------
-- Table structure for role
-- ----------------------------
DROP TABLE IF EXISTS `role`;
CREATE TABLE `role` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '角色id',
  `roleName` varchar(50) DEFAULT NULL COMMENT '角色名称',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of role
-- ----------------------------
INSERT INTO `role` VALUES ('1', '超级管理员');
INSERT INTO `role` VALUES ('2', '俱乐部管理员');
INSERT INTO `role` VALUES ('3', '普通用户');

-- ----------------------------
-- Table structure for role_power
-- ----------------------------
DROP TABLE IF EXISTS `role_power`;
CREATE TABLE `role_power` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '角色权限表id',
  `power_id` int(11) NOT NULL COMMENT '菜单id',
  `role_id` int(11) NOT NULL COMMENT '角色id',
  `createDate` datetime DEFAULT NULL COMMENT '创建日期',
  PRIMARY KEY (`id`),
  KEY `power_id` (`power_id`),
  KEY `role_id` (`role_id`),
  CONSTRAINT `role_power_ibfk_1` FOREIGN KEY (`power_id`) REFERENCES `power` (`id`),
  CONSTRAINT `role_power_ibfk_2` FOREIGN KEY (`role_id`) REFERENCES `role` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=87 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of role_power
-- ----------------------------
INSERT INTO `role_power` VALUES ('1', '1', '1', '2014-08-12 10:56:51');
INSERT INTO `role_power` VALUES ('2', '2', '1', '2014-08-12 11:25:22');
INSERT INTO `role_power` VALUES ('3', '3', '1', '2014-08-13 13:31:01');
INSERT INTO `role_power` VALUES ('4', '4', '1', '2014-08-13 13:31:03');
INSERT INTO `role_power` VALUES ('5', '5', '1', '2014-08-13 13:31:06');
INSERT INTO `role_power` VALUES ('6', '6', '1', '2014-08-13 13:31:08');
INSERT INTO `role_power` VALUES ('7', '7', '1', '2014-08-13 13:31:10');
INSERT INTO `role_power` VALUES ('8', '8', '1', null);
INSERT INTO `role_power` VALUES ('9', '9', '1', null);
INSERT INTO `role_power` VALUES ('10', '10', '1', null);
INSERT INTO `role_power` VALUES ('27', '1', '2', null);
INSERT INTO `role_power` VALUES ('28', '2', '2', null);
INSERT INTO `role_power` VALUES ('29', '3', '2', null);
INSERT INTO `role_power` VALUES ('30', '4', '2', null);
INSERT INTO `role_power` VALUES ('31', '5', '2', null);
INSERT INTO `role_power` VALUES ('32', '6', '2', null);
INSERT INTO `role_power` VALUES ('33', '7', '2', null);
INSERT INTO `role_power` VALUES ('34', '8', '2', null);
INSERT INTO `role_power` VALUES ('35', '11', '1', null);
INSERT INTO `role_power` VALUES ('36', '12', '1', null);
INSERT INTO `role_power` VALUES ('37', '13', '1', null);
INSERT INTO `role_power` VALUES ('38', '14', '1', null);
INSERT INTO `role_power` VALUES ('39', '15', '1', null);
INSERT INTO `role_power` VALUES ('40', '16', '1', null);
INSERT INTO `role_power` VALUES ('41', '17', '1', null);
INSERT INTO `role_power` VALUES ('42', '34', '1', null);
INSERT INTO `role_power` VALUES ('43', '35', '1', null);
INSERT INTO `role_power` VALUES ('44', '36', '1', null);
INSERT INTO `role_power` VALUES ('45', '11', '2', null);
INSERT INTO `role_power` VALUES ('46', '14', '2', null);
INSERT INTO `role_power` VALUES ('47', '15', '2', null);
INSERT INTO `role_power` VALUES ('48', '16', '2', null);
INSERT INTO `role_power` VALUES ('49', '17', '2', null);
INSERT INTO `role_power` VALUES ('50', '22', '2', null);
INSERT INTO `role_power` VALUES ('51', '23', '2', null);
INSERT INTO `role_power` VALUES ('52', '24', '2', null);
INSERT INTO `role_power` VALUES ('53', '25', '2', null);
INSERT INTO `role_power` VALUES ('54', '26', '2', null);
INSERT INTO `role_power` VALUES ('55', '27', '2', null);
INSERT INTO `role_power` VALUES ('56', '28', '2', null);
INSERT INTO `role_power` VALUES ('57', '29', '2', null);
INSERT INTO `role_power` VALUES ('58', '30', '2', null);
INSERT INTO `role_power` VALUES ('59', '31', '2', null);
INSERT INTO `role_power` VALUES ('60', '32', '2', null);
INSERT INTO `role_power` VALUES ('61', '33', '2', null);
INSERT INTO `role_power` VALUES ('62', '34', '2', null);
INSERT INTO `role_power` VALUES ('63', '35', '2', null);
INSERT INTO `role_power` VALUES ('64', '36', '2', null);
INSERT INTO `role_power` VALUES ('65', '27', '3', null);
INSERT INTO `role_power` VALUES ('66', '28', '3', null);
INSERT INTO `role_power` VALUES ('67', '29', '3', null);
INSERT INTO `role_power` VALUES ('68', '30', '3', null);
INSERT INTO `role_power` VALUES ('69', '31', '3', null);
INSERT INTO `role_power` VALUES ('70', '32', '3', null);
INSERT INTO `role_power` VALUES ('71', '33', '3', null);
INSERT INTO `role_power` VALUES ('72', '34', '3', null);
INSERT INTO `role_power` VALUES ('73', '35', '3', null);
INSERT INTO `role_power` VALUES ('74', '36', '3', null);
INSERT INTO `role_power` VALUES ('75', '37', '1', null);
INSERT INTO `role_power` VALUES ('76', '37', '2', null);
INSERT INTO `role_power` VALUES ('77', '37', '3', null);
INSERT INTO `role_power` VALUES ('78', '32', '1', null);
INSERT INTO `role_power` VALUES ('79', '33', '1', null);
INSERT INTO `role_power` VALUES ('80', '9', '2', null);
INSERT INTO `role_power` VALUES ('81', '18', '2', null);
INSERT INTO `role_power` VALUES ('82', '18', '3', null);
INSERT INTO `role_power` VALUES ('83', '19', '2', null);
INSERT INTO `role_power` VALUES ('84', '20', '2', null);
INSERT INTO `role_power` VALUES ('85', '21', '2', null);
INSERT INTO `role_power` VALUES ('86', '12', '2', null);

-- ----------------------------
-- Table structure for user
-- ----------------------------
DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '用户id',
  `employeeCode` varchar(8) NOT NULL COMMENT '用户名',
  `password` varchar(100) DEFAULT NULL COMMENT '密码',
  `email` varchar(50) DEFAULT NULL COMMENT '邮箱',
  `tel` varchar(20) DEFAULT NULL COMMENT '电话',
  `phone` varchar(20) DEFAULT NULL COMMENT '手机号',
  `name` varchar(50) DEFAULT NULL COMMENT '姓名',
  `sex` int(11) DEFAULT NULL COMMENT '性别',
  `entryTime` datetime DEFAULT NULL COMMENT '入职时间',
  `roleId` int(11) DEFAULT NULL COMMENT '角色id',
  `team` varchar(50) DEFAULT NULL COMMENT '所属组别',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of user
-- ----------------------------
INSERT INTO `user` VALUES ('1', '000001', '670B14728AD9902AECBA32E22FA4F6BD', '1234@163.com', '1000', '18626058997', '李四', '0', '2014-08-13 10:45:12', '1', '2');
INSERT INTO `user` VALUES ('15', '000002', '670B14728AD9902AECBA32E22FA4F6BD', 'angelfan@163.com', '2714', '18626058997', '耿返', '0', '2014-08-23 00:00:00', '2', '1kTeam1');
INSERT INTO `user` VALUES ('16', '000003', '670B14728AD9902AECBA32E22FA4F6BD', 'angel@163.com', '1234', '18626058997', '测试1', '0', '2014-08-23 00:00:00', '3', '1kTeam1');
INSERT INTO `user` VALUES ('17', '1111111', '670B14728AD9902AECBA32E22FA4F6BD', 'angelfan@163.com', '1234', '18626058997', '测试2', '0', '2014-08-23 00:00:00', '3', '1kTeam1');
INSERT INTO `user` VALUES ('18', '000004', '670B14728AD9902AECBA32E22FA4F6BD', 'angelfan@163.com', '1234', '18626058997', '@.FT', '0', '2014-08-23 00:00:00', '3', '1kTeam1');
INSERT INTO `user` VALUES ('20', '000006', '670B14728AD9902AECBA32E22FA4F6BD', 'angelfan@163.com', '2714', '18626058997', '1', '0', '2014-08-23 00:00:00', '3', '1kTeam1');
INSERT INTO `user` VALUES ('21', '000007', '670B14728AD9902AECBA32E22FA4F6BD', 'angelfan@163.com', null, '18626058997', '测试3', '1', '2014-08-23 00:00:00', '3', '1kTeam1');
INSERT INTO `user` VALUES ('22', '000011', '670B14728AD9902AECBA32E22FA4F6BD', 'angelfan@163.com', '1234', '18626058997', '测试2', '0', '2014-08-23 00:00:00', '3', '1kTeam1');
INSERT INTO `user` VALUES ('23', '000012', '670B14728AD9902AECBA32E22FA4F6BD', 'angelfan@163.com', '1234', '18626058997', '测试4', '0', '2014-08-23 00:00:00', '3', '1kTeam1');
INSERT INTO `user` VALUES ('25', '000015', '670B14728AD9902AECBA32E22FA4F6BD', 'angelfan@163.com', '1234', '18626058997', '测试2', '0', '2014-08-23 00:00:00', '3', '1kTeam112312312312312222222222222222222223');
INSERT INTO `user` VALUES ('26', '123456', '670B14728AD9902AECBA32E22FA4F6BD', 'angelfan@163.com', '1234', '18626058997', 'gf测试1', '0', '2014-08-23 00:00:00', '3', '1kTeam1');
INSERT INTO `user` VALUES ('27', '000111', '670B14728AD9902AECBA32E22FA4F6BD', 'angelfan@163.com', '2714', '18626058997', '测试2', '0', '2014-08-23 00:00:00', '3', '1kTeam1');

-- ----------------------------
-- Table structure for user_activity
-- ----------------------------
DROP TABLE IF EXISTS `user_activity`;
CREATE TABLE `user_activity` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '报名id',
  `user_id` int(11) NOT NULL COMMENT '用户id',
  `activity_id` int(11) NOT NULL COMMENT '活动id',
  `enrollTime` datetime DEFAULT NULL COMMENT '报名时间',
  `state` int(11) DEFAULT NULL COMMENT '活动提醒状态',
  PRIMARY KEY (`id`),
  KEY `user_id` (`user_id`),
  KEY `activity_id` (`activity_id`),
  CONSTRAINT `user_activity_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`),
  CONSTRAINT `user_activity_ibfk_2` FOREIGN KEY (`activity_id`) REFERENCES `activity` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of user_activity
-- ----------------------------

-- ----------------------------
-- Table structure for user_role
-- ----------------------------
DROP TABLE IF EXISTS `user_role`;
CREATE TABLE `user_role` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '用户角色id',
  `user_id` int(11) NOT NULL COMMENT '用户id',
  `role_id` int(11) NOT NULL COMMENT '角色id',
  `createDate` datetime DEFAULT NULL COMMENT '更改用户权限时间',
  PRIMARY KEY (`id`),
  KEY `user_id` (`user_id`),
  KEY `role_id` (`role_id`),
  CONSTRAINT `user_role_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`),
  CONSTRAINT `user_role_ibfk_2` FOREIGN KEY (`role_id`) REFERENCES `role` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of user_role
-- ----------------------------
