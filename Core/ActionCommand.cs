using System;
using System.Windows.Input;

namespace autoTime.Core
{
    internal class ActionCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly Action action;
        public ActionCommand(Action action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action();
        }
    }
}
