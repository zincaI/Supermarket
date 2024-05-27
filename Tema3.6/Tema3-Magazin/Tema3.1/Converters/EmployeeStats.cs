using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Tema3._1.Converters
{
    public class EmployeeStats:IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] != null && values[1] != null)
            {
                string name = values[0].ToString();
                DateTime date = DateTime.Parse(values[1].ToString());
                string year = date.Year.ToString();
                string month = date.Month.ToString();
                var tuple = new { Name = name, Year = year, Month = month };
                return tuple;
            }
            else
            {
                return null;
            }
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
