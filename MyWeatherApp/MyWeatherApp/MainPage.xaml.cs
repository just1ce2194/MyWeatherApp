using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Reflection;
using System.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// Документацию по шаблону элемента "Пустая страница" см. по адресу http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MyWeatherApp
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        DataLoader dataloader = new DataLoader();

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            WebRequest request = WebRequest.Create("http://api.openweathermap.org/data/2.5/find?q=Sergiyev%20Posad&APPID=a4efb5a77becd8d8a68e251cadafab28");
            WebResponse response = await request.GetResponseAsync();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            textBox.Text = responseFromServer;

          
            country_choose_cmbBox.ItemsSource = dataloader.getCountries();


            // Clean up the streams and the response.

        }

        private void country_choose_cmbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> cities = dataloader.getCities(country_choose_cmbBox.SelectedValue.ToString());
            cities.Sort();
            city_choose_cmbBox.ItemsSource = cities;
        }

        private async void city_choose_cmbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (city_choose_cmbBox.SelectedValue != null)
            {
                WebRequest request = WebRequest.Create("http://api.openweathermap.org/data/2.5/find?q=" + city_choose_cmbBox.SelectedValue.ToString() + ","+country_choose_cmbBox.SelectedValue.ToString()+ "&units=metric&APPID=a4efb5a77becd8d8a68e251cadafab28");
                WebResponse response = await request.GetResponseAsync();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                textBox.Text = responseFromServer;
                // WeatherData weather_data = JsonConvert.DeserializeObject<WeatherData>(responseFromServer);
                WeatherApi w_api = new WeatherApi();
                //WeatherData weather_data = w_api.getItems("http://api.openweathermap.org/data/2.5/find?q=" + city_choose_cmbBox.SelectedValue.ToString() + ","+country_choose_cmbBox.SelectedValue.ToString()+ "&units=metric&APPID=a4efb5a77becd8d8a68e251cadafab28");
                WeatherData weather_data = w_api.getDataByCity(city_choose_cmbBox.SelectedValue.ToString());
                textBox.Text = weather_data.list.First().wind.speed.ToString();




            }
        }
    }
}
