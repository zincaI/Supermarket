using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data.Objects;
using System.Windows.Automation.Peers;

namespace Tema3._1.Models.BusinessLogicLayer
{
    public class EmployeeBLL
    {
        public bool IsActive { get; set; } = true;
        private Supermarket2Entities context = new Supermarket2Entities();
        public ObservableCollection<Employee> EmployeeList { get; set; }
        public string ErrorMessage { get; set; }
        public static GetAllEmployee_Result loggedInEmpoyee;
        public event EventHandler<bool> OnLogInSuccessfull;


        public EmployeeBLL()
        {
            EmployeeList = new ObservableCollection<Employee>();
        }
        public ObservableCollection<Employee> GetAllEmployee()
        {
            List<Employee> employees = context.Employees.Where(c => c.IsActive).ToList();
            ObservableCollection<Employee> emp = new ObservableCollection<Employee>();
            foreach (Employee employee in employees)
            {
                emp.Add(employee);
            }
            return emp;
        }
        public void AddMethod(object obj)
        {
            Employee employee = obj as Employee;
            if (employee != null)
            {
                bool ok = false;
                foreach (Employee emp in EmployeeList)
                    if (emp.EmployeeID == employee.EmployeeID)
                        ok = true;
                if (ok)
                {
                    context.RestoreCategory(employee.EmployeeID);
                    context.ModifyCategory(employee.EmployeeID,employee.Name);
                }
                else
                {
                    if (string.IsNullOrEmpty(employee.Name))
                    {
                        ErrorMessage = "Numele angajatului nu este precizat";
                    }
                    else
                    {
                        context.Employees.Add(employee);
                        context.SaveChanges();
                        employee.EmployeeID = context.Employees.Max(item => item.EmployeeID);
                        EmployeeList.Add(employee);
                        ErrorMessage = "";
                    }
                }
            }
        }
        public void UpdateMethod(object obj)
        {
            Employee emp = obj as Employee;
            if (emp == null)
            {
                ErrorMessage = "Selecteaza angajat";
            }
            else if (string.IsNullOrEmpty(emp.Name))
            {
                ErrorMessage = "Numele angajatului trebuie precizat";
            }
            else
            {
                bool ok = false;
                foreach (Employee e in EmployeeList)
                    if (e.EmployeeID == emp.EmployeeID && e.IsActive == true)
                        ok = true;
                if (ok)
                {
                    context.ModifyEmployee(emp.EmployeeID,emp.Name,emp.Password,emp.Type);
                    context.SaveChanges();
                    ErrorMessage = "";
                }
                else
                {
                    ErrorMessage = "Nu exista angajatul.";
                }
            }
        }
        public void DeleteMethod(object obj)
        {
            Employee emp = obj as Employee;
            if (emp == null)
            {
                ErrorMessage = "Selecteaza un angajat";
            }
            else
            {
                Employee e = context.Employees.Where(i => i.EmployeeID == emp.EmployeeID).FirstOrDefault();
                if (e != null)
                {
                    context.DeleteEmployee(e.EmployeeID);
                    context.SaveChanges();
                    EmployeeList.Remove(e);
                    ErrorMessage = "";
                }
                else
                {
                    ErrorMessage = "Angajatul nu a fost găsit";
                }
            }
        }

        public void CheckExistingEmployee(object obj)
        {
            Employee employee = obj as Employee;
            if (employee != null)
            {
                if (string.IsNullOrEmpty(employee.Name))
                {
                    //de adaugat
                }
                else if (string.IsNullOrEmpty(employee.Password))
                {
                    //de adaugat
                }
                else
                {
                    ObjectResult<GetAllEmployee_Result> employees = context.GetAllEmployee();
                    foreach (var individualemployee in employees)
                    {
                        if (individualemployee.Name == employee.Name && individualemployee.Password == employee.Password &&individualemployee.IsActive==true)
                        {
                            loggedInEmpoyee = individualemployee;
                            OnLogInSuccessfull?.Invoke(this, individualemployee.Type == "admin");
                        }
                    }
                }
            }
        }

    }
}
