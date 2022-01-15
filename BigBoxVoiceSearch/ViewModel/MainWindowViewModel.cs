using BigBoxVoiceSearch.EmbeddedResources;
using BigBoxVoiceSearch.Helpers;
using BigBoxVoiceSearch.Models;
using BigBoxVoiceSearch.VoiceSearch;
using System;
using System.ComponentModel;
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
        private BigBoxVoiceSearchState state;
        private Visibility userControlVisibility;


        public bool ShowInitializing { get; set; }
        public bool ShowInitializingFailed { get; set; }
        public bool ShowInactive { get; set; }
        public bool ShowActive { get; set; }
        public bool ShowRecognizing { get; set; }
        public string CustomInitializingImagePath { get; set; }
        public string CustomInitializingFailedImagePath { get; set; }
        public string CustomInactiveImagePath { get; set; }
        public string CustomActiveImagePath { get; set; }
        public string CustomRecognizingImagePath { get; set; }        

        private bool activateOnUp, activateOnDown, activateOnLeft, activateOnRight, activateOnPageUp, activateOnPageDown;
        private bool deactivateOnUp, deactivateOnDown, deactivateOnLeft, deactivateOnRight;

        private Uri initializingImage;
        private Uri initializingFailedImage;
        private Uri inactiveImage;
        private Uri activeImage;
        private Uri recognizingImage;

        public Uri InitializingImage
        {
            get { return initializingImage; }
            set
            {
                initializingImage = value;
                OnPropertyChanged("InitializingImage");
            }
        }

        public Uri InitializingFailedImage
        {
            get { return initializingFailedImage; }
            set
            {
                initializingFailedImage = value;
                OnPropertyChanged("InitializingFailedImage");
            }
        }

        public Uri InactiveImage
        {
            get { return inactiveImage; }
            set
            {
                inactiveImage = value;
                OnPropertyChanged("InactiveImage");
            }
        }

        public Uri ActiveImage
        {
            get { return activeImage; }
            set
            {
                activeImage = value;
                OnPropertyChanged("ActiveImage");
            }
        }

        public Uri RecognizingImage
        {
            get { return recognizingImage; }
            set
            {
                recognizingImage = value;
                OnPropertyChanged("RecognizingImage");
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

        public BigBoxVoiceSearchState State
        {
            get { return state; }
            set
            {
                if (state != value)
                {
                    state = value;
                    OnPropertyChanged("State");

                    ManageUserControlVisibility();
                }
            }
        }

        private void ManageUserControlVisibility()
        {
            switch (State)
            {
                case BigBoxVoiceSearchState.None:
                    UserControlVisibility = Visibility.Collapsed;
                    break;

                case BigBoxVoiceSearchState.Initializing:
                    UserControlVisibility = ShowInitializing
                        ? Visibility.Visible
                        : Visibility.Collapsed;
                    break;

                case BigBoxVoiceSearchState.InitializingFailed:
                    UserControlVisibility = ShowInitializingFailed
                                            ? Visibility.Visible
                                            : Visibility.Collapsed;
                    break;

                case BigBoxVoiceSearchState.Inactive:
                    UserControlVisibility = ShowInactive
                        ? Visibility.Visible
                        : Visibility.Collapsed;
                    break;

                case BigBoxVoiceSearchState.Active:
                    UserControlVisibility = ShowActive
                                            ? Visibility.Visible
                                            : Visibility.Collapsed;
                    break;

                case BigBoxVoiceSearchState.Recognizing:
                    UserControlVisibility = ShowRecognizing
                                            ? Visibility.Visible
                                            : Visibility.Collapsed;
                    break;

                default:
                    UserControlVisibility = Visibility.Collapsed;
                    break;
            }
        }

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
            await InitializeInBackground();
        }

        private async Task InitializeInBackground()
        {
            InitializeImages();

            voiceSearcher = new VoiceSearcher(RecognizeCompleted);
            if (await voiceSearcher.Initialize())
            {
                State = BigBoxVoiceSearchState.Inactive;
            }
            else
            {
                State = BigBoxVoiceSearchState.InitializingFailed;
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

            if(File.Exists(Path.Combine(DirectoryInfoHelper.Instance.ApplicationPath, CustomInitializingFailedImagePath)))
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

        private string activationMode;
        public string ActivationMode
        {
            get { return activationMode; }
            set
            {
                if (activationMode != value)
                {
                    InitializeActivation(value);
                }

                activationMode = value;
            }
        }

        private void InitializeActivation(string _activationMode)
        {
            activateOnUp = _activationMode == BigBoxVoiceSearchActivationMode.Up;
            activateOnDown = _activationMode == BigBoxVoiceSearchActivationMode.Down;
            activateOnLeft = _activationMode == BigBoxVoiceSearchActivationMode.Left;
            activateOnRight = _activationMode == BigBoxVoiceSearchActivationMode.Right;
            activateOnPageUp = _activationMode == BigBoxVoiceSearchActivationMode.PageUp;
            activateOnPageDown = _activationMode == BigBoxVoiceSearchActivationMode.PageDown;

            deactivateOnUp = activateOnDown;
            deactivateOnDown = activateOnUp;
            deactivateOnLeft = activateOnRight;
            deactivateOnRight = activateOnLeft;
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

        public bool DoUp(bool held)
        {
            if (activateOnUp && State != BigBoxVoiceSearchState.Active)
            {
                State = BigBoxVoiceSearchState.Active;
                return true;
            }

            if (deactivateOnUp && State == BigBoxVoiceSearchState.Active)
            {
                State = BigBoxVoiceSearchState.Inactive;
                return true;
            }

            return false;
        }

        public bool DoDown(bool held)
        {
            if (activateOnDown && State != BigBoxVoiceSearchState.Active)
            {
                State = BigBoxVoiceSearchState.Active;
                return true;
            }

            if (deactivateOnDown && State == BigBoxVoiceSearchState.Active)
            {
                State = BigBoxVoiceSearchState.Inactive;
                return true;
            }

            return false;
        }

        public bool DoLeft(bool held)
        {
            if (activateOnLeft && State != BigBoxVoiceSearchState.Active)
            {
                State = BigBoxVoiceSearchState.Active;
                return true;
            }

            if (deactivateOnLeft && State == BigBoxVoiceSearchState.Active)
            {
                State = BigBoxVoiceSearchState.Inactive;
                return true;
            }

            return false;
        }


        public bool DoRight(bool held)
        {
            if (activateOnRight && State != BigBoxVoiceSearchState.Active)
            {
                State = BigBoxVoiceSearchState.Active;
                return true;
            }

            if (deactivateOnRight && State == BigBoxVoiceSearchState.Active)
            {
                State = BigBoxVoiceSearchState.Inactive;
                return true;
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
                return true;
            }

            return false;
        }

        public bool DoPageDown()
        {
            if (activateOnPageDown)
            {
                DoVoiceSearch();
                return true;
            }

            return false;
        }

        public bool DoPageUp()
        {
            if (activateOnPageUp)
            {
                DoVoiceSearch();
                return true;
            }

            return false;
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
