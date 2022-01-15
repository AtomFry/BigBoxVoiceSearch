using BigBoxVoiceSearch.Models;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BigBoxVoiceSearch.Converters
{
    public class BigBoxVoiceSearchStateToVisibilityConverter : IValueConverter
    {
        public BigBoxVoiceSearchState StateComparison { get; set; }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BigBoxVoiceSearchState bigBoxVoiceSearchStateFromBinding = (BigBoxVoiceSearchState)value;
            if (bigBoxVoiceSearchStateFromBinding == StateComparison)
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
