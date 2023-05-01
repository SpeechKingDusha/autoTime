using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace autoTime.Models
{
    public class ConfigAPP
    {
        const string NAMECONFIG = "ConfigAPP.json";
        public string NameFileConfigTZ { get; set; }
        public string NameFileConfigTS { get; set; }
        public string NameProcessTZ { get; set; }
        public string NameProcessTS { get; set; }
        public string PathBin { get; set; }
        public string PrefixUserNameTS { get; set; }

        static public ConfigAPP initialize() { 
            string json;
            ConfigAPP configApp = new ConfigAPP();
            using (StreamReader reader = new StreamReader(NAMECONFIG))
            {
                json = reader.ReadToEnd();
            }
            try
            {
                configApp = JsonSerializer.Deserialize<ConfigAPP>(json);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return configApp;
        }
    }
}
