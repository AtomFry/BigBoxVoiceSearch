using BigBoxVoiceSearch.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BigBoxVoiceSearch.DataAccess
{
    public class BigBoxVoiceSearchSettingsDataProvider
    {
        private BigBoxVoiceSearchSettings bigBoxVoiceSearchSettings;
        public BigBoxVoiceSearchSettings BigBoxVoiceSearchSettings 
        {
            get
            {
                if(bigBoxVoiceSearchSettings == null)
                {
                    bigBoxVoiceSearchSettings = BigBoxVoiceSearchDataService.GetSettings();
                }
                return bigBoxVoiceSearchSettings;
            }            
        }

        #region singleton implementation 
        public static BigBoxVoiceSearchSettingsDataProvider Instance
        {
            get
            {
                return instance;
            }
        }

        private static readonly BigBoxVoiceSearchSettingsDataProvider instance = new BigBoxVoiceSearchSettingsDataProvider();

        static BigBoxVoiceSearchSettingsDataProvider()
        {
        }

        private BigBoxVoiceSearchSettingsDataProvider()
        {
        }
        #endregion
    }
}
