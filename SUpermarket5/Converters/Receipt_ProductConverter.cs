using SUpermarket5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SUpermarket5.Converters
{
    class Receipt_ProductConvert : IMultiValueConverter
    {
        private Supermarket4Entities context = new Supermarket4Entities();

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (values[0] != null && values[1] != null && values[2] != null)
            {

                string productName = values[0].ToString();
                int productid = context.Employees.Where(employee => employee.Name == productName).Select(employee => employee.EmployeeID).FirstOrDefault();



                int receiptId;
                if (int.TryParse(values[1].ToString(), out receiptId))
                {
                    // Conversia a reușit, poți utiliza valoarea id
                }

                int quantity;
                if (int.TryParse(values[2].ToString(), out quantity))
                {
                    // Conversia a reușit, poți utiliza valoarea id
                }

                //bool isActive = values[2] is int && (int)values[2] != 0; // Conversie la bool

                return new Receipt_Product()
                {
                    ProductID = productid,
                    ReceiptID = receiptId,
                    Quantity = quantity
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
