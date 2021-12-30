using BigBoxVoiceSearch.EmbeddedResources;
using BigBoxVoiceSearch.VoiceSearch;
using System;
using System.Linq;
using System.Threading.Tasks;
using Unbroken.LaunchBox.Plugins;

namespace BigBoxVoiceSearch.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private VoiceSearcher voiceSearcher;
        private bool isRecognizing;

        public async Task Initialize()
        {
            voiceSearcher = new VoiceSearcher(RecognizeCompleted);
            await voiceSearcher.Initialize();
        }

        public void DoVoiceSearch()
        {
            if(!voiceSearcher.IsInitialized)
            {
                return;
            }

            IsRecognizing = true;
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

        private void RecognizeCompleted(SpeechRecognizerResult result)
        {
            IsRecognizing = false;
            if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
            {
                return;
            }

            IOrderedEnumerable<RecognizedPhrase> recognizedPhrases = result.RecognizedPhrases.OrderByDescending(p => p.Confidence);
            if (!recognizedPhrases.Any())
            {
                return;
            }
            
            RecognizedPhrase recognizedPhrase = recognizedPhrases.FirstOrDefault();
            PluginHelper.BigBoxMainViewModel.Search(recognizedPhrase.Phrase);
        }

        public Uri VoiceRecognitionGif { get; } = ResourceImages.VoiceRecognitionGifPath;

    }
}
