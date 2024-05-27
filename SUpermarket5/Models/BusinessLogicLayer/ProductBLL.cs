using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUpermarket5.Models.BusinessLogicLayer
{
    public class ProductBLL
    {
        private Supermarket4Entities context = new Supermarket4Entities();
        public ObservableCollection<Product> products { get; set; }
        public string ErrorMessage { get; set; }

        public ProductBLL()
        {
            products = new ObservableCollection<Product>();
        }

        public void AddMethod(object obj)
        {
            //parametrul obj este cel dat prin CommandParameter cu MultipleBinding la Button in xaml
            Product prd = obj as Product;
            if (prd.ProducerID == 0)
                ErrorMessage = "Selecteaza un producator din lista";
            else if (prd.CategoryID == 0)
                ErrorMessage = "Selecteaza o categorie din lista";
            else if (prd.BarCode == 0)
                ErrorMessage = "Codul de bare trebuie precizat";
            else
            {

                context.AddProduct(prd.ProducerID, prd.CategoryID, prd.Name, prd.BarCode, new ObjectParameter("prdID", prd.ProductID));
                context.SaveChanges();
                prd.ProductID = context.Products.Max(item => item.ProductID);
                products.Add(prd);
                ErrorMessage = "";
            }
        }


        public void UpdateMethod(object obj)
        {
            Product prd = obj as Product;
            if (prd == null)
            {
                ErrorMessage = "Selecteaza produs";
            }
            if (prd.ProducerID == 0)
                ErrorMessage = "Selecteaza un producator din lista";
            else if (prd.CategoryID == 0)
                ErrorMessage = "Selecteaza o categorie din lista";
            else if (prd.BarCode == 0)
                ErrorMessage = "Codul de bare trebuie precizat";
            else
            {
                bool ok = false;
                foreach (Product p in products)
                    if (p.ProductID == prd.ProductID && p.IsActive == true)
                        ok = true;
                if (ok)
                {
                    context.ModifyProduct(prd.ProductID, prd.ProducerID, prd.CategoryID, prd.Name, prd.BarCode);
                    context.SaveChanges();
                    ErrorMessage = "";
                }
                else
                {
                    ErrorMessage = "Nu exista produsul.";
                }
            }
        }

        public void DeleteMethod(object obj)
        {
            Product prd = obj as Product;
            if (prd == null)
            {
                ErrorMessage = "Selecteaza un produs";
            }
            else
            {
                Product p = context.Products.Where(i => i.ProductID == prd.ProductID).FirstOrDefault();
                if (p != null)
                {
                    context.DeleteProduct(p.ProductID);
                    context.SaveChanges();
                    products.Remove(p);
                    ErrorMessage = "";
                }
                else
                {
                    ErrorMessage = "Produsul nu a fost găsit";
                }
            }
        }

    }
}
