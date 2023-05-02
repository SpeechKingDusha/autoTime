using autoTime.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace autoTime.Models
{
    public class ConfigTZ: ISavable, INotifyPropertyChanged
    {
        public string Email { get; set; }
        public List<String> Holidays { get; set; }
        public string StartHoursDay { get; set; }
        public int HoursDay { get; set; }
        public int HoursWeekDay { get; set; }
        public bool isTestedMode { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private ConfigTZ defaultConfig() { 
        return new ConfigTZ() {Email="Example@amdocs.com", Holidays = new List<string>() {"Sunday","Saturday"}, 
                               StartHoursDay="8:30",HoursDay=8,HoursWeekDay=40, isTestedMode=true};
        }
        public ConfigTZ readFromFile(ConfigAPP configAPP)
        {
            ConfigTZ configTZ = new ConfigTZ();
            string jsonString;
            try
            {
                StreamReader reader = new StreamReader(configAPP.PathBin + configAPP.NameFileConfigTZ);
                jsonString = reader.ReadToEnd();

                configTZ = JsonSerializer.Deserialize<ConfigTZ>(jsonString);
                reader.Close();
            } 
            catch (Exception ex)
            {
                configTZ = defaultConfig();
                Console.WriteLine(ex.Message);
            }
            return configTZ;
        }

        public void saveToFile(ConfigAPP configAPP)
        {
            ConfigTZ configTZ = new ConfigTZ() { Email = Email, Holidays = Holidays, 
                                                 StartHoursDay = StartHoursDay, HoursDay = HoursDay, 
                                                 HoursWeekDay = HoursWeekDay, isTestedMode = isTestedMode };

            using (StreamWriter writer = new StreamWriter(configAPP.PathBin + configAPP.NameFileConfigTZ, false))
            {
                try
                {
                    string json = JsonSerializer.Serialize(configTZ);
                    writer.WriteLine(json);
                }
                catch(Exception ex) { Console.WriteLine(ex); }
            }
        }
    }

}
