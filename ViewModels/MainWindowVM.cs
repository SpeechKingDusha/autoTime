using autoTime.Core;
using autoTime.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace autoTime.ViewModels
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ConfigAPP configAPP { get; set; }
        public ConfigTZExtended configTZExtended { get; set; }
        
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public MainWindowVM() { 
            configAPP = ConfigAPP.initialize();
            configTZExtended = new ConfigTZExtended().readFromFile(configAPP);
        }

        public void SaveConfigToFile(ISavable configT) {
            configT.saveToFile(configAPP);
        }

        public ICommand SaveCommand { 
            get
            {
                return new ActionCommand(() =>
                {
                    doSaveCommand();
                });

            }
        }

        private void doSaveCommand()
        {
            MessageBox.Show("Configuration saved!", "Configuration", MessageBoxButton.OK, MessageBoxImage.Information);
            if (configTZExtended != null) {
                configTZExtended.saveToFile(configAPP); 
            }
        }

    }
}
