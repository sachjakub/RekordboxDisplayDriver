using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RekordboxDisplayDriver.Entities
{
    public class ConfigHandler
    {
        private string _fileName = "conf.cfg";
        public ConfigHandler() { }

        public void OpenConfig()
        {
            string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _fileName);

            if (!File.Exists(configFilePath))
            {
                CreateConfig(configFilePath);
            }

            Process.Start("notepad.exe", configFilePath);
        }

        private void CreateConfig(string path)
        {
            string defaultConfig = "name | deck 1 start | deck 1 height | deck 2 start | deck 2 height\n" +
                                   "ex.: (name;10;20;30;40) - do not delete first 2 lines!\n" +
                                   "default;80;70;151;70";
            File.WriteAllText(path, defaultConfig);
        }

        public List<string> LoadConfig()
        {
            string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _fileName);
            if (!File.Exists(configFilePath))
            {
                CreateConfig(configFilePath);
            }
            var lines = File.ReadAllLines(configFilePath).ToList();
            return lines.Skip(2).ToList();
        }
    }
}
