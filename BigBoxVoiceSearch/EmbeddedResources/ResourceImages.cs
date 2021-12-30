using System;
using System.Collections.Generic;
using System.Text;

namespace BigBoxVoiceSearch.EmbeddedResources
{
    public class ResourceImages
    {
        private static readonly string ResourceFolder = "pack://application:,,,/BigBoxVoiceSearch;component/EmbeddedResources";
        public static Uri VoiceRecognitionGifPath { get; } = new Uri($"{ResourceFolder}/VoiceRecognitionGif.gif");
    }
}
