using autoTime.Core;
using autoTime.Models;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
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

        public ICommand HelpCommand
        {
            get
            {
                return new ActionCommand((obj) =>
                {
                    doHelpCommand(obj as string);
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

        private void doHelpCommand(string param)
        {
            MessageBox.Show("Если отмечен \"Тестовый режим\", приложения автоматически заполняют и сохраняют данные, " +
                "но без Approve. Это дает вам возможность еще раз проверить и отправить данные вручную ", "Справка", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void doStartCommand(string param)
        {
            doSaveCommand(getObj(param));
            try
            {
                
               Process.Start(configAPP.PathBin.Replace('/', '\\') + param);
            }
            catch (System.Exception)
            {

                MessageBox.Show("File " + param + " not found", "Execute process", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private ISavable getObj (string Name)
        {
            if (Name.Equals("autoTimeZonePortable.exe")) return configTZExtended;
           
            return configTSExt;
        }
    }
}
