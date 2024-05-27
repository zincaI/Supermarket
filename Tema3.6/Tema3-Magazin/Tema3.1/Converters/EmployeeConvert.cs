using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Tema3._1.Models;

namespace Tema3._1.Converters
{
    public class EmployeeConvert : IMultiValueConverter
    {
        //public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        //{

        //    if (values[0] != null && values[1] != null && values[2]!=null && values[3]!=null)
        //    {
        //        string name = values[0].ToString();
        //        string password = values[1].ToString();
        //        string type = values[2].ToString();
        //        bool isActive = values[3] is int && (int)values[3] != 0; // Conversie la bool

        //        return new Employee()
        //        {
        //            Name = name,
        //            Password = password,
        //            Type = type,
        //            IsActive = isActive
        //        };
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is string name)
            {
                if (values.Length == 2 && values[1] is string type)
                    return new Employee
                    {
                        Name = name,
                        Password = null,
                        Type = type,
                        IsActive = true
                    };
                else if (values.Length == 1)
                {
                    return new Employee
                    {
                        Name = name,
                        Password = null,
                        Type = null,
                        IsActive = true
                    };
                }
            }

            return null;
        }


        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
