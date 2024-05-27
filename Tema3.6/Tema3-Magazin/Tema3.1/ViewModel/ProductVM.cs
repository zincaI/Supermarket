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
    public class ProductVM:BaseVM
    {
        private ProductBLL productBLL;
        private string errorMessage;

        public ProductVM()
        {
            productBLL = new ProductBLL();
            //producerBLL.OperationCompleted += ProducerBLL_OperationCompleted;
            ProductsList = new ObservableCollection<Product>(productBLL.GetAllProducts().Where(product => product.IsActive == true));
        }

        public ObservableCollection<Product> ProductsList
        {
            get => productBLL.ProductsList;
            set => productBLL.ProductsList = value;
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
            Product product = obj as Product;
            if (product != null)
            {
                productBLL.AddMethod(product);
                ErrorMessage = productBLL.ErrorMessage;
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
            productBLL.UpdateMethod(obj);
            ErrorMessage = productBLL.ErrorMessage;
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
            productBLL.DeleteMethod(obj);
            ErrorMessage = productBLL.ErrorMessage;
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

        public void SearchProducerMethod(object obj)
        {
            productBLL.SearchProducerMethod(obj);
            ErrorMessage = productBLL.ErrorMessage;
        }

        private ICommand searchProducerCommand;
        public ICommand SearchProducerCommand
        {
            get
            {
                if (searchProducerCommand == null)
                {
                    searchProducerCommand = new RelayCommand(SearchProducerMethod);
                }
                return searchProducerCommand;
            }
        }
    }
}
