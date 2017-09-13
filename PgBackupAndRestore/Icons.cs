
// https://stackoverflow.com/questions/69761/how-to-associate-a-file-extension-to-the-current-executable-in-c-sharp
// https://msdn.microsoft.com/en-us/library/windows/desktop/hh127427(v=vs.85).aspx
// https://msdn.microsoft.com/en-us/library/windows/desktop/bb762118(v=vs.85).aspx
// http://nsis.sourceforge.net/Refresh_shell_icons
// https://stackoverflow.com/questions/16829736/windows-changing-the-name-icon-of-an-application-associated-with-a-file-type
// https://stackoverflow.com/questions/16829736/windows-changing-the-name-icon-of-an-application-associated-with-a-file-type
namespace PgBackupAndRestore
{

    public class Icons
    {

        // https://stackoverflow.com/questions/16829736/windows-changing-the-name-icon-of-an-application-associated-with-a-file-type
        [System.Runtime.InteropServices.DllImport("Shell32.dll")]
        private static extern int SHChangeNotify(int eventId, int flags, System.IntPtr item1, System.IntPtr item2);


        private const int SHCNE_ASSOCCHANGED = 0x8000000;
        private const int SHCNF_FLUSH = 0x1000;



        public static void testc()
        {
            // https://stackoverflow.com/questions/647270/how-to-refresh-the-windows-desktop-programmatically-i-e-f5-from-c
            // const int SHCNE_ASSOCCHANGED = 0x08000000;
            // SHChangeNotify(0x8000000, 0x1000, System.IntPtr.Zero, System.IntPtr.Zero);
            // SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_FLUSH, System.IntPtr.Zero, System.IntPtr.Zero);

        }
    }


    public class FileAssociation
    {
        public string Extension { get; set; }
        public string ProgId { get; set; }
        public string FileTypeDescription { get; set; }
        public string ExecutableFilePath { get; set; }
    }



    public class FileAssociations
    {
        // needed so that Explorer windows get refreshed after the registry is updated
        [System.Runtime.InteropServices.DllImport("Shell32.dll")]
        private static extern int SHChangeNotify(int eventId, int flags, System.IntPtr item1, System.IntPtr item2);

        private const int SHCNE_ASSOCCHANGED = 0x8000000;
        private const int SHCNF_FLUSH = 0x1000;

        public static void EnsureAssociationsSet()
        {
            var filePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            EnsureAssociationsSet(
                new FileAssociation
                {
                    Extension = ".binlog",
                    ProgId = "MSBuildBinaryLog",
                    FileTypeDescription = "MSBuild Binary Log",
                    ExecutableFilePath = filePath
                },
                new FileAssociation
                {
                    Extension = ".buildlog",
                    ProgId = "MSBuildStructuredLog",
                    FileTypeDescription = "MSBuild Structured Log",
                    ExecutableFilePath = filePath
                });
        }

        public static void EnsureAssociationsSet(params FileAssociation[] associations)
        {
            bool madeChanges = false;
            foreach (var association in associations)
            {
                madeChanges |= SetAssociation(
                    association.Extension,
                    association.ProgId,
                    association.FileTypeDescription,
                    association.ExecutableFilePath);
            }

            if (madeChanges)
            {
                SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_FLUSH, System.IntPtr.Zero, System.IntPtr.Zero);
            }
        }

        public static bool SetAssociation(string extension, string progId, string fileTypeDescription, string applicationFilePath)
        {
            bool madeChanges = false;
            madeChanges |= SetKeyDefaultValue(@"Software\Classes\" + extension, progId);
            madeChanges |= SetKeyDefaultValue(@"Software\Classes\" + progId, fileTypeDescription);
            madeChanges |= SetKeyDefaultValue($@"Software\Classes\{progId}\shell\open\command", "\"" + applicationFilePath + "\" \"%1\"");
            return madeChanges;
        }

        private static bool SetKeyDefaultValue(string keyPath, string value)
        {
            
            //using (var key = Registry.CurrentUser.CreateSubKey(keyPath))
            //{
            //    if (key.GetValue(null) as string != value)
            //    {
            //        key.SetValue(null, value);
            //        return true;
            //    }
            //}

            return false;
        }


    }

}

