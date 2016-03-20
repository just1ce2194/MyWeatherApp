using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWeatherApp
{
    class Settings
    {
        private static Settings instance;

        public string api_key;
        public string service_url;

        private Settings() {
            using (StreamReader file = File.OpenText(@"Data\settings.json"))
            {
                string json = file.ReadToEnd();
                var result = JsonConvert.DeserializeObject<dynamic>(json);
                this.api_key = result.api_key;
                this.service_url = result.service_url;
            }

        }

        public static Settings Instance
        {
            get
            {
                if (instance == null)
                    instance = new Settings();
                return instance;
            }
        }
    }
}
