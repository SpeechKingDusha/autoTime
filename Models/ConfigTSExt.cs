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
                domainName = @"Bell-main\",
                userName = "Example",
                password = "password",
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
                //configTSExt.password = DecodingPassword(configTSExt.password);
                configTSExt.password = configTSExt.password;
            }
            catch (Exception ex)
            {
                configTSExt = defaultConfig();
                Console.WriteLine(ex.Message);
            }


            return configTSExt;
        }

        private string encodingPassword() {
            var passwordArray = base.password.ToArray<char>();
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
        private string DecodingPassword(string pas)
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
                domainName = domainName,
                userName = userName,
                //password = encodingPassword(),
                password = password,
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
