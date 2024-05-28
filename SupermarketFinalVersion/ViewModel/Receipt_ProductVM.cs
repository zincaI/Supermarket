using SupermarketFinalVersion.Helpers;
using SupermarketFinalVersion.Models.BusinessLogicLayer;
using SupermarketFinalVersion.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.IO;

namespace SupermarketFinalVersion.ViewModel
{
    public class Receipt_ProductVM : BaseVM
    {
        private Receipt_ProductBLL receiptproductBLL;
        private ReceiptBLL receiptBLL;
        private string errorMessage;
        int receiptID;
        //public ObservableCollection<(DateTime?, double)> monthlySales = new ObservableCollection<(DateTime?, double)>();


        StockVM StockVM = new StockVM();
        public Receipt_ProductVM()
        {
            receiptproductBLL = new Receipt_ProductBLL();
            receiptBLL = new ReceiptBLL();
            ReceiptList = new ObservableCollection<Receipt>(receiptBLL.GetAllReceipt());
            if (ReceiptList.Count == 0)
            {
                Receipt receipt = new Receipt
                {
                    EmployeeID = GetConnectedEmployeeId(),
                    Date = DateTime.Now,
                    IsActive = true
                };

                receiptBLL.AddMethod(receipt);
                ErrorMessage = receiptBLL.ErrorMessage;
                receiptID = receipt.ReceiptID;
                ReceiptList.Add(receipt);
                ReceiptProductList.Clear();
                ReceiptProductList = new ObservableCollection<Receipt_Product>(receiptproductBLL.GetAllReceipt_Products().Where(product => product.ReceiptID == receiptID));

            }


            receiptID = ReceiptList[ReceiptList.Count - 1].ReceiptID;

        }


        private ObservableCollection<Receipt_ProductBLL.DaySalesSummary> _monthlySales = new ObservableCollection<Receipt_ProductBLL.DaySalesSummary>();
        public ObservableCollection<Receipt_ProductBLL.DaySalesSummary> MonthlySales
        {
            get => _monthlySales;
            set
            {
                if (_monthlySales != value)
                {
                    _monthlySales = value;
                    NotifyPropertyChanged(nameof(MonthlySales));
                }
            }
        }

        public ObservableCollection<Receipt_Product> ReceiptProductList
        {
            get => receiptproductBLL.listProducts;
            set
            {
                if (receiptproductBLL.listProducts != value)
                {
                    receiptproductBLL.listProducts = value;
                    NotifyPropertyChanged(nameof(ReceiptProductList));
                }
            }
        }

        private string cashierName;
        public string CashierName
        {
            get { return cashierName; }
            set
            {
                if (cashierName != value)
                {
                    cashierName = value;
                    NotifyPropertyChanged(nameof(CashierName));
                }
            }
        }

        private Receipt bestReceipt;
        public Receipt BestReceipt
        {
            get { return bestReceipt; }
            set
            {
                if (bestReceipt != value)
                {
                    bestReceipt = value;
                    NotifyPropertyChanged(nameof(BestReceipt));

                }
            }
        }

        private double bestPrice;
        public double BestPrice
        {
            get { return bestPrice; }
            set
            {
                if (bestPrice != value)
                {
                    bestPrice = value;
                    NotifyPropertyChanged(nameof(BestPrice));

                }
            }
        }

        public ObservableCollection<Receipt> ReceiptList
        {
            get => receiptBLL.receipts;
            set => receiptBLL.receipts = value;
        }

        public string ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                NotifyPropertyChanged("ErrorMessage");
            }
        }





        public void AddMethod(object obj)
        {
            //ReceiptProductList = new ObservableCollection<Receipt_Product>(receiptproductBLL.GetAllReceipt_Products().Where(product=>product.ReceiptID==receiptID));

            Receipt_Product receipt_Product = obj as Receipt_Product;
            //var activeStocksEnumerator= StockVM.StocksList.Where(stock => stock.ProductID == receipt_Product.ProductID&&stock.IsActive);
            int totalQuantity = StockVM.StocksList.Where(stock => stock.ProductID == receipt_Product.ProductID && stock.IsActive).Sum(stock => stock.Quantity);
            var activeReceipt = ReceiptList.LastOrDefault();
            if (totalQuantity >= receipt_Product.Quantity && receipt_Product.ProductID != 0 && receipt_Product.Quantity > 0 && activeReceipt.IsActive == true)
            {
                var activeStocksEnumerator = StockVM.StocksList.Where(stock => stock.ProductID == receipt_Product.ProductID && stock.IsActive).GetEnumerator();
                int aux = receipt_Product.Quantity;
                bool ok = false;
                while (activeStocksEnumerator.MoveNext())
                {

                    var stock = activeStocksEnumerator.Current;
                    if (stock.ExpirationDate >= DateTime.Now)
                    {
                        if (stock.Quantity <= aux)
                        {
                            stock.Quantity = 0;
                            aux = aux - stock.Quantity;
                            stock.IsActive = false;
                            //StockBLL stockBLL = new StockBLL();
                            //stockBLL.context.SaveChanges();
                            StockVM.UpdateMethod(stock);
                        }
                        else
                        {
                            stock.Quantity = stock.Quantity - aux;
                            //StockBLL stockBLL = new StockBLL();
                            //stockBLL.context.SaveChanges();
                            StockVM.UpdateMethod(stock);
                            break;
                        }
                    }
                    else
                    {
                        ok = true;
                        stock.IsActive = false;
                        StockVM.UpdateMethod(stock);
                    }

                }
                if (ok == false)
                {
                    int size = ReceiptList.Count;
                    receipt_Product.ReceiptID = receiptID;


                    receiptproductBLL.AddMethod(receipt_Product);
                    ErrorMessage = receiptproductBLL.ErrorMessage;
                }
            }
            else
            {
                MessageBox.Show("Stoc insuficient!");
            }
            //ReceiptProductList = new ObservableCollection<Receipt_Product>(receiptproductBLL.GetAllReceipt_Products().Where(product=>product.ReceiptID==receiptID));
        }

        private ICommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand(AddMethod);
                }
                return addCommand;
            }
        }

        private int GetConnectedEmployeeId()
        {
            string filePath = "EmployeeConected.txt";
            try
            {
                string idText = File.ReadAllText(filePath);
                // string idText = File.ReadAllText(filePath);
                if (int.TryParse(idText, out int employeeId))
                {
                    return employeeId;
                }
                else
                {
                    ErrorMessage = "Invalid employee ID in file.";
                    return -1;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error reading employee ID: {ex.Message}";
                return -1;
            }
        }

        public void CloseReceipt()
        {
            var lastActiveReceipt = ReceiptList.LastOrDefault(r => r.IsActive);
            if (lastActiveReceipt != null)
            {
                lastActiveReceipt.IsActive = false;
                receiptBLL.UpdateMethod(lastActiveReceipt);  // Asigură-te că ai o metodă de update în BLL
            }
        }
        public void AddMethodReceipt(object obj)
        {
            var activeReceipt = ReceiptList.LastOrDefault(r => r.IsActive);

            if (activeReceipt != null)
            {
                int employeeId = GetConnectedEmployeeId();
                if (employeeId != -1)
                {
                    //activeReceipt.EmployeeID = employeeId;
                    //activeReceipt.Date = DateTime.Now;
                    activeReceipt.IsActive = false;
                    receiptBLL.UpdateMethod(activeReceipt);  // Asigură-te că ai o metodă de update în BLL
                    ErrorMessage = receiptBLL.ErrorMessage;
                    receiptID = activeReceipt.ReceiptID;
                    ReceiptProductList = new ObservableCollection<Receipt_Product>(receiptproductBLL.GetAllReceipt_Products().Where(product => product.ReceiptID == receiptID));
                }
            }
            Receipt receipt = new Receipt
            {
                EmployeeID = GetConnectedEmployeeId(),
                Date = DateTime.Now,
                IsActive = true
            };

            receiptBLL.AddMethod(receipt);
            ErrorMessage = receiptBLL.ErrorMessage;
            receiptID = receipt.ReceiptID;
            ReceiptList.Add(receipt);
            ReceiptProductList.Clear();
            ReceiptProductList = new ObservableCollection<Receipt_Product>(receiptproductBLL.GetAllReceipt_Products().Where(product => product.ReceiptID == receiptID));
        
    }

    private ICommand addCommandReceipt;
    public ICommand AddCommandReceipt
    {
        get
        {
            if (addCommandReceipt == null)
            {
                addCommandReceipt = new RelayCommand(AddMethodReceipt);
            }
            return addCommandReceipt;
        }
    }


    public void SearchReceiptMethod(object obj)
    {
        receiptproductBLL.SearchReceiptProductMethod(obj);
        FinalPrice = 0;
        foreach (Receipt_Product receiptproduct in receiptproductBLL.listProducts)
        {
            FinalPrice += ((float)receiptproduct.TotalPrice);
        }
        ErrorMessage = receiptproductBLL.ErrorMessage;
    }

    private ICommand searchReceiptCommand;
    public ICommand SearchReceiptCommand
    {
        get
        {
            if (searchReceiptCommand == null)
            {
                searchReceiptCommand = new RelayCommand(SearchReceiptMethod);
            }
            return searchReceiptCommand;
        }
    }

    private float finalPrice = 0;
    public float FinalPrice
    {
        get
        {
            return finalPrice;
        }
        set
        {
            finalPrice = value;
            NotifyPropertyChanged(nameof(FinalPrice));
        }
    }

    private string description;
    public string Description
    {
        get { return description; }
        set
        {
            if (description != value)
            {
                description = value;
                NotifyPropertyChanged(nameof(Description));
            }
        }
    }


    public void BestReceiptMethod(object obj)
    {

        (BestReceipt, BestPrice) = receiptproductBLL.BestReceiptMethod(obj);
    }

    private ICommand bestReceiptCommand;
    public ICommand BestReceiptCommand
    {
        get
        {
            if (bestReceiptCommand == null)
            {
                bestReceiptCommand = new RelayCommand(BestReceiptMethod);
            }
            return bestReceiptCommand;
        }
    }

    public void MonthlyStats(object obj)
    {
        MonthlySales.Clear(); // Asigură-te că lista este golită înainte de a adăuga date noi
        var salesData = receiptproductBLL.MonthlyStatsMethod(obj);
        foreach (var item in salesData)
        {
            MonthlySales.Add(item);
        }
    }


    private ICommand monthlyStatsCommand;
    public ICommand MonthlyStatsCommand
    {
        get
        {
            if (monthlyStatsCommand == null)
            {
                monthlyStatsCommand = new RelayCommand(MonthlyStats);
            }
            return monthlyStatsCommand;
        }
    }


    //public void FilterMethod(object obj)
    //{
    //    stockBLL.FilterMethod(obj);
    //    ErrorMessage = stockBLL.ErrorMessage;
    //}
    //private ICommand filterCommand;
    //public ICommand FilterCommand
    //{
    //    get
    //    {
    //        if (filterCommand == null)
    //        {
    //            filterCommand = new RelayCommand(FilterMethod);
    //        }
    //        return filterCommand;
    //    }
    //}
}
}
