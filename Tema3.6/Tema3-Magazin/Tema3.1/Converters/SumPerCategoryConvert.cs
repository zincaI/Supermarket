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
    public class SumPerCategoryConvert:IValueConverter
    {
        private Supermarket2Entities context = new Supermarket2Entities();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            int categoryId = (int)value;

            double? categorySum = (from c in context.Categories
                                   join p in context.Products on c.CategoryID equals p.CategoryID
                                   join s in context.Stocks on p.ProductID equals s.ProductID
                                   where c.CategoryID == categoryId && s.Date < DateTime.Now &&s.IsActive==true
                                   select s.SellPrice).Sum();
            if (categorySum == null) 
                return 0;
            return categorySum;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
