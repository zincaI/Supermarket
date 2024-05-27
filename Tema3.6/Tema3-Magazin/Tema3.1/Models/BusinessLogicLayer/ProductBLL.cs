
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Objects;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Tema3._1.Models.BusinessLogicLayer
{
    public class ProductBLL
    {
        public bool IsActive { get; set; } = true;
        private Supermarket2Entities context = new Supermarket2Entities();
        public ObservableCollection<Product> ProductsList { get; set; }
        public string ErrorMessage { get; set; }

        public ProductBLL()
        {
            ProductsList = new ObservableCollection<Product>();
        }

        public ObservableCollection<Product> GetAllProducts()
        {
            List<Product> products = context.Products.Where(c => c.IsActive).ToList();
            ObservableCollection<Product> prod = new ObservableCollection<Product>();
            foreach (Product product in products)
            {
                prod.Add(product);
            }
            return prod;
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
                context.Products.Add(prd);
                ObservableCollection<Product> products =GetAllProducts();
                foreach (Product product in products)
                {
                    if (product.BarCode == prd.BarCode)
                    {
                        ErrorMessage = "Bar code invalid";
                        return;
                    }
                }
                context.SaveChanges();
                prd.ProductID = context.Products.Max(item => item.ProductID);
                ProductsList.Add(prd);
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
                foreach (Product p in ProductsList)
                    if (p.ProductID == prd.ProductID && p.IsActive == true)
                        ok = true;
                if (ok)
                {
                    context.ModifyProduct(prd.ProductID, prd.ProducerID, prd.CategoryID,prd.Name, prd.BarCode);
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
                    ProductsList.Remove(p);
                    ErrorMessage = "";
                }
                else
                {
                    ErrorMessage = "Produsul nu a fost găsit";
                }
            }
        }

        public void SearchProducerMethod(object obj)
        {
            string producerName = obj as string;
            var query = from p in context.Products
                        where p.IsActive == true
                        join pr in context.Producers on p.ProducerID equals pr.ProducerID
                        where pr.Name.StartsWith(producerName)
                        join c in context.Categories on p.CategoryID equals c.CategoryID
                        group p by c.Name into g
                        select g;

            if (query != null)
            {

                ProductsList.Clear();
                var products = query.ToList();
                foreach (var group in products)
                {
                    ProductsList.Add(group.First());
                }
            }
        }

    }
}
