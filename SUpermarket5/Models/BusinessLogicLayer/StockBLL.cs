﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUpermarket5.Models.BusinessLogicLayer
{
    public class StockBLL
    {
        private Supermarket4Entities context = new Supermarket4Entities();
        public ObservableCollection<Stock> stocks { get; set; }
        public string ErrorMessage { get; set; }
        private double? adaos;
        public StockBLL()
        {
            stocks = new ObservableCollection<Stock>();
        }

        public void AddMethod(object obj)
        {
            //parametrul obj este cel dat prin CommandParameter cu MultipleBinding la Button in xaml
            Stock stock = obj as Stock;
            if (stock.ProductID == 0)
                ErrorMessage = "Selecteaza un producator din lista";
            else if (stock.Quantity == 0)
                ErrorMessage = "Cantitatea trebuie precizata";
            else if (stock.Date == null)
                ErrorMessage = "Data trebuie precizata";
            else if (stock.BuyPrice == 0)
                ErrorMessage = "Pretul trebuie precizat";
            else if (stock.Date == null)
                ErrorMessage = "Data aprovizionarii trebuie precizata";
            else if (stock.ExpirationDate == null)
                ErrorMessage = "Data expirarii trebuie precizata";
            else
            {
                string linie;

                using (StreamReader sr = new StreamReader("Configuration.txt"))
                {

                    linie = sr.ReadLine();
                    if (double.TryParse(linie, out double result))
                    {
                        adaos = result;
                    }
                }
                double? sellprice = 0;
                sellprice = stock.BuyPrice + (stock.BuyPrice * adaos) / 100;
                context.AddStock(stock.ProductID, stock.Quantity, stock.Date, stock.BuyPrice, sellprice, stock.ExpirationDate, new ObjectParameter("stockID", stock.StockID));
                context.SaveChanges();
                stock.StockID = context.Stocks.Max(item => item.StockID);
                stocks.Add(stock);
                ErrorMessage = "";
            }
        }


        public void UpdateMethod(object obj)
        {
            Stock stock = obj as Stock;
            //if (stock == null)
            //{
            //    ErrorMessage = "Selecteaza stock";
            //}
            //if (prd.ProducerID == 0)
            //    ErrorMessage = "Selecteaza un producator din lista";
            //else if (prd.CategoryID == 0)
            //    ErrorMessage = "Selecteaza o categorie din lista";
            //else if (prd.BarCode == 0)
            //    ErrorMessage = "Codul de bare trebuie precizat";

            bool ok = false;
            foreach (Stock s in stocks)
                if (s.StockID == stock.StockID && s.IsActive == true)
                {

                    context.ModifyStock(s.StockID, s.ProductID, stock.Quantity, s.Date, stock.SellPrice, s.ExpirationDate);
                    context.SaveChanges();
                    ErrorMessage = "";
                }

        }

        public void DeleteMethod(object obj)
        {
            Stock stock = obj as Stock;
            if (stock == null)
            {
                ErrorMessage = "Selecteaza un stock";
            }
            else
            {
                Stock s = context.Stocks.Where(i => i.StockID == stock.StockID).FirstOrDefault();
                if (s != null)
                {
                    context.DeleteStock(s.StockID);
                    context.SaveChanges();
                    stocks.Remove(s);
                    ErrorMessage = "";
                }
                else
                {
                    ErrorMessage = "Stockul nu a fost găsit";
                }
            }
        }

    }
}
