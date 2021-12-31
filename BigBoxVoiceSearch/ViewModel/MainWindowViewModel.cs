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

        private bool activateOnUp, activateOnDown, activateOnLeft, activateOnRight, activateOnPageUp, activateOnPageDown;
        private bool deactivateOnUp, deactivateOnDown, deactivateOnLeft, deactivateOnRight;

        private Uri inactiveBackgroundImage;
        private Uri activeBackgroundImage;
        private Uri recognizingBackgroundImage;
        private Uri inactiveImage;
        private Uri activeImage;
        private Uri recognizingImage;

        private Uri foregroundImage;
        public Uri ForegroundImage
        {
            get { return foregroundImage; }
            set
            {
                foregroundImage = value;
                OnPropertyChanged("ForegroundImage");
            }
        }

        private Uri backgroundImage;
        public Uri BackgroundImage
        {
            get { return backgroundImage; }
            set
            {
                backgroundImage = value;
                OnPropertyChanged("BackgroundImage");
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

        public string VisibilityMode
        {
            get { return visibilityMode; }
            set
            {
                if(visibilityMode != value)
                {
                    ManageVisibility(value, IsActive, IsRecognizing);
                }
                
                visibilityMode = value;
                OnPropertyChanged("VisibilityMode");
            }
        }

        public async Task Initialize()
        {
            IsActive = false;
            IsRecognizing = false;

            InitializeImages();

            voiceSearcher = new VoiceSearcher(RecognizeCompleted);
            await voiceSearcher.Initialize();
        }

        public void InitializeImages()
        {
            if (File.Exists(DirectoryInfoHelper.Instance.InactiveBackgroundPath))
            {
                inactiveBackgroundImage = new Uri(DirectoryInfoHelper.Instance.InactiveBackgroundPath);
            }

            if (File.Exists(DirectoryInfoHelper.Instance.ActiveBackgroundPath))
            {
                activeBackgroundImage = new Uri(DirectoryInfoHelper.Instance.ActiveBackgroundPath);
            }

            if (File.Exists(DirectoryInfoHelper.Instance.RecognizingBackgroundPath))
            {
                recognizingBackgroundImage = new Uri(DirectoryInfoHelper.Instance.RecognizingBackgroundPath);
            }

            if (File.Exists(DirectoryInfoHelper.Instance.InactiveImagePath))
            {
                inactiveImage = new Uri(DirectoryInfoHelper.Instance.InactiveImagePath);
            }

            if (File.Exists(DirectoryInfoHelper.Instance.ActiveImagePath))
            {
                activeImage = new Uri(DirectoryInfoHelper.Instance.ActiveImagePath);
            }

            if (File.Exists(DirectoryInfoHelper.Instance.RecognizingImagePath))
            {
                recognizingImage = new Uri(DirectoryInfoHelper.Instance.RecognizingImagePath);
            }

            ForegroundImage = inactiveImage;
            BackgroundImage = inactiveBackgroundImage;
        }

        public void DoVoiceSearch()
        {
            if (!voiceSearcher.IsInitialized)
            {
                IsRecognizing = false;
                IsActive = false;
                return;
            }

            IsRecognizing = true;
            voiceSearcher.DoVoiceSearch();
        }


        public bool DoUp(bool held)
        {
            if (activateOnUp && !IsActive)
            {
                IsActive = true;
                return true;
            }

            if (deactivateOnUp && IsActive)
            {
                IsActive = false;
                return true;
            }

            return false;
        }


        public bool DoDown(bool held)
        {
            if (activateOnDown && !IsActive)
            {
                IsActive = true;
                return true;
            }

            if (deactivateOnDown && IsActive)
            {
                IsActive = false;
                return true;
            }

            return false;
        }

        public bool DoLeft(bool held)
        {
            if (activateOnLeft && !IsActive)
            {
                IsActive = true;
                return true;
            }

            if (deactivateOnLeft && IsActive)
            {
                IsActive = false;
                return true;
            }

            return false;
        }


        public bool DoRight(bool held)
        {
            if (activateOnRight && !IsActive)
            {
                IsActive = true;
                return true;
            }

            if (deactivateOnRight && IsActive)
            {
                IsActive = false;
                return true;
            }

            return false;
        }


        public bool DoEnter()
        {
            if (IsActive)
            {
                DoVoiceSearch();
                return true;
            }

            return false;
        }

        public bool DoEscape()
        {
            if(IsRecognizing)
            {                
                voiceSearcher.TryCancelSearch();
                IsRecognizing = false;
                return true;
            }

            if(IsActive)
            {
                IsActive = false;
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

        public bool IsRecognizing
        {
            get { return isRecognizing; }
            set
            {
                if (isRecognizing != value)
                {
                    ManageVisibility(VisibilityMode, IsActive, value);
                    ManageImages(value, IsActive);
                }

                isRecognizing = value;
                OnPropertyChanged("IsRecognizing");
            }
        }

        public bool IsActive
        {
            get { return isActive; }
            set
            {
                if (isActive != value)
                {
                    ManageVisibility(VisibilityMode, value, IsRecognizing);
                    ManageImages(IsRecognizing, value);
                }

                isActive = value;
                OnPropertyChanged("IsActive");
            }
        }

        private void RecognizeCompleted(SpeechRecognizerResult result)
        {
            IsRecognizing = false;
            IsActive = false;

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

        public Visibility UserControlVisibility
        {
            get { return userControlVisibility; }
            set
            {
                if (userControlVisibility != value)
                {
                    userControlVisibility = value;
                    OnPropertyChanged("UserControlVisibility");
                }
            }
        }

        private void ManageImages(bool _isRecognizing, bool _isActive)
        {
            if (_isRecognizing)
            {
                ForegroundImage = recognizingImage;
                BackgroundImage = recognizingBackgroundImage;
            }
            else if (_isActive)
            {
                ForegroundImage = activeImage;
                BackgroundImage = activeBackgroundImage;
            }
            else
            {
                ForegroundImage = inactiveImage;
                BackgroundImage = inactiveBackgroundImage;
            }
        }

        private void ManageVisibility(string _visibilityMode, bool _isActive, bool _isRecognizing)
        {
            switch (_visibilityMode)
            {
                case BigBoxVoiceSearchVisibilityMode.Never:
                    UserControlVisibility = Visibility.Collapsed;
                    break;

                case BigBoxVoiceSearchVisibilityMode.Always:
                    UserControlVisibility = Visibility.Visible;
                    break;

                case BigBoxVoiceSearchVisibilityMode.Active:
                    UserControlVisibility = _isActive
                        ? Visibility.Visible
                        : Visibility.Collapsed;
                    break;

                case BigBoxVoiceSearchVisibilityMode.Recognizing:
                    UserControlVisibility = _isRecognizing
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
