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

        public MainWindowView()
        {
            InitializeComponent();

            mainWindowViewModel = new MainWindowViewModel();
            DataContext = mainWindowViewModel;

            Loaded += MainWindowView_Loaded;
        }

        private async void MainWindowView_Loaded(object sender, RoutedEventArgs e)
        {
            await mainWindowViewModel.Initialize();

            // set the activation mode in the view model 
            InitializeMainWindowViewModelActivationMode();

            // set the visibility mode in the view model 
            InitializeMainWindowViewModelVisibilityMode();
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
            if(mainWindowViewModel != null)
            {
                mainWindowViewModel.VisibilityMode = VisibilityMode;
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
