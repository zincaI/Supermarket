using SUpermarket5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SUpermarket5.Converters
{
    public class ProductProducerConvert : IValueConverter
    {
        private Supermarket4Entities context = new Supermarket4Entities();
        public object Convert(object values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (values != null)
            {
                int producerId = (int)values;
                string producerName = context.Producers.Where(producer => producer.ProducerID == producerId).Select(producer => producer.Name).FirstOrDefault();

                return producerName;
            }
            else
            {
                return null;
            }
        }
        public object ConvertBack(object value, Type targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                string producerName = (string)value;
                int producerId = context.Producers.Where(producer => producer.Name == producerName).Select(producer => producer.ProducerID).FirstOrDefault();

                return producerId;
            }
            else
            {
                return null;
            }
        }
    }
}
