using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Tema3._1.Helpers;
using Tema3._1.Models;
using Tema3._1.Models.BusinessLogicLayer;

namespace Tema3._1.ViewModel
{
    public class StockVM :BaseVM
    {
        private StockBLL stockBLL;
        private string errorMessage;

        public StockVM()
        {
            stockBLL = new StockBLL();
            //stockBLL.OperationCompleted += StockBLL_OperationCompleted; ;
            StocksList = new ObservableCollection<Stock>(stockBLL.GetAllStocks().Where(stock => stock.IsActive == true));

        }

       

        public ObservableCollection<Stock> StocksList
        {
            get => stockBLL.stocks;
            set => stockBLL.stocks = value;
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
            Stock stock = obj as Stock;
            if (stock != null)
            {
                stockBLL.AddMethod(stock);
                ErrorMessage = stockBLL.ErrorMessage;
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
            stockBLL.UpdateMethod(obj);
            ErrorMessage = stockBLL.ErrorMessage;
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
            stockBLL.DeleteMethod(obj);
            ErrorMessage = stockBLL.ErrorMessage;
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
        public void FilterMethod(object obj)
        {
            stockBLL.FilterMethod(obj);
            ErrorMessage = stockBLL.ErrorMessage;
        }
        private ICommand filterCommand;
        public ICommand FilterCommand
        {
            get
            {
                if (filterCommand == null)
                {
                    filterCommand = new RelayCommand(FilterMethod);
                }
                return filterCommand;
            }
        }
    }
}

