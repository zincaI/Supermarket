using SupermarketFinalVersion.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SupermarketFinalVersion.Converters
{
    public class EmployeeConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is string name)
            {
                if (values.Length == 2 && values[1] is string type)
                    return new Employee
                    {
                        Name = name,
                        Password = null,
                        Type = type,
                        IsActive = true
                    };
                else if (values.Length == 1)
                {
                    return new Employee
                    {
                        Name = name,
                        Password = null,
                        Type = null,
                        IsActive = true
                    };
                }
            }

            return null;
        }


        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
