using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Tema3._1.Models;

namespace Tema3._1.Converters
{
    class ReceiptConvert : IMultiValueConverter
    {
        private Supermarket2Entities context = new Supermarket2Entities();
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (values[0] == null && values[1] == null)
            {
               string employeeName = values[0].ToString();
                int employeeId = context.Employees.Where(employee => employee.Name == employeeName).
                    Select(employee => employee.EmployeeID).
                    FirstOrDefault();

                DateTime date;
                if (DateTime.TryParse(values[1].ToString(), out date))
                {
                    // Conversia a reușit, poți utiliza valoarea id
                }
                bool isActive = values[2] is int && (int)values[2] != 0; // Conversie la bool

                return new Receipt()
                {
                    EmployeeID = employeeId,
                    Date = date,
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
