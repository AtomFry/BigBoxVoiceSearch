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

        public static readonly DependencyProperty ShowInitializingProperty =
            DependencyProperty.Register("ShowInitializing", typeof(bool), typeof(MainWindowView), new PropertyMetadata(false));

        public static readonly DependencyProperty ShowInitializingFailedProperty =
            DependencyProperty.Register("ShowInitializingFailed", typeof(bool), typeof(MainWindowView), new PropertyMetadata(false));

        public static readonly DependencyProperty ShowInactiveProperty =
            DependencyProperty.Register("ShowInactive", typeof(bool), typeof(MainWindowView), new PropertyMetadata(false));

        public static readonly DependencyProperty ShowActiveProperty =
            DependencyProperty.Register("ShowActive", typeof(bool), typeof(MainWindowView), new PropertyMetadata(false));

        public static readonly DependencyProperty ShowRecognizingProperty =
            DependencyProperty.Register("ShowRecognizing", typeof(bool), typeof(MainWindowView), new PropertyMetadata(false));

        public static readonly DependencyProperty InitializingImagePathProperty =
            DependencyProperty.Register("InitializingImagePath", typeof(string), typeof(MainWindowView), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty InitializingFailedImagePathProperty =
            DependencyProperty.Register("InitializingFailedImagePath", typeof(string), typeof(MainWindowView), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty InactiveImagePathProperty =
            DependencyProperty.Register("InactiveImagePath", typeof(string), typeof(MainWindowView), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty ActiveImagePathProperty =
            DependencyProperty.Register("ActiveImagePath", typeof(string), typeof(MainWindowView), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty RecognizingImagePathProperty =
            DependencyProperty.Register("RecognizingImagePath", typeof(string), typeof(MainWindowView), new PropertyMetadata(string.Empty));


        public MainWindowView()
        {
            InitializeComponent();

            mainWindowViewModel = new MainWindowViewModel();
            DataContext = mainWindowViewModel;

            Loaded += MainWindowView_Loaded;
        }

        private void MainWindowView_Loaded(object sender, RoutedEventArgs e)
        {
            // set the activation mode in the view model 
            InitializeMainWindowViewModelActivationMode();

            // set custom image paths in the view model
            InitializeMainWindowViewModelCustomInitializingImagePath();
            InitializeMainWindowViewModelCustomInitializingFailedImagePath();
            InitializeMainWindowViewModelCustomInactiveImagePath();
            InitializeMainWindowViewModelCustomActiveImagePath();
            InitializeMainWindowViewModelCustomRecognizingImagePath();            

            // set flags that drive what states the user control is visible in 
            InitializeShowActive();
            InitializeShowInactive();
            InitializeShowInitializing();
            InitializeShowInitializingFailed();
            InitializeShowRecognizing();

            // ask the view model to set itself up
            mainWindowViewModel.Initialize();
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

        public string InitializingFailedImagePath
        {
            get { return (string)GetValue(InitializingFailedImagePathProperty); }
            set
            {
                SetValue(InitializingFailedImagePathProperty, value);
                InitializeMainWindowViewModelCustomInitializingFailedImagePath();
            }
        }

        private void InitializeMainWindowViewModelCustomInitializingFailedImagePath()
        {
            if(mainWindowViewModel != null)
            {
                mainWindowViewModel.CustomInitializingFailedImagePath = InitializingFailedImagePath;
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

        public bool ShowInitializing
        {
            get { return (bool)GetValue(ShowInitializingProperty); }
            set 
            { 
                SetValue(ShowInitializingProperty, value);
                InitializeShowInitializing();
            }
        }

        private void InitializeShowInitializing()
        {
            if (mainWindowViewModel != null)
            {
                mainWindowViewModel.ShowInitializing = ShowInitializing;
            }
        }

        public bool ShowInitializingFailed
        {
            get { return (bool)GetValue(ShowInitializingFailedProperty); }
            set
            {
                SetValue(ShowInitializingFailedProperty, value);
                InitializeShowInitializingFailed();
            }
        }

        private void InitializeShowInitializingFailed()
        {
            if (mainWindowViewModel != null)
            {
                mainWindowViewModel.ShowInitializingFailed = ShowInitializingFailed;
            }
        }

        public bool ShowInactive
        {
            get { return (bool)GetValue(ShowInactiveProperty); }
            set 
            { 
                SetValue(ShowInactiveProperty, value);
                InitializeShowInactive();
            }
        }

        private void InitializeShowInactive()
        {
            if (mainWindowViewModel != null)
            {
                mainWindowViewModel.ShowInactive = ShowInactive;
            }
        }

        public bool ShowActive
        {
            get { return (bool)GetValue(ShowActiveProperty); }
            set 
            { 
                SetValue(ShowActiveProperty, value);
                InitializeShowActive();
            }
        }

        private void InitializeShowActive()
        {
            if (mainWindowViewModel != null)
            {
                mainWindowViewModel.ShowActive = ShowActive;
            }
        }

        public bool ShowRecognizing
        {
            get { return (bool)GetValue(ShowRecognizingProperty); }
            set 
            { 
                SetValue(ShowRecognizingProperty, value);
                InitializeShowRecognizing();
            }
        }

        private void InitializeShowRecognizing()
        {
            if (mainWindowViewModel != null)
            {
                mainWindowViewModel.ShowRecognizing = ShowRecognizing;
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
