using BigBoxVoiceSearch.Models;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BigBoxVoiceSearch.Converters
{
    public class BigBoxVoiceSearchStateToVisibilityConverter : IMultiValueConverter
    {
        public BigBoxVoiceSearchState StateComparison { get; set; }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            BigBoxVoiceSearchState bigBoxVoiceSearchStateFromBinding = (BigBoxVoiceSearchState)values[0];
            bool showStateValue = (bool)values[1];

            if (bigBoxVoiceSearchStateFromBinding == StateComparison && showStateValue)
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
