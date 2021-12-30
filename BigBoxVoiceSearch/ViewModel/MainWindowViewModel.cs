using BigBoxVoiceSearch.EmbeddedResources;
using BigBoxVoiceSearch.VoiceSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unbroken.LaunchBox.Plugins;

namespace BigBoxVoiceSearch.ViewModel
{
    public enum RecognitionStatus
    {
        None,
        Initializing,
        InitializationFailed,
        Ready,
        Recognizing,
        RecognitionFailed,
        RecognitionNoResult
    }

    public class MainWindowViewModel : ViewModelBase
    {
        private VoiceSearcher voiceSearcher;
        private bool isRecognizing;
        private string recognitionStatusMessage;
        private RecognitionStatus recognitionStatus;
        
        public Uri VoiceRecognitionGif { get; } = ResourceImages.VoiceRecognitionGifPath;

        public MainWindowViewModel()
        {
            RecognitionStatus = RecognitionStatus.None;
        }

        public async Task Initialize()
        {
            voiceSearcher = new VoiceSearcher(RecognizeCompleted);

            RecognitionStatus = RecognitionStatus.Initializing;
            RecognitionStatusMessage = "Initializing";

            if (await voiceSearcher.Initialize())
            {
                RecognitionStatus = RecognitionStatus.Ready;
                RecognitionStatusMessage = "Ready";
            }
            else
            {
                RecognitionStatus = RecognitionStatus.InitializationFailed;
                RecognitionStatusMessage = "Failed to initialize";
            }
        }

        public void DoVoiceSearch()
        {
            if(!voiceSearcher.IsInitialized)
            {
                return;
            }

            IsRecognizing = true;
            RecognitionStatus = RecognitionStatus.Recognizing;
            RecognitionStatusMessage = "Recognizing";

            voiceSearcher.DoVoiceSearch();
        }

        public bool IsRecognizing
        {
            get { return isRecognizing; }
            set
            {
                isRecognizing = value;
                OnPropertyChanged("IsRecognizing");
            }
        }

        public string RecognitionStatusMessage
        {
            get { return recognitionStatusMessage; }
            set
            {
                recognitionStatusMessage = value;
                OnPropertyChanged("RecognitionStatusMessage");
            }
        }

        public RecognitionStatus RecognitionStatus
        {
            get { return recognitionStatus; }
            set
            {
                recognitionStatus = value;
                OnPropertyChanged("RecognitionStatus");
            }
        }
        void RecognizeCompleted(SpeechRecognizerResult result)
        {
            IsRecognizing = false;
            if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
            {
                RecognitionStatusMessage = result.ErrorMessage;
                RecognitionStatus = RecognitionStatus.RecognitionFailed;
            }

            IOrderedEnumerable<RecognizedPhrase> recognizedPhrases = result.RecognizedPhrases.OrderByDescending(p => p.Confidence);
            if (!recognizedPhrases.Any())
            {
                RecognitionStatusMessage = "No results";
                RecognitionStatus = RecognitionStatus.RecognitionNoResult;
            }
            else
            {
                RecognizedPhrase recognizedPhrase = recognizedPhrases.FirstOrDefault();
                PluginHelper.BigBoxMainViewModel.Search(recognizedPhrase.Phrase);

                RecognitionStatusMessage = "Ready";
                RecognitionStatus = RecognitionStatus.Ready;
            }
        }
    }
}
