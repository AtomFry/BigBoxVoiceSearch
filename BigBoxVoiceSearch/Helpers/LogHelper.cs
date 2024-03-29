﻿using BigBoxVoiceSearch.DataAccess;
using System;
using System.IO;

namespace BigBoxVoiceSearch.Helpers
{
    class LogHelper
    {
        public static string LogFile = DirectoryInfoHelper.Instance.LogFile;

        public static void Log(string logMessage)
        {
            if (!File.Exists(LogFile))
            {
                DirectoryInfoHelper.CreateFileIfNotExists(LogFile);
            }

            using (StreamWriter w = File.AppendText(LogFile))
            {
                if (BigBoxVoiceSearchSettingsDataProvider.Instance.BigBoxVoiceSearchSettings.EnableDebugLog)
                {
                    w.Write("\r\nLog Entry : ");
                    w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                    w.WriteLine($"  :{logMessage}");
                    w.WriteLine("-------------------------------");
                }
            }
        }

        public static void LogException(Exception ex, string context)
        {
            if (ex != null)
            {
                if (BigBoxVoiceSearchSettingsDataProvider.Instance.BigBoxVoiceSearchSettings.EnableErrorLog)
                {
                    Log($"An exception occurred while attempting to {context}");
                    Log($"Exception message: {ex?.Message ?? "null"}");
                    Log($"Exception stack: {ex?.StackTrace ?? "null"}");

                    if (ex.InnerException != null)
                    {
                        LogException(ex.InnerException, "Inner Exception");
                    }
                }
            }
        }
    }
}
