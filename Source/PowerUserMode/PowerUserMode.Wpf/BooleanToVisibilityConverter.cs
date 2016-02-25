using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PowerUserMode.Wpf
{
    public class BooleanToVisibilityConverter : DependencyObject, IValueConverter
    {        
        public bool InvertBoundValue
        {
            get { return (bool)GetValue(InvertBoundValueProperty); }
            set { SetValue(InvertBoundValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InvertBoundValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InvertBoundValueProperty =
            DependencyProperty.Register("InvertBoundValue", typeof(bool), typeof(BooleanToVisibilityConverter), new PropertyMetadata(false));

        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var incomingValue = (bool)value;

            if(InvertBoundValue)
            {
                incomingValue = !incomingValue;
            }

            return incomingValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var incomingValue = (Visibility)value;

            var normalBoolValue = incomingValue == Visibility.Visible;

            return InvertBoundValue ? !normalBoolValue : normalBoolValue;
        }
    }
}
