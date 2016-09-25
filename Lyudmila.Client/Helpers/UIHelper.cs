// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Lyudmila.Client.Helpers
{
    public static class UIHelper
    {
        public static void Bind(object dataSource, string sourcePath, FrameworkElement destinationObject, DependencyProperty dp)
        {
            Bind(dataSource, sourcePath, destinationObject, dp, null, BindingMode.Default, null);
        }

        public static void Bind(object dataSource, string sourcePath, FrameworkElement destinationObject, DependencyProperty dp, BindingMode bindingMode)
        {
            Bind(dataSource, sourcePath, destinationObject, dp, null, bindingMode, null);
        }

        public static void Bind(object dataSource, string sourcePath, FrameworkElement destinationObject, DependencyProperty dp, string stringFormat)
        {
            Bind(dataSource, sourcePath, destinationObject, dp, stringFormat, BindingMode.Default, null);
        }

        public static void Bind(object dataSource, string sourcePath, FrameworkElement destinationObject, DependencyProperty dp, string stringFormat,
            BindingMode bindingMode)
        {
            Bind(dataSource, sourcePath, destinationObject, dp, stringFormat, bindingMode, null);
        }

        public static void Bind(object dataSource, string sourcePath, FrameworkElement destinationObject, DependencyProperty dp, string stringFormat,
            BindingMode bindingMode, IValueConverter converter)
        {
            var binding = new Binding
            {
                Source = dataSource,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Mode = bindingMode,
                Path = new PropertyPath(sourcePath),
                StringFormat = stringFormat,
                Converter = converter
            };
            destinationObject.SetBinding(dp, binding);
        }

        public static T FindParent<T>(this DependencyObject child) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(child);
            do
            {
                var matchedParent = parent as T;
                if(matchedParent != null)
                    return matchedParent;
                parent = VisualTreeHelper.GetParent(parent);
            }
            while(parent != null);

            return null;
        }
    }

    public class HalfValueConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double) value / 2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double) value * 2;
        }

        #endregion
    }
}