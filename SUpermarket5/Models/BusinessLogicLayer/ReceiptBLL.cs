using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUpermarket5.Models.BusinessLogicLayer
{
    public class ReceiptBLL
    {
        private Supermarket4Entities context = new Supermarket4Entities();
        public ObservableCollection<Receipt> receipts { get; set; }
        public string ErrorMessage { get; set; }

        public ReceiptBLL()
        {
            receipts = new ObservableCollection<Receipt>();
        }

        public void AddMethod(object obj)
        {
            //parametrul obj este cel dat prin CommandParameter cu MultipleBinding la Button in xaml
            Receipt rct = obj as Receipt;
            if (rct.EmployeeID == 0)
                ErrorMessage = "Selecteaza un angajat din lista";
            else if (rct.Date == null)
                ErrorMessage = "Data trebuie precizat";
            else
            {
                context.AddReceipt(rct.EmployeeID, rct.Date, new ObjectParameter("rctId", rct.ReceiptID));
                context.SaveChanges();
                rct.ReceiptID = context.Receipts.Max(item => item.ReceiptID);
                receipts.Add(rct);
                ErrorMessage = "";
            }
        }

        //public void DeleteMethod(object obj)
        //{
        //    Receipt rct = obj as Receipt;
        //    if (rct == null)
        //    {
        //        ErrorMessage = "Selecteaza un bon";
        //    }
        //    else
        //    {
        //        Receipt r = context.Receipts.Where(i => i.ReceiptID == rct.ReceiptID).FirstOrDefault();
        //        if (r != null)
        //        {
        //            context.DeleteProduct(r.ReceiptID);
        //            context.SaveChanges();
        //            receipts.Remove(r);
        //            ErrorMessage = "";
        //        }
        //        else
        //        {
        //            ErrorMessage = "Bonul nu a fost găsit";
        //        }
        //    }
        //}
    }
}
