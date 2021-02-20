using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Valkirie.Client.Utilities
{
    [ValueConversion(typeof(int), typeof(string))]
    public class IntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(int))
                throw new InvalidOperationException("The target must be a int");

            var result = value.ToString();

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(float), typeof(string))]
    public class FloatToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(float))
                throw new InvalidOperationException("The target must be a float");

            var result = value.ToString();

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(bool), typeof(bool))]
    public class InverseBooleanConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a boolean");

            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
