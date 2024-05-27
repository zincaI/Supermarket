using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using SUpermarket5.Models;

namespace SUpermarket5.Converters
{
    public class ProductCategoryConvert : IValueConverter
    {
        private Supermarket4Entities context = new Supermarket4Entities();
        public object Convert(object values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (values != null)
            {
                int categoryId = (int)values;
                string categoryName = context.Categories.Where(category => category.CategoryID == categoryId).Select(category => category.Name).FirstOrDefault();

                return categoryName;
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
                string categoryName = (string)value;
                int categoryId = context.Categories.Where(category => category.Name == categoryName).Select(category => category.CategoryID).FirstOrDefault();

                return categoryId;
            }
            else
            {
                return null;
            }
        }
    }
}
