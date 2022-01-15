using System.IO;

namespace BigBoxVoiceSearch.Helpers
{
    public sealed class DirectoryInfoHelper
    {
        private static readonly DirectoryInfoHelper instance = new DirectoryInfoHelper();

        // path to the big box application directory
        private string applicationPath;
        public string ApplicationPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(applicationPath))
                {
                    applicationPath = Directory.GetCurrentDirectory();
                }

                return applicationPath;
            }
        }

        private string pluginFolder;
        public string PluginFolder
        {
            get
            {
                if (string.IsNullOrWhiteSpace(pluginFolder))
                {
                    pluginFolder = $"{ApplicationPath}\\Plugins\\BigBoxVoiceSearch";
                }
                return pluginFolder;
            }
        }

        private string mediaFolder;
        public string MediaFolder
        {
            get
            {
                if(string.IsNullOrWhiteSpace(mediaFolder))
                {
                    mediaFolder = $"{PluginFolder}\\Media";
                }
                return mediaFolder;
            }
        }

        private string initializingImagePath;
        public string InitializingImagePath
        {
            get
            {
                if(string.IsNullOrWhiteSpace(initializingImagePath))
                {
                    initializingImagePath = $"{MediaFolder}\\Default\\Initializing.png";
                }
                return initializingImagePath;
            }
        }

        private string initializingFailedImagePath;
        public string InitializingFailedImagePath
        {
            get
            {
                if(string.IsNullOrWhiteSpace(initializingFailedImagePath))
                {
                    initializingFailedImagePath = $"{MediaFolder}\\Default\\InitializingFailed.png";
                }
                return initializingFailedImagePath;
            }
        }

        private string inactiveImagePath;
        public string InactiveImagePath
        {
            get
            {
                if(string.IsNullOrWhiteSpace(inactiveImagePath))
                {
                    inactiveImagePath = $"{MediaFolder}\\Default\\Inactive.png";
                }
                return inactiveImagePath;
            }
        }

        private string activeImagePath;
        public string ActiveImagePath
        {
            get
            {
                if(string.IsNullOrWhiteSpace(activeImagePath))
                {
                    activeImagePath = $"{MediaFolder}\\Default\\Active.png";
                }
                return activeImagePath;
            }
        }

        private string recognizingImagePath;
        public string RecognizingImagePath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(recognizingImagePath))
                {
                    recognizingImagePath = $"{MediaFolder}\\Default\\Recognizing.png";
                }
                return recognizingImagePath;
            }
        }

        private string logFile;
        public string LogFile
        {
            get
            {
                if (string.IsNullOrWhiteSpace(logFile))
                {
                    logFile = $"{PluginFolder}\\log.txt";
                }
                return logFile;
            }
        }

        private string settingsFile;
        public string SettingsFile
        {
            get
            {
                if (string.IsNullOrWhiteSpace(settingsFile))
                {
                    settingsFile = $"{PluginFolder}\\settings.json";
                }
                return settingsFile;
            }
        }

        public static void CreateFolders()
        {
            CreateFolder(Instance.PluginFolder);
        }

        public static void CreateFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static void CreateFileIfNotExists(string filePath)
        {
            if (!File.Exists(filePath))
            {
                CreateDirectoryIfNotExists(filePath);

                File.WriteAllText(filePath, "");
            }
        }

        public static void CreateDirectoryIfNotExists(string filePath)
        {
            string folderPath = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }



        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static DirectoryInfoHelper()
        {
        }

        private DirectoryInfoHelper()
        {

        }

        public static DirectoryInfoHelper Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
