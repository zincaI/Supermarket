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
    class Receipt_ProductConvert : IMultiValueConverter
    {
        private Supermarket2Entities context = new Supermarket2Entities();

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (values[0] != null && values[1] != null )
            {

                string productName = values[0].ToString();
                int productid = context.Products.Where(product => product.Name == productName).Select(product => product.ProductID).FirstOrDefault();

                int quantity;
                if (int.TryParse(values[1].ToString(), out quantity))
                {
                    // Conversia a reușit, poți utiliza valoarea id
                }
                double totalprice=0;
                double? price=context.Stocks.Where(stock=>stock.ProductID==productid).Select(stock=>stock.SellPrice).FirstOrDefault();

                totalprice = (price ?? 0) * quantity;
                return new Receipt_Product()
                {
                    ProductID = productid,
                    //ReceiptID = receiptId,
                    Quantity = quantity,
                    TotalPrice = totalprice
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
