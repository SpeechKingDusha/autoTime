using autoTime.Core;
using autoTime.Models;
using System.ComponentModel;
using System.Diagnostics;
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
        public ConfigTSExt configTSExt { get; set; }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public MainWindowVM() { 
            configAPP = ConfigAPP.initialize();
            configTZExtended = new ConfigTZExtended().readFromFile(configAPP);
            configTSExt = new ConfigTSExt().readFromFile(configAPP);
        }

        public void SaveConfigToFile(ISavable configT) {
            configT.saveToFile(configAPP);
        }

        public ICommand SaveCommand { 
            get
            {
                return new ActionCommand((obj) =>
                {
                    doSaveCommand(obj as ISavable);
                });

            }
        }

        public ICommand StartCommand
        {
            get
            {
                return new ActionCommand((obj) =>
                {
                    doStartCommand(obj as string);
                });

            }
        }

        private void doSaveCommand(ISavable param)
        {
            try
            {
                param.saveToFile(configAPP);
                MessageBox.Show("Configuration saved!", "Configuration", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (System.Exception)
            {
                MessageBox.Show("Configuration was not saved!", "Configuration", MessageBoxButton.OK, MessageBoxImage.Error);
            } 
        }

        private void doStartCommand(string param)
        {
            try
            {
                
               Process.Start(configAPP.PathBin.Replace('/', '\\') + param);
            }
            catch (System.Exception)
            {

                MessageBox.Show("File " + param + " not found", "Execute process", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
