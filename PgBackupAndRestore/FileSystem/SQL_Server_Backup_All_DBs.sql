
-- https://www.mssqltips.com/sqlservertip/1070/simple-script-to-backup-all-sql-server-databases/


DECLARE @name nvarchar(256) -- database name  
DECLARE @path nvarchar(256) -- path for backup files  
DECLARE @fileName nvarchar(256) -- filename for backup  
DECLARE @fileDate nvarchar(10) -- used for file name
 
-- specify database backup directory
SET @path = 'D:\AllDBs\'  

-- specify filename format
SELECT @fileDate = CONVERT(nvarchar(10), CURRENT_TIMESTAMP, 112); 
 
DECLARE db_cursor CURSOR READ_ONLY FOR  
		SELECT name 
		FROM master.dbo.sysdatabases 
		WHERE name NOT IN ('master','model','msdb','tempdb')  -- exclude these databases
 
OPEN db_cursor   
FETCH NEXT FROM db_cursor INTO @name   
 
WHILE @@FETCH_STATUS = 0   
BEGIN   
   SET @fileName = @path + @name + '_' + @fileDate + '.bak'  
   BACKUP DATABASE @name TO DISK = @fileName  
 
   FETCH NEXT FROM db_cursor INTO @name   
END   
