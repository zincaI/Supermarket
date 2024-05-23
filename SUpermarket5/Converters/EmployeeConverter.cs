using SUpermarket5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SUpermarket5.Converters
{
    class EmployeeConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (values[0] != null && values[1] != null)
            {
                string name = values[0].ToString();
                string password = values[1].ToString();
                string type = values[2].ToString();
                bool isActive = values[3] is int && (int)values[3] != 0; // Conversie la bool

                return new Employee()
                {
                    Name = name,
                    Password = password,
                    Type = type,
                    IsActive = isActive
                };
            }
            else
            {
                return null;
            }
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
