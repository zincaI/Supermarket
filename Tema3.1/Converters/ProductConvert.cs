using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Tema3._1.Models;

namespace Tema3._1.Converters
{
    class ProductConvert : IMultiValueConverter
    {
        private Supermarket2Entities context = new Supermarket2Entities();

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (values[0] != null && values[1] != null)
            {
                string producerName = values[0].ToString();
                int producerid = context.Producers.Where(producer => producer.Name == producerName).Select(producer => producer.ProducerID).FirstOrDefault();
                
                
                
                string categoryName = values[1].ToString();
                int categoryid = context.Categories.Where(category => category.Name == categoryName).Select(category => category.CategoryID).FirstOrDefault();

                string productName = values[2].ToString();

                int barCode;
                if (int.TryParse(values[3].ToString(), out barCode))
                {
                    // Conversia a reușit, poți utiliza valoarea id
                }

                //bool isActive = values[4] is int && (int)values[4] != 0; // Conversie la bool

                return new Product()
                {
                   ProducerID = producerid,
                   CategoryID = categoryid,
                   Name= productName,
                   BarCode = barCode,
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
