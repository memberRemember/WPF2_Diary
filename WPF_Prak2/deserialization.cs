using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Prak2
{
    internal class deserialization
    {
        private static string FileName = "DiaryZametki.json";
        private static string Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public static T Deserialize<T>()
        {
            if (!File.Exists(Path + "\\" + FileName))
            {
                List<Zametka> aktemaz = new List<Zametka>();
                string zametka = JsonConvert.SerializeObject(aktemaz);
                File.WriteAllText(Path + "\\" + FileName, zametka);
            }

            string json = File.ReadAllText(Path + "\\" + FileName);
            T abeme123 = JsonConvert.DeserializeObject<T>(json);
            return abeme123;
        }

        public static void Serialize<T>(T SaveIt)
        {
            string json = JsonConvert.SerializeObject(SaveIt);
            File.WriteAllText(Path + "\\" + FileName, json);
        }
    }
}