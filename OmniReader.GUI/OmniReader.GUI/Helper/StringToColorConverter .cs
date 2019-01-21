using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OmniReader.GUI.Helper
{
    public class StringToColorConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) value = "";

            string valueAsString = value.ToString();
            switch (valueAsString)
            {

                case (""):
                {
                    return Color.Default;
                }
                case ("Default"):
                {
                    return Color.Default;
                }
                case ("Red"):
                {
                    return Color.Red;
                }
                case ("Green"):
                {
                    return Color.Green;
                }
                case ("Blue"):
                {
                    return Color.Blue;
                }
                case ("Yellow"):
                {
                    return Color.Yellow;
                }
                default:
                {
                    return Color.FromHex(value.ToString());
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
