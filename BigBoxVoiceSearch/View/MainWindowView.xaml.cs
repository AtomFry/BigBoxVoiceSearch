using System.Windows;
using System.Windows.Controls;
using BigBoxVoiceSearch.Models;
using BigBoxVoiceSearch.ViewModel;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace BigBoxVoiceSearch.View
{
    public partial class MainWindowView : UserControl, IBigBoxThemeElementPlugin
    {
        private readonly MainWindowViewModel mainWindowViewModel;

        public static readonly DependencyProperty ActivationModeProperty =
            DependencyProperty.Register("ActivationMode", typeof(string), typeof(MainWindowView), new PropertyMetadata(BigBoxVoiceSearchActivationMode.Off));

        public static readonly DependencyProperty VisibilityModeProperty =
            DependencyProperty.Register("VisibilityMode", typeof(string), typeof(MainWindowView), new PropertyMetadata(BigBoxVoiceSearchVisibilityMode.Never));

       public static readonly DependencyProperty InitializingImagePathProperty =
            DependencyProperty.Register("InitializingImagePath", typeof(string), typeof(MainWindowView), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty InactiveImagePathProperty =
            DependencyProperty.Register("InactiveImagePath", typeof(string), typeof(MainWindowView), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty ActiveImagePathProperty =
            DependencyProperty.Register("ActiveImagePath", typeof(string), typeof(MainWindowView), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty RecognizingImagePathProperty =
            DependencyProperty.Register("RecognizingImagePath", typeof(string), typeof(MainWindowView), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty RecognizingAnimatedGifPathProperty =
            DependencyProperty.Register("RecognizingAnimatedGifPath", typeof(string), typeof(MainWindowView), new PropertyMetadata(string.Empty));


        public MainWindowView()
        {
            InitializeComponent();

            mainWindowViewModel = new MainWindowViewModel();
            DataContext = mainWindowViewModel;

            Loaded += MainWindowView_Loaded;
        }

        private async void MainWindowView_Loaded(object sender, RoutedEventArgs e)
        {
            // set the activation mode in the view model 
            InitializeMainWindowViewModelActivationMode();

            // set the visibility mode in the view model 
            InitializeMainWindowViewModelVisibilityMode();

            // set custom image paths in the view model
            InitializeMainWindowViewModelCustomInitializingImagePath();
            InitializeMainWindowViewModelCustomInactiveImagePath();
            InitializeMainWindowViewModelCustomActiveImagePath();
            InitializeMainWindowViewModelCustomRecognizingImagePath();
            InitializeMainWindowViewModelCustomRecognizingAnimatedGifPath();

            // ask the view model to set itself up
            await mainWindowViewModel.Initialize();
        }

        public string ActivationMode
        {
            get { return (string)GetValue(ActivationModeProperty); }
            set
            {
                SetValue(ActivationModeProperty, value);

                // set the activation mode in the view model 
                InitializeMainWindowViewModelActivationMode();
            }
        }

        private void InitializeMainWindowViewModelActivationMode()
        {
            if (mainWindowViewModel != null)
            {
                mainWindowViewModel.ActivationMode = ActivationMode;
            }
        }

        public string VisibilityMode
        {
            get { return (string)GetValue(VisibilityModeProperty); }
            set
            {
                SetValue(VisibilityModeProperty, value);

                // set the visibility mode in the view model 
                InitializeMainWindowViewModelVisibilityMode();
            }
        }

        private void InitializeMainWindowViewModelVisibilityMode()
        {
            if (mainWindowViewModel != null)
            {
                mainWindowViewModel.VisibilityMode = VisibilityMode;
            }
        }

        public string InitializingImagePath
        {
            get { return (string)GetValue(InitializingImagePathProperty); }
            set
            {
                SetValue(InitializingImagePathProperty, value);
                InitializeMainWindowViewModelCustomInitializingImagePath();
            }
        }

        private void InitializeMainWindowViewModelCustomInitializingImagePath()
        {
            if (mainWindowViewModel != null)
            {
                mainWindowViewModel.CustomInitializingImagePath = InitializingImagePath;
            }
        }

        public string InactiveImagePath
        {
            get { return (string)GetValue(InactiveImagePathProperty); }
            set
            {
                SetValue(InactiveImagePathProperty, value);
                InitializeMainWindowViewModelCustomInactiveImagePath();
            }
        }

        private void InitializeMainWindowViewModelCustomInactiveImagePath()
        {
            if (mainWindowViewModel != null)
            {
                mainWindowViewModel.CustomInactiveImagePath = InactiveImagePath;
            }
        }

        public string ActiveImagePath
        {
            get { return (string)GetValue(ActiveImagePathProperty); }
            set
            {
                SetValue(ActiveImagePathProperty, value);
                InitializeMainWindowViewModelCustomActiveImagePath();
            }
        }

        private void InitializeMainWindowViewModelCustomActiveImagePath()
        {
            if (mainWindowViewModel != null)
            {
                mainWindowViewModel.CustomActiveImagePath = ActiveImagePath;
            }
        }

        public string RecognizingImagePath
        {
            get { return (string)GetValue(RecognizingImagePathProperty); }
            set
            {
                SetValue(RecognizingImagePathProperty, value);
                InitializeMainWindowViewModelCustomRecognizingImagePath();
            }
        }

        private void InitializeMainWindowViewModelCustomRecognizingImagePath()
        {
            if (mainWindowViewModel != null)
            {
                mainWindowViewModel.CustomRecognizingImagePath = RecognizingImagePath;
            }
        }

        public string RecognizingAnimatedGifPath
        {
            get { return (string)GetValue(RecognizingAnimatedGifPathProperty); }
            set 
            { 
                SetValue(RecognizingAnimatedGifPathProperty, value);
                InitializeMainWindowViewModelCustomRecognizingAnimatedGifPath();
            }
        }

        private void InitializeMainWindowViewModelCustomRecognizingAnimatedGifPath()
        {
            if (mainWindowViewModel != null)
            {
                mainWindowViewModel.CustomRecognizingAnimatedGifPath = RecognizingAnimatedGifPath;
            }
        }

        public bool OnDown(bool held)
        {
            return mainWindowViewModel.DoDown(held);
        }

        public bool OnEnter()
        {
            return mainWindowViewModel.DoEnter();
        }

        public bool OnEscape()
        {
            return mainWindowViewModel.DoEscape();
        }

        public bool OnLeft(bool held)
        {
            return mainWindowViewModel.DoLeft(held);
        }

        public bool OnPageDown()
        {
            return mainWindowViewModel.DoPageDown();
        }

        public bool OnPageUp()
        {
            return mainWindowViewModel.DoPageUp();
        }

        public bool OnRight(bool held)
        {
            return mainWindowViewModel.DoRight(held);
        }

        public bool OnUp(bool held)
        {
            return mainWindowViewModel.DoUp(held);
        }

        public void OnSelectionChanged(FilterType filterType, string filterValue, IPlatform platform, IPlatformCategory category, IPlaylist playlist, IGame game)
        {
            return;
        }
    }
}
