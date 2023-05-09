using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace autoTime.Models
{
    public class ConfigTSExt : ConfigTS, ISavable
    {
        public bool IsVisiblePass { get; set; }
        private ConfigTSExt defaultConfig()
        {
            return new ConfigTSExt()
            {
                UserName = "Example",
                Password = "Password",
                IsVisiblePass = false,
                isTestedMode = true
            };
        }
        public ConfigTSExt readFromFile(ConfigAPP configAPP)
        {
            ConfigTSExt configTSExt = new ConfigTSExt();
            string jsonString;
            try
            {
                StreamReader reader = new StreamReader(configAPP.PathBin + configAPP.NameFileConfigTS);
                jsonString = reader.ReadToEnd();

                configTSExt = JsonSerializer.Deserialize<ConfigTSExt>(jsonString);
                reader.Close();
            }
            catch (Exception ex)
            {
                configTSExt = defaultConfig();
                Console.WriteLine(ex.Message);
            }


            return configTSExt;
        }
        public void saveToFile(ConfigAPP configAPP)
        {
            ConfigTS configTS = new ConfigTS()
            {
                UserName = UserName,
                Password = Password,
                isTestedMode = isTestedMode
            };

            using (StreamWriter writer = new StreamWriter(configAPP.PathBin + configAPP.NameFileConfigTS, false))
            {
                try
                {
                    string json = JsonSerializer.Serialize(configTS);
                    writer.WriteLine(json);
                }
                catch (Exception ex) { Console.WriteLine(ex); }
            }
        }
    }
}
