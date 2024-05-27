using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tema3._1.Helpers;
using Tema3._1.Models;
using Tema3._1.Models.BusinessLogicLayer;

namespace Tema3._1.ViewModel
{
    public class CategoryVM : BaseVM
    {
        private CategoryBLL categoryBLL;
        private string errorMessage;

        public ObservableCollection<Category> CategoriesList
        {
            get => categoryBLL.CategoryList;
            set => categoryBLL.CategoryList = value;
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

        public CategoryVM()
        {
            categoryBLL = new CategoryBLL();
            //categoryBLL.OperationCompleted += CategoryBLL_OperationCompleted;
            CategoriesList = new ObservableCollection<Category>(categoryBLL.GetAllCategory().Where(category => category.IsActive == true));
        }

        public void AddMethod(object obj)
        {
            Category category = obj as Category;
            if (category != null)
            {
                categoryBLL.AddMethod(category);
                ErrorMessage = categoryBLL.ErrorMessage;
            }
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

        public void UpdateMethod(object obj)
        {
            categoryBLL.UpdateMethod(obj);
            ErrorMessage = categoryBLL.ErrorMessage;
        }

        private ICommand updateCommand;
        public ICommand UpdateCommand
        {
            get
            {
                if (updateCommand == null)
                {
                    updateCommand = new RelayCommand(UpdateMethod);
                }
                return updateCommand;
            }
        }

        public void DeleteMethod(object obj)
        {
            categoryBLL.DeleteMethod(obj);
            ErrorMessage = categoryBLL.ErrorMessage;
        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new RelayCommand(DeleteMethod);
                }
                return deleteCommand;
            }
        }

    }
}
