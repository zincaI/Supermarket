using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tema3._1.Helpers;
using Tema3._1.View.Cashier;

namespace Tema3._1.ViewModel
{
    public class CashierVM
    {
        private ICommand openWindowCommand;
        public ICommand OpenWindowCommand
        {
            get
            {
                if (openWindowCommand == null)
                {
                    openWindowCommand = new RelayCommand(OpenWindow);
                }
                return openWindowCommand;
            }
        }

        public void OpenWindow(object obj)
        {
            string nr = obj as string;
            switch (nr)
            {
                case "1":
                    ProductListView productsListView = new ProductListView();
                    productsListView.ShowDialog();
                    break;
                case "2":
                    ReceiptView receiptView = new ReceiptView();
                    receiptView.ShowDialog();
                    break;

                case "3":
                    AllReceiptsView allreceiptsView = new AllReceiptsView();
                    allreceiptsView.ShowDialog();
                    break;
                default:
                    break;
            }
        }


    }
}
