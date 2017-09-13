
namespace PgBackupAndRestore
{


    internal class SQL
    {

        internal static string GetConnectionString()
        {
            Npgsql.NpgsqlConnectionStringBuilder csb = new Npgsql.NpgsqlConnectionStringBuilder();

            csb.Host = "127.0.0.1";
            csb.Port = 5432;

            csb.Database = Settings.Database;
            csb.Pooling = false;
            csb.IntegratedSecurity = false;
            if (!csb.IntegratedSecurity)
            {
                csb.Username = Settings.Username;
                csb.Password = Settings.Password;
            } // End if (!csb.IntegratedSecurity) 

            csb.SslMode = Npgsql.SslMode.Disable;
            csb.PersistSecurityInfo = false;

            return csb.ConnectionString;
        } // End Function GetConnectionString 


        public static void CreateDb(string dbName)
        {

            string sql = @"
CREATE DATABASE " + dbName + @"
  WITH OWNER = postgres
       ENCODING = 'UTF8'
       TABLESPACE = pg_default
       LC_COLLATE = 'C'
       LC_CTYPE = 'C'
       CONNECTION LIMIT = -1;
";

            // sql = "CREATE ROLE \"" + System.Environment.UserName + "\" WITH PASSWORD 'TopSecret' LOGIN SUPERUSER CREATEDB CREATEROLE REPLICATION VALID UNTIL 'infinity';";
            // System.Console.WriteLine(sql);

            using (Npgsql.NpgsqlConnection con = new Npgsql.NpgsqlConnection(GetConnectionString()))
            {

                using (var cmd = con.CreateCommand())
                {
                    if (con.State != System.Data.ConnectionState.Open)
                        con.Open();

                    cmd.CommandText = "SELECT COUNT(*) FROM pg_database WHERE datname = '" + dbName.Replace("'", "''") + "'";
                    long dbCount = (long)cmd.ExecuteScalar();

                    if (dbCount > 0)
                    {
                        cmd.CommandText = @"SELECT pg_terminate_backend(pg_stat_activity.pid) 
FROM pg_stat_activity 
WHERE pg_stat_activity.datname = '" + dbName.Replace("'", "''") + @"' 
AND pid <> pg_backend_pid();";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "DROP DATABASE " + dbName + ";";
                        cmd.ExecuteNonQuery();
                    } // End if (dbCount > 0) 

                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                    if (con.State != System.Data.ConnectionState.Closed)
                        con.Close();
                } // End Using cmd 

            } // End using con 

        } // End Sub CreateDb 


    } // End Class SQL 


} // End Namespace PgBackupAndRestore 
