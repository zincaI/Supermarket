using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data.Objects;
using System.Windows.Automation.Peers;
using System.Windows;

namespace Tema3._1.Models.BusinessLogicLayer
{
    public class CategoryBLL
    {
        private Supermarket2Entities context = new Supermarket2Entities();
        public ObservableCollection<Category> CategoryList { get; set; }
        public string ErrorMessage { get; set; }

        public CategoryBLL()
        {
            
            CategoryList = new ObservableCollection<Category>();
            GetAllCategory();
        }
        public ObservableCollection<Category> GetAllCategory()
        {
        
            List<Category> categories = context.Categories.ToList();
            ObservableCollection<Category> categ = new ObservableCollection<Category>();
            foreach (Category category in categories)
            {
                categ.Add(category);
            }
            CategoryList = categ; 
            return categ;
        }
        public void AddMethod(object obj)
        {
            Category category = obj as Category;
            if (category != null)
            {
                bool exists = false;
                bool isActive = false;
                Category existCategory = null;

                foreach (Category cat in CategoryList)
                {
                    if (cat.CategoryID == category.CategoryID)
                    {
                        exists = true;
                        if (cat.IsActive == false)
                        {
                            isActive = true;
                            existCategory = cat;
                        }
                        break; 
                    }
                }

                if (isActive)
                {
                    
                    context.RestoreCategory(category.CategoryID);
                    existCategory.IsActive = true;  // Mark it as active in the list
                    existCategory.Name = category.Name;  // Update the name in the list
                    context.ModifyCategory(category.CategoryID, category.Name);
                    context.SaveChanges(); // Save changes to the database
                    ErrorMessage = "";
                }
                else if (!exists)
                {
                    if (string.IsNullOrEmpty(category.Name))
                    {
                        ErrorMessage = "Numele categoriei nu este precizat";
                    }
                    else
                    {
                        context.Categories.Add(category);
                        context.SaveChanges();
                        category.CategoryID = context.Categories.Max(item => item.CategoryID);
                        CategoryList.Add(category);
                        ErrorMessage = "";
                    }
                }
            }
        }
        public void UpdateMethod(object obj)
        {
            Category categ = obj as Category;
            if (categ == null)
            {
                ErrorMessage = "Selecteaza categorie";
            }
            else if (string.IsNullOrEmpty(categ.Name))
            {
                ErrorMessage = "Numele categoriei trebuie precizat";
            }
            else
            {
                bool ok = false;
                foreach (Category cat in CategoryList)
                    if (cat.CategoryID == categ.CategoryID && cat.IsActive == true)
                    {
                        ok = true;

                    }
                if (ok)
                {
                    MessageBox.Show($"Categoria a fost actualizata.");
                    context.ModifyCategory(categ.CategoryID, categ.Name);
                    context.SaveChanges();
                    ErrorMessage = "";
                }
                else
                {
                    ErrorMessage = "Nu exista categoria.";
                }
            }
        }
        public void DeleteMethod(object obj)
        {
            Category categ = obj as Category;
            if (categ == null)
            {
                ErrorMessage = "Selecteaza o categorie";
            }
            else
            {
                Category c = context.Categories.Where(i => i.CategoryID == categ.CategoryID).FirstOrDefault();
                if (c != null)
                {
                    context.DeleteCategory(c.CategoryID);
                    context.SaveChanges();
                    CategoryList.Remove(c);
                    ErrorMessage = "";
                }
                else
                {
                    ErrorMessage = "Categoria nu a fost găsită";
                }
            }
        }

    }
}
