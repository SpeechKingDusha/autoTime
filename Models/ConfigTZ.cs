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
        public string[] hoursAndMin
        {
            get { return StartHoursDay.Split(':'); }
            set {
                if (value[0].Length == 1) value[0]=value[0].Insert(0, "0");
                if (value[1].Length == 1) value[1] = value[1].Insert(0, "0");
                StartHoursDay = value[0] + ":" + value[1]; 
            }
        }
        public bool[] isChekedDays {
            get {
                Dictionary<string, bool> days = new Dictionary<string, bool>() { { "Sunday", false }, {"Monday", false }, {"Tuesday", false },
                                                                                 {"Wednesday", false }, {"Thursday", false }, {"Friday", false }, {"Saturday", false } };
                foreach (string holiday in Holidays) {
                    days[holiday] = true;
                }
                bool[] isChecked = days.Values.ToArray();  
                return isChecked;
            }
            set {
                Dictionary<int, string> days = new Dictionary<int, string>() { { 0, "Sunday" }, {1, "Monday"}, {2, "Tuesday"},
                                                                               {3, "Wednesday"}, {4, "Thursday"}, {5, "Friday"}, {6, "Saturday"} };
                List<string> holidaysRec = new List<string>();
                for (int i = 0; i < isChekedDays.Length; ++i)
                {
                    if (isChekedDays[i])
                    {
                        holidaysRec.Add(days[i]);
                    }
                }
                Holidays = holidaysRec;
            }
        }

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
        public string[] splitStartHoursDay()=> StartHoursDay.Split(':');
    }

}
