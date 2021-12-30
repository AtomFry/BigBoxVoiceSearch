using BigBoxVoiceSearch.Models;

namespace BigBoxVoiceSearch.DataAccess
{
    public class BigBoxVoiceSearchSettingsDataProvider
    {
        private BigBoxVoiceSearchSettings bigBoxVoiceSearchSettings;
        public BigBoxVoiceSearchSettings BigBoxVoiceSearchSettings
        {
            get
            {
                if (bigBoxVoiceSearchSettings == null)
                {
                    bigBoxVoiceSearchSettings = BigBoxVoiceSearchDataService.GetSettings();
                }
                return bigBoxVoiceSearchSettings;
            }
        }

        #region singleton implementation 
        public static BigBoxVoiceSearchSettingsDataProvider Instance { get; } = new BigBoxVoiceSearchSettingsDataProvider();

        static BigBoxVoiceSearchSettingsDataProvider()
        {
        }

        private BigBoxVoiceSearchSettingsDataProvider()
        {
        }
        #endregion
    }
}
