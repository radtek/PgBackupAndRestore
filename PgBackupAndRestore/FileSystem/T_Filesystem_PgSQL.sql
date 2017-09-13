
CREATE TABLE t_filesystem 
(
  FS_Id bigint NOT NULL,
  FS_Target_FS_Id bigint NOT NULL,
  FS_Parent_FS_Id bigint DEFAULT NULL,
  FS_Path national character varying(4000) DEFAULT NULL,
  FS_LowerCasePath national character varying(4000) DEFAULT NULL,
  FS_Text national character varying(4000) DEFAULT NULL,
  FS_LowerCaseText national character varying(4000) DEFAULT NULL,
  FS_NameWithoutExtension national character varying(4000) DEFAULT NULL,
  FS_Extension national character varying(255) DEFAULT NULL,
  FS_IsFolder bit DEFAULT NULL,
  FS_IsCompressed bit DEFAULT NULL,
  FS_IsEncrypted bit DEFAULT NULL,
  FS_IsReadOnly bit DEFAULT NULL,
  FS_ReadLock bit DEFAULT NULL,
  FS_WriteLock bit DEFAULT NULL,
  FS_CreationTime timestamp without time zone DEFAULT NULL,
  FS_CreationTimeUTC timestamp without time zone DEFAULT NULL,
  FS_LastAccessTime timestamp without time zone DEFAULT NULL,
  FS_LastAccessTimeUTC timestamp without time zone DEFAULT NULL,
  FS_LastWriteTime timestamp without time zone DEFAULT NULL,
  FS_LastWriteTimeUTC timestamp without time zone DEFAULT NULL,
  FS_OwnerId bigint DEFAULT NULL,
  FS_OwnerGroupId bigint DEFAULT NULL,
  FS_UnixPermissions integer DEFAULT NULL,
  PRIMARY KEY (FS_Id)
);
