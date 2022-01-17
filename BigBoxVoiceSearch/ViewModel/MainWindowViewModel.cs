using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.ComponentModel;
using Unbroken.LaunchBox.Plugins;
using BigBoxVoiceSearch.Helpers;
using BigBoxVoiceSearch.Models;
using BigBoxVoiceSearch.VoiceSearch;

namespace BigBoxVoiceSearch.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private VoiceSearcher voiceSearcher;
        private BigBoxVoiceSearchState state;

        private Uri initializingImage;
        private Uri initializingFailedImage;
        private Uri inactiveImage;
        private Uri activeImage;
        private Uri recognizingImage;

        private bool showInitializing;
        private bool showInitializingFailed;
        private bool showInactive;
        private bool showActive;
        private bool showRecognizing;

        // specifies how the control is activated - up, down, left, right, page up, page down
        // passed in from dependency property on the view
        public string ActivationMode { get; set; }

        // custom images for each state allow override to default images
        public string CustomInitializingImagePath { get; set; }
        public string CustomInitializingFailedImagePath { get; set; }
        public string CustomInactiveImagePath { get; set; }
        public string CustomActiveImagePath { get; set; }
        public string CustomRecognizingImagePath { get; set; }

        public void Initialize()
        {
            try
            {
                State = BigBoxVoiceSearchState.Initializing;

                BackgroundWorker backgroundWorker = new BackgroundWorker();
                backgroundWorker.DoWork += BackgroundWorker_DoWork;
                backgroundWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                State = BigBoxVoiceSearchState.InitializingFailed;
                LogHelper.LogException(ex, "MainWindowViewModel.Initialize");
            }
        }

        private async void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            InitializeImages();

            voiceSearcher = VoiceSearcher.Instance;
            voiceSearcher.RecognitionCompletedDelegate = RecognizeCompleted;

            if (!voiceSearcher.IsInitialized)
            {
                State = await voiceSearcher.Initialize()
                    ? BigBoxVoiceSearchState.Inactive
                    : BigBoxVoiceSearchState.InitializingFailed;
            }
            else
            {
                State = BigBoxVoiceSearchState.Inactive;
            }
        }

        private void InitializeImages()
        {
            if (File.Exists(Path.Combine(DirectoryInfoHelper.Instance.ApplicationPath, CustomInitializingImagePath)))
            {
                InitializingImage = new Uri(Path.Combine(DirectoryInfoHelper.Instance.ApplicationPath, CustomInitializingImagePath));
            }
            else if (File.Exists(DirectoryInfoHelper.Instance.InitializingImagePath))
            {
                InitializingImage = new Uri(DirectoryInfoHelper.Instance.InitializingImagePath);
            }

            if (File.Exists(Path.Combine(DirectoryInfoHelper.Instance.ApplicationPath, CustomInitializingFailedImagePath)))
            {
                InitializingFailedImage = new Uri(Path.Combine(DirectoryInfoHelper.Instance.ApplicationPath, CustomInitializingFailedImagePath));
            }
            else if (File.Exists(DirectoryInfoHelper.Instance.InitializingFailedImagePath))
            {
                InitializingFailedImage = new Uri(DirectoryInfoHelper.Instance.InitializingFailedImagePath);
            }

            if (File.Exists(Path.Combine(DirectoryInfoHelper.Instance.ApplicationPath, CustomInactiveImagePath)))
            {
                InactiveImage = new Uri(Path.Combine(DirectoryInfoHelper.Instance.ApplicationPath, CustomInactiveImagePath));
            }
            else if (File.Exists(DirectoryInfoHelper.Instance.InactiveImagePath))
            {
                InactiveImage = new Uri(DirectoryInfoHelper.Instance.InactiveImagePath);
            }

            if (File.Exists(Path.Combine(DirectoryInfoHelper.Instance.ApplicationPath, CustomActiveImagePath)))
            {
                ActiveImage = new Uri(Path.Combine(DirectoryInfoHelper.Instance.ApplicationPath, CustomActiveImagePath));
            }
            else if (File.Exists(DirectoryInfoHelper.Instance.ActiveImagePath))
            {
                ActiveImage = new Uri(DirectoryInfoHelper.Instance.ActiveImagePath);
            }

            if (File.Exists(Path.Combine(DirectoryInfoHelper.Instance.ApplicationPath, CustomRecognizingImagePath)))
            {
                RecognizingImage = new Uri(Path.Combine(DirectoryInfoHelper.Instance.ApplicationPath, CustomRecognizingImagePath));
            }
            else if (File.Exists(DirectoryInfoHelper.Instance.RecognizingImagePath))
            {
                RecognizingImage = new Uri(DirectoryInfoHelper.Instance.RecognizingImagePath);
            }
        }


        public bool ShowInitializing
        {
            get => showInitializing;
            set
            {
                showInitializing = value;
                OnPropertyChanged("ShowInitializing");
            }
        }

        public bool ShowInitializingFailed
        {
            get => showInitializingFailed;
            set
            {
                showInitializingFailed = value;
                OnPropertyChanged("ShowInitializingFailed");
            }

        }

        public bool ShowInactive
        {
            get => showInactive;
            set
            {
                showInactive = value;
                OnPropertyChanged("ShowInactive");
            }
        }

        public bool ShowActive
        {
            get => showActive;
            set
            {
                showActive = value;
                OnPropertyChanged("ShowActive");
            }
        }

        public bool ShowRecognizing
        {
            get => showRecognizing;
            set
            {
                showRecognizing = value;
                OnPropertyChanged("ShowRecognizing");
            }
        }

        public Uri InitializingImage
        {
            get => initializingImage;
            set
            {
                initializingImage = value;
                OnPropertyChanged("InitializingImage");
            }
        }

        public Uri InitializingFailedImage
        {
            get => initializingFailedImage;
            set
            {
                initializingFailedImage = value;
                OnPropertyChanged("InitializingFailedImage");
            }
        }

        public Uri InactiveImage
        {
            get => inactiveImage;
            set
            {
                inactiveImage = value;
                OnPropertyChanged("InactiveImage");
            }
        }

        public Uri ActiveImage
        {
            get => activeImage;
            set
            {
                activeImage = value;
                OnPropertyChanged("ActiveImage");
            }
        }

        public Uri RecognizingImage
        {
            get => recognizingImage;
            set
            {
                recognizingImage = value;
                OnPropertyChanged("RecognizingImage");
            }
        }

        public BigBoxVoiceSearchState State
        {
            get => state;
            set
            {
                state = value;
                OnPropertyChanged("State");
            }
        }

        public bool DoUp(bool held)
        {
            if (ActivationMode == BigBoxVoiceSearchActivationMode.Up && State == BigBoxVoiceSearchState.Inactive)
            {
                State = BigBoxVoiceSearchState.Active;
                return true;
            }

            if (ActivationMode != BigBoxVoiceSearchActivationMode.Up && State == BigBoxVoiceSearchState.Active)
            {
                State = BigBoxVoiceSearchState.Inactive;
            }

            return false;
        }

        public bool DoDown(bool held)
        {
            if (ActivationMode == BigBoxVoiceSearchActivationMode.Down && State == BigBoxVoiceSearchState.Inactive)
            {
                State = BigBoxVoiceSearchState.Active;
                return true;
            }

            if (ActivationMode != BigBoxVoiceSearchActivationMode.Down && State == BigBoxVoiceSearchState.Active)
            {
                State = BigBoxVoiceSearchState.Inactive;
            }

            return false;
        }

        public bool DoLeft(bool held)
        {
            if (ActivationMode == BigBoxVoiceSearchActivationMode.Left && State == BigBoxVoiceSearchState.Inactive)
            {
                State = BigBoxVoiceSearchState.Active;
                return true;
            }

            if (ActivationMode != BigBoxVoiceSearchActivationMode.Left && State == BigBoxVoiceSearchState.Active)
            {
                State = BigBoxVoiceSearchState.Inactive;
            }

            return false;
        }


        public bool DoRight(bool held)
        {
            if (ActivationMode == BigBoxVoiceSearchActivationMode.Right && State == BigBoxVoiceSearchState.Inactive)
            {
                State = BigBoxVoiceSearchState.Active;
                return true;
            }

            if (ActivationMode != BigBoxVoiceSearchActivationMode.Right && State == BigBoxVoiceSearchState.Active)
            {
                State = BigBoxVoiceSearchState.Inactive;
            }

            return false;
        }

        public bool DoEnter()
        {
            if (State == BigBoxVoiceSearchState.Active)
            {
                DoVoiceSearch();
                return true;
            }

            return false;
        }

        public bool DoEscape()
        {
            if (State == BigBoxVoiceSearchState.Recognizing)
            {
                voiceSearcher.TryCancelSearch();
                State = BigBoxVoiceSearchState.Inactive;
                return true;
            }

            if (State == BigBoxVoiceSearchState.Active)
            {
                State = BigBoxVoiceSearchState.Inactive;
            }

            return false;
        }

        public bool DoPageDown()
        {
            if (ActivationMode == BigBoxVoiceSearchActivationMode.PageDown)
            {
                DoVoiceSearch();
                return true;
            }

            return false;
        }

        public bool DoPageUp()
        {
            if (ActivationMode == BigBoxVoiceSearchActivationMode.PageUp)
            {
                DoVoiceSearch();
                return true;
            }

            return false;
        }

        public void DoVoiceSearch()
        {
            if (!voiceSearcher.IsInitialized)
            {
                return;
            }

            State = BigBoxVoiceSearchState.Recognizing;
            voiceSearcher.DoVoiceSearch();
        }

        private void RecognizeCompleted(SpeechRecognizerResult result)
        {
            State = BigBoxVoiceSearchState.Inactive;

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
    }
}
