using BigBoxVoiceSearch.Helpers;
using BigBoxVoiceSearch.Models;
using Newtonsoft.Json;
using System.IO;

namespace BigBoxVoiceSearch.DataAccess
{
    public class BigBoxVoiceSearchDataService
    {
        private readonly string StorageFile = DirectoryInfoHelper.Instance.SettingsFile;

        public static BigBoxVoiceSearchSettings GetSettings()
        {
            return Instance.ReadSettingsFile();
        }

        private BigBoxVoiceSearchSettings ReadSettingsFile()
        {
            // make sure the data file exists 
            if (!File.Exists(StorageFile))
            {
                // make sure the folders exist 
                DirectoryInfoHelper.CreateFolders();

                BigBoxVoiceSearchSettings bigBoxVoiceSearchSettings = new BigBoxVoiceSearchSettings()
                {
                    VoiceSearchTimeoutInSeconds = 5,
                    EnableDebugLog = false,
                    EnableErrorLog = true
                };

                // save the file 
                SaveToFileAsync(bigBoxVoiceSearchSettings);

                return bigBoxVoiceSearchSettings;
            }

            // read and deserialize the file
            string json = File.ReadAllText(StorageFile);
            return JsonConvert.DeserializeObject<BigBoxVoiceSearchSettings>(json);
        }

        private void SaveToFileAsync(BigBoxVoiceSearchSettings bigBoxVoiceSearchSettings)
        {
            string json = JsonConvert.SerializeObject(bigBoxVoiceSearchSettings, Formatting.Indented);
            File.WriteAllText(StorageFile, json);
        }

        #region singleton implementation 
        public static BigBoxVoiceSearchDataService Instance { get; } = new BigBoxVoiceSearchDataService();

        static BigBoxVoiceSearchDataService()
        {
        }

        private BigBoxVoiceSearchDataService()
        {
        }
        #endregion
    }
}
