using System;
using System.Collections.Generic;
using System.Text;

namespace BigBoxVoiceSearch.EmbeddedResources
{
    public class ResourceImages
    {
        private static readonly string ResourceFolder = "pack://application:,,,/BigBoxVoiceSearch;component/EmbeddedResources";
        public static Uri InitializingIconPath { get; } = new Uri($"{ResourceFolder}/InitializingIcon.png");
        public static Uri InitializingFailedIconPath { get; } = new Uri($"{ResourceFolder}/InitializingFailedIcon.png");
        public static Uri ReadyIconPath { get; } = new Uri($"{ResourceFolder}/ReadyIcon.png");
        public static Uri RecognizingIconPath { get; } = new Uri($"{ResourceFolder}/RecognizingIcon.png");
        public static Uri RecognitionFailedIconPath { get; } = new Uri($"{ResourceFolder}/RecognizingFailedIcon.png");
        public static Uri VoiceRecognitionGifPath { get; } = new Uri($"{ResourceFolder}/VoiceRecognitionGif.gif");
    }
}
