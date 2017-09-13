
SELECT 
	 datname 
	,'pg_dump -U postgres -Fc ' || datname || ' > D:/PGDUMP/' || datname || '_' || to_char(current_timestamp, 'YYYYMMDD') || '.dump' AS cmd 
	-- ,current_timestamp 
	-- ,to_char(current_timestamp, 'HH12:MI:SS') 
	-- ,to_char(current_timestamp, 'HH24:MI:SS') 
	-- ,to_char(current_timestamp, 'YYYYMMDD') 
FROM pg_database 
WHERE datistemplate = false 
AND datname <> 'postgres' 

ORDER BY datname 
