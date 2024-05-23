using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SUpermarket5.Converters
{
    class StockConvert : IMultiValueConverter
    {
        private Supermarket4Entities context = new Supermarket4Entities();

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
                float sellPrice;
                if (float.TryParse(values[3].ToString(), out sellPrice))
                {
                    // Conversia a reușit, poți utiliza valoarea id
                }
                float buyPrice;
                if (float.TryParse(values[4].ToString(), out buyPrice))
                {
                    // Conversia a reușit, poți utiliza valoarea id
                }
                DateTime expiration;
                if (DateTime.TryParse(values[5].ToString(), out expiration))
                {
                    // Conversia a reușit, poți utiliza valoarea id
                }


                bool isActive = values[5] is int && (int)values[5] != 0; // Conversie la bool

                return new Stock()
                {
                    ProductID = productId,
                    Date = date,
                    SellPrice = sellPrice,
                    BuyPrice = buyPrice,
                    ExpirationDate = expiration,
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
