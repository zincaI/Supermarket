using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using Tema3._1.Models;

namespace Tema3._1.Converters
{
    public class ReceiptEmployeeConvert:IValueConverter
    {
        private Supermarket2Entities context = new Supermarket2Entities();
        public object Convert(object values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (values != null)
            {
                int employeeId = (int)values;
                string employeeName = context.Employees.Where(employee => employee.EmployeeID == employeeId).Select(employee => employee.Name).FirstOrDefault();

                return employeeName;
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
                string employeeName = (string)value;
                int employeeId = context.Employees.Where(employee => employee.Name == employeeName).Select(employee => employee.EmployeeID).FirstOrDefault();

                return employeeId;
            }
            else
            {
                return null;
            }
        }
    }
}
