using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace autoTime.Models
{
    public class ConfigTZExtended: ConfigTZ, INotifyPropertyChanged
    {
        public string[] hoursAndMin
        {
            get { return base.StartHoursDay.Split(':'); }
            set
            {
                //if (value[0].Length == 1) value[0] = value[0].Insert(0, "0");
                //if (value[1].Length == 1) value[1] = value[1].Insert(0, "0");
                //base.StartHoursDay = value[0] + ":" + value[1];
                hoursAndMin = value;
            }
        }
        public List<bool> isChekedDays
        {
            get
            {
                Dictionary<string, bool> days = new Dictionary<string, bool>() { { "Sunday", false }, {"Monday", false }, {"Tuesday", false },
                                                                                 {"Wednesday", false }, {"Thursday", false }, {"Friday", false }, {"Saturday", false } };
                foreach (string holiday in base.Holidays)
                {
                    days[holiday] = true;
                }
                List<bool> isChecked = days.Values.ToList();
                return isChecked;
            }
            set
            {
                //Dictionary<int, string> days = new Dictionary<int, string>() { { 0, "Sunday" }, {1, "Monday"}, {2, "Tuesday"},
                //                                                               {3, "Wednesday"}, {4, "Thursday"}, {5, "Friday"}, {6, "Saturday"} };
                //List<string> holidaysRec = new List<string>();
                //for (int i = 0; i < isChekedDays.Length; ++i)
                //{
                //    if (isChekedDays[i])
                //    {
                //        holidaysRec.Add(days[i]);
                //    }
                //}
                //base.Holidays = holidaysRec;
                isChekedDays = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private ConfigTZExtended defaultConfig()
        {
            return new ConfigTZExtended()
            {
                Email = "Example@amdocs.com",
                Holidays = new List<string>() { "Sunday", "Saturday" },
                StartHoursDay = "08:30",
                HoursDay = 8,
                HoursWeekDay = 40,
                isTestedMode = true
            };
        }
        public ConfigTZExtended readFromFile(ConfigAPP configAPP)
        {
            ConfigTZExtended configTZExtended = new ConfigTZExtended();
            string jsonString;
            try
            {
                StreamReader reader = new StreamReader(configAPP.PathBin + configAPP.NameFileConfigTZ);
                jsonString = reader.ReadToEnd();

                configTZExtended = JsonSerializer.Deserialize<ConfigTZExtended>(jsonString);
                reader.Close();
            }
            catch (Exception ex)
            {
                configTZExtended = defaultConfig();
                Console.WriteLine(ex.Message);
            }
            return configTZExtended;
        }

        public void saveToFile(ConfigAPP configAPP)
        {
            if (hoursAndMin[0].Length == 1) hoursAndMin[0] = hoursAndMin[0].Insert(0, "0");
            if (hoursAndMin[1].Length == 1) hoursAndMin[1] = hoursAndMin[1].Insert(0, "0");
            string startHoursDay = hoursAndMin[0] + ":" + hoursAndMin[1];

            Dictionary<int, string> days = new Dictionary<int, string>() { { 0, "Sunday" }, {1, "Monday"}, {2, "Tuesday"},
                                                                           {3, "Wednesday"}, {4, "Thursday"}, {5, "Friday"}, {6, "Saturday"} };
            List<string> holidaysRec = new List<string>();
            for (int i = 0; i < isChekedDays.Count; ++i)
            {
                if (isChekedDays[i])
                {
                    holidaysRec.Add(days[i]);
                }
            }


            ConfigTZ configTZ = new ConfigTZ()
            {
                Email = Email,
                Holidays = holidaysRec,
                StartHoursDay = startHoursDay,
                HoursDay = HoursDay,
                HoursWeekDay = HoursWeekDay,
                isTestedMode = isTestedMode
            };

            using (StreamWriter writer = new StreamWriter("bin/" + configAPP.NameFileConfigTZ, false))
            {
                try
                {
                    string json = JsonSerializer.Serialize(configTZ);
                    writer.WriteLine(json);
                }
                catch (Exception ex) { Console.WriteLine(ex); }
            }
        }

        //public string[] splitStartHoursDay() => StartHoursDay.Split(':');
    }
}

