using autoTime.Models;
using System;
using System.Windows.Input;

namespace autoTime.Core
{
    public class ActionCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly Action<ISavable> action;
        public ActionCommand(Action<ISavable> action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {

            action((ISavable)parameter);
        }
    }
}
