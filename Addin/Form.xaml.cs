using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SafeguardAddin
{
    /// <summary>
    /// Interaction logic for SafeguardForm.xaml
    /// </summary>
    public partial class SafeguardForm : UserControl
    {
        public SafeguardForm()
        {
            InitializeComponent();
        }
    }

    public class ToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                return ((bool) value) ? Visibility.Visible : Visibility.Collapsed;
            }

            if (value is string)
            {
                return string.IsNullOrEmpty((string) value) ? Visibility.Collapsed : Visibility.Visible;
            }

            if (value is int)
            {
                return (int) value == 0 ? Visibility.Collapsed : Visibility.Visible;
            }

            return value == null ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ToVisibilityConverterInverse : IValueConverter
    {
        private static ToVisibilityConverter _baseConverter = new ToVisibilityConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = _baseConverter.Convert(value, targetType, parameter, culture) as Visibility?;
            if (result == null) return Visibility.Collapsed;
            return result.Value == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
