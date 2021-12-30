using BigBoxVoiceSearch.EmbeddedResources;
using BigBoxVoiceSearch.Helpers;
using BigBoxVoiceSearch.Models;
using BigBoxVoiceSearch.VoiceSearch;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unbroken.LaunchBox.Plugins;

namespace BigBoxVoiceSearch.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private VoiceSearcher voiceSearcher;
        private bool isRecognizing;
        private bool isActive;
        private Visibility userControlVisibility;
        private string visibilityMode;


        public string ActivationMode { get; set; }

        public string VisibilityMode
        {
            get { return visibilityMode; } 
            set
            {
                visibilityMode = value;
                ManageVisibility();
            }
        }

        public async Task Initialize()
        {
            IsActive = false;
            IsRecognizing = false;

            // initialize the gif with the custom path if it exists
            VoiceRecognitionGif = File.Exists(DirectoryInfoHelper.Instance.CustomGifPath)
                ? new Uri(DirectoryInfoHelper.Instance.CustomGifPath)
                : ResourceImages.VoiceRecognitionGifPath;

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


        public bool DoUp(bool held)
        {
            bool eventHandled = false;

            if (ActivationMode == BigBoxVoiceSearchActivationMode.Up)
            {
                IsActive = true;
                eventHandled = true;
            }
            else if (ActivationMode == BigBoxVoiceSearchActivationMode.Down)
            {
                IsActive = false;
                eventHandled = true;
            }

            return eventHandled;
        }


        public bool DoDown(bool held)
        {
            bool eventHandled = false;

            if (ActivationMode == BigBoxVoiceSearchActivationMode.Down)
            {
                IsActive = true;
                eventHandled = true;
            }
            else if (ActivationMode == BigBoxVoiceSearchActivationMode.Up)
            {
                IsActive = false;
                eventHandled = true;
            }

            return eventHandled;
        }

        public bool DoLeft(bool held)
        {
            bool eventHandled = false;

            if (ActivationMode == BigBoxVoiceSearchActivationMode.Left)
            {
                IsActive = true;
                eventHandled = true;
            }
            else if (ActivationMode == BigBoxVoiceSearchActivationMode.Right)
            {
                IsActive = false;
                eventHandled = true;
            }

            return eventHandled;
        }


        public bool DoRight(bool held)
        {
            bool eventHandled = false;

            if (ActivationMode == BigBoxVoiceSearchActivationMode.Right)
            {
                IsActive = true;
                eventHandled = true;
            }
            else if (ActivationMode == BigBoxVoiceSearchActivationMode.Left)
            {
                IsActive = false;
                eventHandled = true;
            }

            return eventHandled;
        }


        public bool DoEnter()
        {
            bool eventHandled = false;

            if (IsActive)
            {
                DoVoiceSearch();
                eventHandled = true;
            }

            return eventHandled;
        }

        public bool DoEscape()
        {
            bool eventHandled = false;
            // not handling escape
            return eventHandled;
        }

        public bool DoPageDown()
        {
            bool eventHandled = false;

            if (ActivationMode == BigBoxVoiceSearchActivationMode.PageDown)
            {
                DoVoiceSearch();
                eventHandled = true;
            }

            return eventHandled;
        }

        public bool DoPageUp()
        {
            bool eventHandled = false;

            if (ActivationMode == BigBoxVoiceSearchActivationMode.PageUp)
            {
                DoVoiceSearch();
                eventHandled = true;
            }

            return eventHandled;
        }

        public bool IsRecognizing
        {
            get { return isRecognizing; }
            set
            {
                isRecognizing = value;
                OnPropertyChanged("IsRecognizing");

                ManageVisibility();
            }
        }

        public bool IsActive
        {
            get { return isActive; }
            set
            {
                isActive = value;
                OnPropertyChanged("IsActive");

                ManageVisibility();
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

        private Uri voiceRecognitionGif;
        public Uri VoiceRecognitionGif
        {
            get { return voiceRecognitionGif; } 
            set
            {
                voiceRecognitionGif = value;
                OnPropertyChanged("VoiceRecognitionGif");
            }
        }

        public Visibility UserControlVisibility
        {
            get { return userControlVisibility; }
            set
            {
                userControlVisibility = value;
                OnPropertyChanged("UserControlVisibility");
            }
        }

        private void ManageVisibility()
        {
            switch (VisibilityMode)
            {
                case BigBoxVoiceSearchVisibilityMode.Never:
                    UserControlVisibility = Visibility.Collapsed;
                    break;

                case BigBoxVoiceSearchVisibilityMode.Always:
                    UserControlVisibility = Visibility.Visible;
                    break;

                case BigBoxVoiceSearchVisibilityMode.Active:
                    UserControlVisibility = IsActive
                        ? Visibility.Visible
                        : Visibility.Collapsed;
                    break;

                case BigBoxVoiceSearchVisibilityMode.Recognizing:
                    UserControlVisibility = IsRecognizing
                        ? Visibility.Visible
                        : Visibility.Collapsed;
                    break;

                default:
                    UserControlVisibility = Visibility.Collapsed;
                    break;
            }
        }

    }
}
