using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Tema3._1.Models;

namespace Tema3._1.Converters
{
    public class ProductProducerConvert : IValueConverter
    {
        private Supermarket2Entities context = new Supermarket2Entities();
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (value == null)
                return null;
            int producerId = (int)value;

            string producerName = context.Producers.
                Where(producer => producer.ProducerID == producerId).
                Select(producer => producer.Name).
                FirstOrDefault();

            return producerName;


        }
        public object ConvertBack(object value, Type targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;
            string producerName = (string)value;

            int producerId = context.Producers
                .Where(producer => producer.Name == producerName)
                .Select(producer => producer.ProducerID)
                .FirstOrDefault();

            return producerId;

        }
    }
}
