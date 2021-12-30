using System.Linq;
using System.Windows.Controls;
using BigBoxVoiceSearch.DataAccess;
using BigBoxVoiceSearch.Models;
using BigBoxVoiceSearch.ViewModel;
using BigBoxVoiceSearch.VoiceSearch;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace BigBoxVoiceSearch.View
{
    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : UserControl, IBigBoxThemeElementPlugin
    {
        readonly MainWindowViewModel mainWindowViewModel;
        
        public MainWindowView()
        {
            InitializeComponent();

            mainWindowViewModel = new MainWindowViewModel();
            DataContext = mainWindowViewModel;

            Loaded += MainWindowView_Loaded;
        }

        private async void MainWindowView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await mainWindowViewModel.Initialize();
        }

        public bool OnDown(bool held)
        {
            return false;
        }

        public bool OnEnter()
        {
            return false;
        }

        public bool OnEscape()
        {
            return false;
        }

        public bool OnLeft(bool held)
        {
            return false;
        }

        public bool OnPageDown()
        {
            if (BigBoxVoiceSearchSettingsDataProvider.Instance.BigBoxVoiceSearchSettings.searchOnPageDown)
            {
                mainWindowViewModel.DoVoiceSearch();
                return true;
            }
            return false;
        }

        public bool OnPageUp()
        {
            if (BigBoxVoiceSearchSettingsDataProvider.Instance.BigBoxVoiceSearchSettings.searchOnPageUp)
            {
                mainWindowViewModel.DoVoiceSearch();
                return true;
            }
            return false;
        }

        public bool OnRight(bool held)
        {
            return false;
        }

        public bool OnUp(bool held)
        {
            return false;
        }

        public void OnSelectionChanged(FilterType filterType, string filterValue, IPlatform platform, IPlatformCategory category, IPlaylist playlist, IGame game)
        {
            // intentionally left blank
        }
    }
}
