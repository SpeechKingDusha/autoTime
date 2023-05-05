using autoTime.Core;
using autoTime.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace autoTime.ViewModels
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //public ConfigTZ configTZ { get; set; }
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
            //configTZExtended.hoursStartDay = Int32.Parse(configTZExtended.StartHoursDay.Split(':')[0]);
            //configTZExtended.minutesStartDay = Int32.Parse(configTZExtended.StartHoursDay.Split(':')[1]);
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

            MessageBox.Show("Saving");
            if (configTZExtended != null) {
                configTZExtended.saveToFile(configAPP); 
            }
        }

    }
}
