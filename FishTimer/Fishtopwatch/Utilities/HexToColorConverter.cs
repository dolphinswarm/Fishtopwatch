using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Graphics;

namespace Fishtopwatch.Utilities
{
    public class HexToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hexString = value as string;
            if (!string.IsNullOrWhiteSpace(hexString))
                return Color.FromArgb(hexString);

            return Color.FromArgb("#FFFFFF");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
