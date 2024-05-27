﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SUpermarket5.Models.BusinessLogicLayer
{
    public class CategoryBLL
    {
        private Supermarket4Entities context = new Supermarket4Entities();
        public ObservableCollection<Category> CategoryList { get; set; }
        public string ErrorMessage { get; set; }

        public CategoryBLL()
        {
            CategoryList = new ObservableCollection<Category>();
        }
        public ObservableCollection<Category> GetAllCategory()
        {
            List<Category> categories = context.Categories.Where(c => c.IsActive).ToList();
            ObservableCollection<Category> categ = new ObservableCollection<Category>();
            foreach (Category category in categories)
            {
                categ.Add(category);
            }
            return categ;
        }
        public void AddMethod(object obj)
        {
            Category category = obj as Category;
            if (category != null)
            {
                bool ok = false;
                foreach (Category cat in CategoryList)
                    if (cat.CategoryID == category.CategoryID)
                        ok = true;
                if (ok)
                {
                    context.RestoreCategory(category.CategoryID);
                    context.ModifyCategory(category.CategoryID, category.Name);
                }
                else
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
                        ok = true;
                if (ok)
                {
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