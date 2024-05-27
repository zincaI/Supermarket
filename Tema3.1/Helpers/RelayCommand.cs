using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tema3._1.Helpers
{
    class RelayCommand : ICommand
    {
        private Action<object> commandTask;
        private Predicate<object> canExecuteTask;

        public RelayCommand(Action<object> workToDo)
            : this(workToDo, DefaultCanExecute)
        {
            commandTask = workToDo;
        }

        public RelayCommand(Action<object> workToDo, Predicate<object> canExecute)
        {
            commandTask = workToDo;
            canExecuteTask = canExecute;
        }

        private static bool DefaultCanExecute(object parameter)
        {
            return true;
        }

        public bool CanExecute(object parameter)
        {
            //return canExecuteTask != null && canExecuteTask(parameter);
            return true;
        }

        public event EventHandler CanExecuteChanged;
        /*{
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }*/

        public void Execute(object parameter)
        {
            commandTask(parameter);
        }
    }
}
