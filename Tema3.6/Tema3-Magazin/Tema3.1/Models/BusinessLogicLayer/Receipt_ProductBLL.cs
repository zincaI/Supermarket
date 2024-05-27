using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema3._1.Models.BusinessLogicLayer
{
    public class Receipt_ProductBLL
    {

        public Supermarket2Entities context = new Supermarket2Entities();
        public ObservableCollection<Receipt_Product> listProducts { get; set; }
        public string ErrorMessage { get; set; }

        public ObservableCollection<DaySalesSummary> sales = new ObservableCollection<DaySalesSummary>();


        public Receipt_ProductBLL()
        {
            listProducts = new ObservableCollection<Receipt_Product>();
        }
        public ObservableCollection<Receipt_Product> GetAllReceipt_Products()
        {
            List<Receipt_Product> rec_products = context.Receipt_Product.ToList();
            ObservableCollection<Receipt_Product> rec_prod = new ObservableCollection<Receipt_Product>();
            foreach (Receipt_Product rec_product in rec_products)
            {
                rec_prod.Add(rec_product);
            }
            return rec_prod;
        }
        public class DaySalesSummary
        {
            public DateTime Date { get; set; }
            public double TotalSales { get; set; }
        }

        //public ObservableCollection<KeyValuePair<DateTime?, DaySalesSummary>> GetDailyStats()
        //{
        //    return new ObservableCollection<KeyValuePair<DateTime?, DaySalesSummary>>(
        //        salesSummaries.Select(kv => new KeyValuePair<DateTime?, DaySalesSummary>(kv.Key, kv.Value))
        //    );
        //}
        public void AddMethod(object obj)
        {
            //parametrul obj este cel dat prin CommandParameter cu MultipleBinding la Button in xaml
            Receipt_Product rp = obj as Receipt_Product;
            if (rp != null)
            {
                if (rp.ProductID == 0)
                {
                    ErrorMessage = "Selecteaza un produs din lista";
                }
                else if (rp.ReceiptID == 0)
                {
                    ErrorMessage = "Selecteaza un bon din lista";
                }
                else if (rp.Quantity == 0)
                {
                    ErrorMessage = "Cantitatea trebuie precizata";
                }
                else
                {
                    context.Receipt_Product.Add(rp);
                    context.SaveChanges();
                    rp.ReceiptProductID = context.Receipt_Product.Max(item => item.ReceiptProductID);
                    listProducts.Add(rp);
                    ErrorMessage = "";

                }
            }
        }
        public void SearchReceiptProductMethod(object obj)
        {
            //string producerName = obj as string;
            //var query = from rp in context.Receipt_Product
            //            where rp.ReceiptProductID.StartsWith(producerName)
            //            select rp;
            //if (query != null)
            //{

            //    listProducts.Clear();
            //    var receiptproducts = query.ToList();
            //    foreach (var group in receiptproducts)
            //    {
            //        listProducts.Add(group.First());
            //    }
            //}
            if (obj is string producerNameStr && int.TryParse(producerNameStr, out int producerName))
            {
                // Adăugăm datele în memorie pentru a folosi `ToString()` și `StartsWith()`.
                var query = from rp in context.Receipt_Product.AsEnumerable()
                            join r in context.Receipts on rp.ReceiptID equals r.ReceiptID
                            where r.ReceiptID.ToString().StartsWith(producerNameStr)
                            select rp;

                listProducts.Clear();
                var receiptproducts = query.ToList();
                foreach (var product in receiptproducts)
                {

                    listProducts.Add(product); // Assuming listProducts is a list of receipt products.
                }
            }
        }
        public (Receipt, double) BestReceiptMethod(object obj)
        {
            //string cashierName = string.Empty;
            //string description = string.Empty;
            //if (obj is DateTime selectedDate)
            //{
            //    int receiptId = (from r in context.Receipt
            //                     where r.date_of_purchase == selectedDate
            //                     join p in context.Product on r.id equals p.receipt_id
            //                     group p by r.id into grouped
            //                     let sum = grouped.Sum(p => p.selling_price)
            //                     orderby sum descending
            //                     select grouped.Key).FirstOrDefault();
            //    cashierName = context.Receipt
            //                  .Where(receipt => receipt.id == receiptId)
            //                  .FirstOrDefault()
            //                  .User
            //                  .username
            //                  .ToString();

            //    description = GetReceiptForDisplay(context.Receipt
            //           .Where(receipt => receipt.id == receiptId)
            //           .FirstOrDefault());

            //}

            Receipt bestReceipt = new Receipt();
            double bestPrice = 0;
            if (obj is DateTime selectedDate)
            {
                Receipt_ProductBLL receiptProductBLL = new Receipt_ProductBLL();
                var receiptsEnumerator = context.Receipts.Where(receipt => receipt.Date == selectedDate).GetEnumerator();
                receiptProductBLL.listProducts = receiptProductBLL.GetAllReceipt_Products();
                while (receiptsEnumerator.MoveNext())
                {
                    double finalPrice = 0;


                    foreach (Receipt_Product receiptproduct in receiptProductBLL.listProducts.Where(prod => prod.ReceiptID == receiptsEnumerator.Current.ReceiptID))
                    {
                        finalPrice += ((float)receiptproduct.TotalPrice);
                    }
                    if (finalPrice > bestPrice)
                    {
                        bestPrice = finalPrice;
                        bestReceipt = receiptsEnumerator.Current;
                    }
                }
            }


            return (bestReceipt, bestPrice);
        }

        public ObservableCollection<DaySalesSummary> MonthlyStatsMethod(object obj)
        {
            sales.Clear(); // Asigură-te că lista este golită înainte de a adăuga date noi

            var properties = obj.GetType().GetProperties();
            string name = (string)properties[0].GetValue(obj);
            string year = (string)properties[1].GetValue(obj);
            string month = (string)properties[2].GetValue(obj);
            Employee employee = context.Employees.Where(emp => emp.Name == name).FirstOrDefault();

            if (employee != null)
            {
                DateTime startDate = new DateTime(int.Parse(year), int.Parse(month), 1);
                DateTime endDate = startDate.AddMonths(1).AddDays(-1);

                var receipts = context.Receipts.Where(recept => recept.Date >= startDate && recept.Date <= endDate && recept.EmployeeID == employee.EmployeeID).ToList();

                Receipt_ProductBLL receiptProductBLL = new Receipt_ProductBLL();
                receiptProductBLL.listProducts = receiptProductBLL.GetAllReceipt_Products();
                for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    DaySalesSummary aux = new DaySalesSummary();
                    aux.Date = date;
                    aux.TotalSales = 0;
                    foreach (var receipt in receipts.Where(rec => rec.Date == date).ToList())
                    {
                        foreach (Receipt_Product receiptproduct in receiptProductBLL.listProducts.Where(prod => prod.ReceiptID == receipt.ReceiptID).ToList())
                        {
                            aux.TotalSales += ((float)receiptproduct.TotalPrice);
                        }
                    }
                    if (aux.TotalSales > 0)
                        sales.Add(aux);
                }
            }
            return sales; // Returnează lista de vânzări
        }

    }
        public class DaySalesSummary
        {
            public DateTime Date { get; set; }
            public double TotalSales { get; set; }
        }

}
