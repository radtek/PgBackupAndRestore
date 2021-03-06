CREATE TABLE `t_filesystem` (
  `FS_Id` bigint(20) NOT NULL,
  `FS_Target_FS_Id` bigint(20) NOT NULL,
  `FS_Parent_FS_Id` bigint(20) DEFAULT NULL,
  `FS_Path` varchar(4000) CHARACTER SET utf8 DEFAULT NULL,
  `FS_LowerCasePath` varchar(4000) CHARACTER SET utf8 DEFAULT NULL,
  `FS_Text` varchar(4000) CHARACTER SET utf8 DEFAULT NULL,
  `FS_LowerCaseText` varchar(4000) CHARACTER SET utf8 DEFAULT NULL,
  `FS_NameWithoutExtension` varchar(4000) CHARACTER SET utf8 DEFAULT NULL,
  `FS_Extension` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  `FS_IsFolder` bit(1) DEFAULT NULL,
  `FS_IsCompressed` bit(1) DEFAULT NULL,
  `FS_IsEncrypted` bit(1) DEFAULT NULL,
  `FS_IsReadOnly` bit(1) DEFAULT NULL,
  `FS_ReadLock` bit(1) DEFAULT NULL,
  `FS_WriteLock` bit(1) DEFAULT NULL,
  `FS_CreationTime` datetime DEFAULT NULL,
  `FS_CreationTimeUTC` datetime DEFAULT NULL,
  `FS_LastAccessTime` datetime DEFAULT NULL,
  `FS_LastAccessTimeUTC` datetime DEFAULT NULL,
  `FS_LastWriteTime` datetime DEFAULT NULL,
  `FS_LastWriteTimeUTC` datetime DEFAULT NULL,
  `FS_OwnerId` bigint(20) DEFAULT NULL,
  `FS_OwnerGroupId` bigint(20) DEFAULT NULL,
  `FS_UnixPermissions` int(11) DEFAULT NULL,
  PRIMARY KEY (`FS_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
