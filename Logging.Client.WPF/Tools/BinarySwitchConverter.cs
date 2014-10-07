using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Logging.Client.WPF
{
    public class BinarySwitchConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty ValueIfTrueProperty = DependencyProperty.Register("ValueIfTrue", typeof(object), typeof(BinarySwitchConverter), null);

        public object ValueIfTrue
        {
            get { return this.GetValue(ValueIfTrueProperty); }
            set { this.SetValue(ValueIfTrueProperty, value); }
        }

        public static readonly DependencyProperty ValueIfFalseProperty = DependencyProperty.Register("ValueIfFalse", typeof(object), typeof(BinarySwitchConverter), null);

        public object ValueIfFalse
        {
            get { return this.GetValue(ValueIfFalseProperty); }
            set { this.SetValue(ValueIfFalseProperty, value); }
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? ValueIfTrue : ValueIfFalse;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
