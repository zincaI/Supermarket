using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Tema3._1.Models.BusinessLogicLayer;

namespace Tema3._1.ViewModel
{
    public class ReceiptVM
    {
        private Receipt_ProductBLL receipt_ProductBLL;
        private ReceiptBLL receiptBLL;
        private StockBLL stockBLL;
        private string ErrorMessage;

        public ReceiptVM()
        {
            receipt_ProductBLL = new Receipt_ProductBLL();
            receiptBLL = new ReceiptBLL();
            stockBLL = new StockBLL();
        }


    }
}
