using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SUpermarket5.Converters
{
    class ReceiptConvert : IMultiValueConverter
    {
        private Supermarket4Entities context = new Supermarket4Entities();
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (values[0] != null && values[1] != null)
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
