using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyWeatherApp
{
    class WeatherApi
    {
        private string service_url;
        private string api_key;

        public WeatherApi()
        {
            this.api_key = Settings.Instance.api_key;
            this.service_url = Settings.Instance.service_url;
        }
        public WeatherData getDataByCity(string city)
        {
            StringBuilder url = new StringBuilder(service_url);
            url.Append(city);
            url.Append("&units=metric&APPID=");
            url.Append(api_key);
            return getDataByUrl(url.ToString());
        }
        public WeatherData getDataByUrl(string url)
        {
            WeatherData result = new WeatherData();
            HttpClient client = new HttpClient();
            using (HttpResponseMessage response = client.GetAsync(url).Result)
            {
                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<WeatherData>(response.Content.ReadAsStringAsync().Result);
                }
            }
            return result;
        }
    }
}
