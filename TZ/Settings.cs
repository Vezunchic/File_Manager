using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager
{
    public class Settings
    {
        public string PathGurenDirectory { get; set; } = "C:\\";
        public int NumberElementsPage { get; set; } = 4;
        public static Settings LoadConfiguration(string path) // берет путь и количество элементов которые будут выводиться на одной странице из файла, если файла нет то создает его 
        {
            if (File.Exists(path))
            {
                var textToFile = File.ReadAllText(path);
                var config = JsonConvert.DeserializeObject<Settings>(textToFile);
                return config;
            }
            var configuration = new Settings();
            string toFile = JsonConvert.SerializeObject(configuration);
            using (var sw = File.AppendText(path))
            {
                sw.WriteLine(toFile);
            }
            return configuration;

        }
    }
}
