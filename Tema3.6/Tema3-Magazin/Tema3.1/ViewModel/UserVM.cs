using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tema3._1.Helpers;
using Tema3._1.Models;
using Tema3._1.Models.BusinessLogicLayer;
using Tema3._1.Converters;
using System.IO;

namespace Tema3._1.ViewModel
{
    class UserVM :BaseVM
    {
        private EmployeeBLL employeeBLL;
        private SecureString password;
        private string errorMessage;
        public ObservableCollection<string> UserTypes { get; }
        public event EventHandler<bool> OnMoveToNextWindow;

        public ObservableCollection<Employee> employeeList
        {
            get => employeeBLL.EmployeeList;
            set=>employeeBLL.EmployeeList = value;
        }

        public SecureString Password
        {
            get { return password; }
            set
            {
                password = value;

                NotifyPropertyChanged(nameof(Password));
            }
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
        public UserVM()
        {
            employeeBLL = new EmployeeBLL();
            employeeBLL.OnLogInSuccessfull += EmployeeBLL_OnLogInSuccessfull;

            employeeList = new ObservableCollection<Employee>(employeeBLL.GetAllEmployee().Where(employee=>employee.IsActive==true));

            UserTypes = new ObservableCollection<string> { "admin", "cashier" };
        }
        private void EmployeeBLL_OnLogInSuccessfull(object sender, bool isAdmin)
        {
            OnMoveToNextWindow?.Invoke(this, isAdmin);
        }
        public void CheckExistingUserMethod(object obj)
        {
            Employee employee = obj as Employee;
            if (employee != null)
            {
                string plainPassword = ConvertToString.ConvertString(Password);
                employee.Password = plainPassword;

                foreach(Employee emp in  employeeList) 
                {
                    if(emp.Name==employee.Name)
                    {
                        SaveConnectedEmployeeId(emp.EmployeeID);
                    }
                }
                employeeBLL.CheckExistingEmployee(employee);
                ErrorMessage = employeeBLL.ErrorMessage;

            }
        }

        private void SaveConnectedEmployeeId(int employeeID)
        {
            string filePath = "EmployeeConected.txt";
            try
            {
                File.WriteAllText(filePath, employeeID.ToString());
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error saving employee ID: {ex.Message}";
            }
        }

        private ICommand logInCommand;
        public ICommand LogInCommand
        {
            get
            {
                if (logInCommand == null)
                {
                    logInCommand = new RelayCommand(CheckExistingUserMethod);
                }
                return logInCommand;
            }
        }

        public void AddMethod(object obj)
        {
            Employee employee = obj as Employee;
            if (employee != null)
            {
                string plainPassword = ConvertToString.ConvertString(Password);
                employee.Password = plainPassword;

                employeeBLL.AddMethod(employee);
                ErrorMessage = employeeBLL.ErrorMessage;
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
            employeeBLL.UpdateMethod(obj);
            ErrorMessage = employeeBLL.ErrorMessage;
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
            employeeBLL.DeleteMethod(obj);
            ErrorMessage = employeeBLL.ErrorMessage;
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
