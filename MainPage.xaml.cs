using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace AL_Sem_3_Course_Project_I_Give_Up
{
    public partial class MainPage : ContentPage
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "0bbef86f96702509c74ef6e45f607d0598ac1066";
        private const string GeoApiKey = "8a0d2c22fd904ec49b9af73c93f9a40c";
        private string lat { get; set; }
        private string lon { get; set; }

        public MainPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private async void OnCheckEvenClicked(object sender, EventArgs e)
        {
            string ipAddress = NumberEntry.Text;

            if (!string.IsNullOrWhiteSpace(ipAddress))
            {
                string city = await GetCityByIp(ipAddress);
                ResultLabel.Text = city;
                string mapImageUrl = GenerateMapUrl(lat, lon);
                MapImage.Source = mapImageUrl;
            }
            else
            {
                ResultLabel.Text = "Ошибка: введите корректный IP-адрес";
            }
        }

        private async Task<string> GetCityByIp(string ipAddress)
        {
            // Пробуем получить ответ
            try
            {
                var response = await _httpClient.GetStringAsync($"http://suggestions.dadata.ru/suggestions/api/4_1/rs/iplocate/address?ip={ipAddress}&token={ApiKey}");
                var json = JObject.Parse(response);

                // Проверяем найден ли город
                try
                {
                    lat = json["location"]["data"]["geo_lat"].ToString();
                    lon = json["location"]["data"]["geo_lon"].ToString();
                    return json["location"]["data"]["city"].ToString();
                }
                catch
                {
                    return "Ошибка: город не найден";
                }
            }
            catch
            {
                return "Ошибка: не удалось получить ответ";
            }
        }

        private string GenerateMapUrl(string lat, string lon)
        {
            // URL для получения изображения карты
            return $"https://maps.geoapify.com/v1/staticmap?style=osm-bright-smooth&width=800&height=500&center=lonlat:{lon},{lat}&zoom=11&apiKey={GeoApiKey}";
        }
    }
}
