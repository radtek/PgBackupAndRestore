
-- https://www.mssqltips.com/sqlservertip/1584/auto-generate-sql-server-restore-script-from-backup-files-in-a-directory/

DECLARE @importPath varchar(8000)
SET @importPath = N'E:\ImportData\AllDBs\ToRestore'


DECLARE @fileList TABLE
(
     subdirectory nvarchar(512)
    ,depth int
    ,isfile bit
)


INSERT @fileList (subdirectory, depth, isfile)
EXEC master.sys.xp_dirtree @importPath, 1, 1;

--SELECT * FROM @fileList 


DECLARE @cmd nvarchar(MAX) 
DECLARE @fileName nvarchar(MAX) -- filename for backup 
DECLARE @fileNameAndPath nvarchar(MAX) -- filename for backup 
DECLARE @dbName nvarchar(MAX) -- filename for backup 

DECLARE fileList CURSOR FOR ( SELECT subdirectory FROM @fileList WHERE isfile = 1 ) 

OPEN fileList 
FETCH NEXT FROM fileList INTO @fileName 

WHILE @@FETCH_STATUS = 0 
BEGIN 
    SET @fileNameAndPath = @importPath  + '\' + @fileName 
	--SET @dbName = SUBSTRING(@fileName, 1, LEN(@fileName) - LEN('.bak') ) 
	SET @dbName = SUBSTRING(@fileName, 1, LEN(@fileName) - LEN('_YYYYMMDD.bak') ) 

    -- PRINT @fileNameAndPath 
	PRINT @dbName
	
	--SET @cmd = 'RESTORE DATABASE [' + REPLACE(@dbName, ']', ']]') + '] FROM DISK = '''  + REPLACE(@fileNameAndPath, '''', '''''') + ''' WITH NORECOVERY, REPLACE' 
	SET @cmd = 'RESTORE DATABASE [' + REPLACE(@dbName, ']', ']]') + '] FROM DISK = '''  + REPLACE(@fileNameAndPath, '''', '''''') + ''' WITH NORECOVERY' 
	PRINT @cmd 
	EXECUTE(@cmd)


	-- SET @cmd = 'RESTORE LOG [' + REPLACE(@dbName, ']', ']]') + '] FROM DISK = '''  + REPLACE(@fileNameAndPath, '''', '''''') + ''' WITH NORECOVERY' 
	-- PRINT @cmd 
	-- EXECUTE(@cmd)


	SET @cmd = 'RESTORE DATABASE [' + REPLACE(@dbName, ']', ']]') + '] WITH RECOVERY' 
	PRINT @cmd 
	EXECUTE(@cmd)

    FETCH NEXT FROM fileList INTO @fileName 
END   

CLOSE fileList 
DEALLOCATE fileList 
