using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace MyWeatherApp
{
    class DataLoader
    {
        public List<Country> countries;
        public List<City> cities;

        public DataLoader()
        {
            //loading countries
            using (StreamReader file = File.OpenText(@"Data\countries.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                var deserialized_countries = JToken.ReadFrom(reader);
                countries = ((JArray)deserialized_countries).Select(x => new Country
                {
                    name = (string)x["name"],
                    ISO_code = (string)x["alpha-2"]
                }).ToList();
            }
            
            //loading cities
            using (StreamReader file = File.OpenText(@"Data\cities.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                var deserialized_cities = JToken.ReadFrom(reader);
                cities = ((JArray)deserialized_cities).Select(x => new City
                {
                    name = (string)x["nm"],
                    countryCode = (string)x["countryCode"]
                }).ToList();
            }
        }

        public List<string> getCountries()
        {

            List<string> temp = new List<string>();
            foreach (Country c in countries)
            {
                temp.Add(c.name);
            }
            return temp;

        }
        public List<string> getCities(string country)
        {
            string ISO_code = "";
            foreach (Country c in countries)
            {
                if (c.name.Equals(country))
                {
                    ISO_code = c.ISO_code;
                    break;
                }
            }
       

            List<string> temp = new List<string>();
            foreach (City c in cities)
            {

                if (c.countryCode.Equals(ISO_code))
                    temp.Add(c.name);

            }
            return temp;
        }
    }


    class Country {
        public string name { get; set; }
        public string ISO_code { get; set; }
    }

    class City
    {
        public string name { get; set; }
        public string countryCode { get; set; }
    }

}
