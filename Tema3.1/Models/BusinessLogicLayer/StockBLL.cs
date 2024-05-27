using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Objects;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Tema3._1.Models.BusinessLogicLayer
{
    public class StockBLL
    {
        public Supermarket2Entities context = new Supermarket2Entities();
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
                double? sellprice=0;
                sellprice = stock.BuyPrice+(stock.BuyPrice*adaos)/100;
                context.AddStock(stock.ProductID, stock.Quantity, stock.Date,stock.BuyPrice,sellprice,stock.ExpirationDate, new ObjectParameter("stockID", stock.StockID));
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
                if (s.StockID == stock.StockID)
                {
                    //s.Quantity = stock.Quantity;
                    //s.IsActive = stock.IsActive;
                    //s.SellPrice = stock.SellPrice;
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
        public ObservableCollection<Stock> GetAllStocks()
        {
            return new ObservableCollection<Stock>(context.Stocks.ToList());
        }

        public void FilterMethod(object obj)
        {
            var properties = obj.GetType().GetProperties();
            string filterText;
            string filterType;
            filterText = (string)properties[0].GetValue(obj);
            filterType = (string)properties[1].GetValue(obj);

            
            IQueryable<Stock> query = null;

            switch (filterType)
            {
                case "Name":
                    query = from s in context.Stocks
                            where s.IsActive == true
                            join p in context.Products on s.ProductID equals p.ProductID
                           // where s.Date < DateTime.Now
                            where p.Name.StartsWith(filterText)
                            select s;
                    break;
                case "Producer":
                    query = from s in context.Stocks
                            where s.IsActive == true
                            join p in context.Products on s.ProductID equals p.ProductID
                           // where s.Date < DateTime.Now
                            join pr in context.Producers on p.ProducerID equals pr.ProducerID
                            where pr.Name.StartsWith(filterText)
                            select s;
                    break;
                case "Expiration date":
                    if (string.IsNullOrEmpty(filterText))
                        return;
                    DateTime filterDate = DateTime.Parse(filterText).Date;
                    query = from s in context.Stocks
                            where s.IsActive == true
                            where s.ExpirationDate == filterDate.Date
                            select s;
                    break;
                case "Barcode":
                    int ex = int.Parse(filterText);
                    query = from s in context.Stocks
                            where s.IsActive == true
                            join p in context.Products on s.ProductID equals p.ProductID
                    // where s.Date < DateTime.Now
                    
                            where p.BarCode.Equals(ex)
                            select s;
                    break;
                case "Category":
                    query = from s in context.Stocks
                            where s.IsActive == true
                            join p in context.Products on s.ProductID equals p.ProductID
                           // where s.Date < DateTime.Now
                            join c in context.Categories on p.CategoryID equals c.CategoryID
                            where c.Name.StartsWith(filterText)
                            select s;
                    break;
                default:
                    break;
            };
            if (query != null)
            {
                var filteredStocks = query.ToList();
                stocks.Clear();
                foreach (var product in filteredStocks)
                {
                    stocks.Add(product);
                }
            }
        }

    }
}
