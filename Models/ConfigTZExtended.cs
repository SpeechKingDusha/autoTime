using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace autoTime.Models
{
    public class ConfigTZExtended: ConfigTZ, INotifyPropertyChanged
    {
        public int hoursStartDay { get; set; }
        public int minutesStartDay { get; set; }
        public Dictionary<string, bool> areHolidays { get; set; } = new Dictionary<string, bool>() { { "Sunday", true }, {"Monday", false }, {"Tuesday", false },
                                                                                 {"Wednesday", false }, {"Thursday", false }, {"Friday", false }, {"Saturday", true } };
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
                isTestedMode = true,
                hoursStartDay = 8,
                minutesStartDay = 30
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
            configTZExtended.hoursStartDay = Int32.Parse(configTZExtended.StartHoursDay.Split(':')[0]);
            configTZExtended.minutesStartDay = Int32.Parse(configTZExtended.StartHoursDay.Split(':')[1]);

            foreach (var holiday in configTZExtended.Holidays)
            {
                configTZExtended.areHolidays[holiday] = true;
            }

            return configTZExtended;
        }

        public void saveToFile(ConfigAPP configAPP)
        {
            StringBuilder startHoursDay = new StringBuilder(5);
            if (hoursStartDay < 9) startHoursDay.Append("0");
            startHoursDay.Append(hoursStartDay.ToString() + ":");
            if (minutesStartDay < 9) startHoursDay.Append("0");
            startHoursDay.Append(minutesStartDay.ToString());

            List<string> holidaysRec = new List<string>();

            foreach (var day in areHolidays) {
                if (day.Value) holidaysRec.Add(day.Key);
            
            }


            ConfigTZ configTZ = new ConfigTZ()
            {
                Email = Email,
                Holidays = holidaysRec,
                StartHoursDay = startHoursDay.ToString(),
                HoursDay = HoursDay,
                HoursWeekDay = HoursWeekDay,
                isTestedMode = isTestedMode
            };

            using (StreamWriter writer = new StreamWriter(configAPP.PathBin + configAPP.NameFileConfigTZ, false))
            {
                try
                {
                    string json = JsonSerializer.Serialize(configTZ);
                    writer.WriteLine(json);
                }
                catch (Exception ex) { Console.WriteLine(ex); }
            }
        }

    }
}

