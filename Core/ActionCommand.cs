using autoTime.Models;
using System;
using System.Windows.Input;

namespace autoTime.Core
{
    public class ActionCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly Action<Object> action;
        public ActionCommand(Action<Object> action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {

            action(parameter);
        }
    }
}
