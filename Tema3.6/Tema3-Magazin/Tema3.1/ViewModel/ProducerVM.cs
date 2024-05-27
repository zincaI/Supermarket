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
    public class ProducerVM :BaseVM
    {
        private ProducerBLL producerBLL;
        private string errorMessage;

        public ProducerVM()
        {
            producerBLL = new ProducerBLL();
            //producerBLL.OperationCompleted += ProducerBLL_OperationCompleted;
            ProducersList = new ObservableCollection<Producer>(producerBLL.GetAllProducer().Where(producer => producer.IsActive == true));
        }

        public ObservableCollection<Producer> ProducersList
        {
            get => producerBLL.ProducerList;
            set => producerBLL.ProducerList = value;
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
            Producer producer = obj as Producer;
            if (producer != null)
            {
                producerBLL.AddMethod(producer);
                ErrorMessage = producerBLL.ErrorMessage;
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
            producerBLL.UpdateMethod(obj);
            ErrorMessage = producerBLL.ErrorMessage;
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
            producerBLL.DeleteMethod(obj);
            ErrorMessage = producerBLL.ErrorMessage;
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
