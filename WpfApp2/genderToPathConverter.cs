using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfApp2
{
    class genderToPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is GENDER)) return null;
            GENDER gender = (GENDER)value;
            switch(gender)
            {
                case GENDER.Male:
                    return "/Resources/man.png";
                case GENDER.Female:
                    return "/Resources/woman.jpg";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
