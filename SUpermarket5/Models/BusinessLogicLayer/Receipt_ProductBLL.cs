using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUpermarket5.Models.BusinessLogicLayer
{
    public class Receipt_ProductBLL
    {

        private Supermarket4Entities context = new Supermarket4Entities();
        public ObservableCollection<Receipt_Product> listProducts { get; set; }
        public string ErrorMessage { get; set; }

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
                    rp.ProductID = context.Receipt_Product.Max(item => item.ReceiptProductID);
                    listProducts.Add(rp);
                    ErrorMessage = "";

                }
            }
        }


    }
}
