using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestRealmCognitiveXamarin.Converters
{
    public class Base64ToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            byte[] data = System.Convert.FromBase64String(value.ToString());
            MemoryStream ms = new MemoryStream(data, 0, data.Length);

            return ImageSource.FromStream(() => { return ms; });

            //BitmapImage biImg = new BitmapImage();
            //biImg.SetSourceAsync(ms.AsRandomAccessStream());
            //return biImg;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
