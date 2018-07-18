using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Monify.Converters.MainViewConverters
{

    class PropertyMarginConverterForHidingControl : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double obj = (double)value;
            string par = parameter as string;
            if (par != null)
            {
                if (par.ToLower() == "left")
                {
                    return new Thickness(-obj, 0, 0, 0);
                }
                else if (par.ToLower() == "top")
                {
                    return new Thickness(0, -obj, 0, 0);
                }
                else if (par.ToLower() == "right")
                {
                    return new Thickness(0, 0, -obj, 0);
                }
                else if (par.ToLower() == "bottom")
                {
                    return new Thickness(0, 0, 0, -obj);
                }
                else
                {
                    return new Exception("Invalid parameter passed in PropertyMarginConverter");
                }
            }
            else
            {
                return new Exception("Invalid parameter passed in PropertyMarginConverter");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
