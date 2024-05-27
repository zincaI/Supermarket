using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Tema3._1.Models;

namespace Tema3._1.Converters
{
    class StockConvert : IMultiValueConverter
    {
        private Supermarket2Entities context = new Supermarket2Entities();
        private double? adaos;

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (values[0] != null && values[1] != null)
            {
                string productName = values[0].ToString();
                int productId = context.Products.Where(product => product.Name == productName).
                    Select(product => product.ProductID).
                    FirstOrDefault();


                int quantity;
                if (int.TryParse(values[1].ToString(), out quantity))
                {
                    // Conversia a reușit, poți utiliza valoarea id
                }

                DateTime date;
                if (DateTime.TryParse(values[2].ToString(), out date))
                {
                    // Conversia a reușit, poți utiliza valoarea id
                }
                
                float buyPrice;
                if (float.TryParse(values[3].ToString(), out buyPrice))
                {
                    // Conversia a reușit, poți utiliza valoarea id
                }
                DateTime expiration;
                if (DateTime.TryParse(values[4].ToString(), out expiration))
                {
                    // Conversia a reușit, poți utiliza valoarea id
                }

                string linie;

                using (StreamReader sr = new StreamReader("Configuration.txt"))
                {

                    linie = sr.ReadLine();
                    if (double.TryParse(linie, out double result))
                    {
                        adaos = result;
                    }
                }
                double? sellprice = 0;
                sellprice = buyPrice + (buyPrice * adaos) / 100;
                return new Stock()
                {
                    ProductID = productId,
                    Quantity = quantity,
                    Date = date,
                    SellPrice = sellprice,
                    BuyPrice = buyPrice,
                    ExpirationDate = expiration,
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
