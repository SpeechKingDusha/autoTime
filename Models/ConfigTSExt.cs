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
                configTSExt.Password = uncodingPassword(configTSExt.Password);
            }
            catch (Exception ex)
            {
                configTSExt = defaultConfig();
                Console.WriteLine(ex.Message);
            }


            return configTSExt;
        }

        private string encodingPassword() {
            var passwordArray = base.Password.ToArray<char>();
            var codeWord = Environment.MachineName.ToArray<char>();
            int sumCodeWord = 0;

            foreach (char c in codeWord)
            {
                sumCodeWord += c;
            }

            for (int i = 0; i < passwordArray.Length; ++i)
            {
                passwordArray[i] -= (char)sumCodeWord;
                passwordArray[i] += 'a'; 
            }
        return new string(passwordArray);
        }
        private string uncodingPassword(string pas)
        {
            var newPas = pas.ToArray<char>();
            var codeWord = Environment.MachineName.ToArray<char>();
            int sumCodeWord = 0;

            foreach (char c in codeWord)
            {
                sumCodeWord += c;
            }

            for (int i = 0; i < newPas.Length; ++i)
            {
                newPas[i] -= 'a';
                newPas[i] += (char)sumCodeWord;
                
            }
            return new string(newPas);
        }

        public void saveToFile(ConfigAPP configAPP)
        {
            ConfigTS configTS = new ConfigTS()
            {
                UserName = UserName,
                Password = encodingPassword(),
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
