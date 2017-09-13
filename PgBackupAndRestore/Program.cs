
namespace PgBackupAndRestore
{


    public class Program
    {

        
        static void Main(string[] argv)
        {
            string wd = @"E:\ImportData\AllPGDUMP";
            string path_to_pg_restore = @"D:\Programme\LessPortableApps\SQL_PostGreSQL\PostgreSQLPortable\App\PgSQL\bin\pg_restore";


            string[] filez = System.IO.Directory.GetFiles(wd, "*.dump");
            foreach (string filename in filez)
            {
                string dbName = System.IO.Path.GetFileNameWithoutExtension(filename);
                SQL.CreateDb(dbName);

                using (System.Diagnostics.Process p = new System.Diagnostics.Process())
                {

                    string args = "-c -d [database_name] [dumpfile_name]";
                    args = args.Replace("[database_name]", dbName).Replace("[dumpfile_name]", filename);

                    System.Console.WriteLine("Starting " + args);

                    p.StartInfo = new System.Diagnostics.ProcessStartInfo(path_to_pg_restore, args);
                    p.StartInfo.WorkingDirectory = wd;

                    // p.StartInfo.EnvironmentVariables = System.Environment.GetEnvironmentVariables();
                    // p.StartInfo.EnvironmentVariables.Add("key", "value");

                    p.Start();
                    p.WaitForExit();

                    System.Console.WriteLine("Finished " + args);
                    System.Console.WriteLine(System.Environment.NewLine);
                    System.Console.WriteLine(System.Environment.NewLine);
                } // End using p 

            } // Next filename 


            System.Console.WriteLine(System.Environment.NewLine);
            System.Console.WriteLine(" --- Press any key to continue --- ");
            System.Console.ReadKey();
        } // End Sub Main 


    } // End Class Program 


} // End Namespace PgBackupAndRestore 
