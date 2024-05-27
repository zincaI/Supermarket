using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Tema3._1.Models;

namespace Tema3._1.Converters
{
    class ProducerConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (values[0] != null && values[1] != null)
            {
                string name = values[0].ToString();
                string country = values[1].ToString();
                //bool isActive = values[2] is int && (int)values[2] != 0; // Conversie la bool

                return new Producer()
                {
                    Name = name,
                    Country = country,
                    IsActive = true
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
