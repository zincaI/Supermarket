﻿using SUpermarket5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SUpermarket5.Converters
{
    internal class StockProductConvert : IValueConverter
    {
        private Supermarket4Entities context = new Supermarket4Entities();
        public object Convert(object values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (values != null)
            {
                int productID = (int)values;
                string productName = context.Products.Where(product => product.ProductID == productID).Select(product => product.Name).FirstOrDefault();

                return productName;
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
                string productName = (string)value;
                int productID = context.Products.Where(product => product.Name == productName).Select(product => product.ProductID).FirstOrDefault();

                return productID;
            }
            else
            {
                return null;
            }
        }
    }
}
